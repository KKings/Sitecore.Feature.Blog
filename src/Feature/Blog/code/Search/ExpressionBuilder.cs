
namespace Sitecore.Feature.Blog.Search
{
    using Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ContentSearch.Linq.Utilities;
    using ContentSearch.SearchTypes;

    public class ExpressionBuilder<T> where T : SearchResultItem
    {
        /// <summary>
        /// Contains the up-to-date filter at a given context before it is applied
        /// higher in the chain
        /// </summary>
        private Expression<Func<T, bool>> Filter { get; set; }

        public ExpressionBuilder(Expression<Func<T, bool>> filter = null)
        {
            this.Filter = filter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Expression<Func<T, bool>> Build()
        {
            return this.Filter;
        }



        /// <summary>
        /// Groups filters by AND
        /// </summary>
        /// <returns>Instance of <see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> And(Action<ExpressionBuilder<T>> filterAction)
        {
            var builder = new ExpressionBuilder<T>();

            filterAction(builder);

            var filter = builder.Build();

            this.Filter = this.Filter != null
                ? this.Filter.And(filter)
                : PredicateBuilder.True<T>().And(filter);

            return this;
        }

        /// <summary>
        /// Groups filters by Or
        /// </summary>
        /// <returns>Instance of <see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> Or(Action<ExpressionBuilder<T>> filterAction)
        {
            var builder = new ExpressionBuilder<T>();

            filterAction(builder);

            var filter = builder.Build();

            this.Filter = this.Filter != null
                ? this.Filter.Or(filter)
                : PredicateBuilder.False<T>().Or(filter);

            return this;
        }

        /// <summary>
        /// Adds to the expression tree as an 'And' [Current Expression Tree] AND [<see cref="filter"/>]
        /// <para>Example: To filter articles by a specific author, Where(result => result.Author == "Test")
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Author'
        /// </para>
        /// </summary>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> Where(Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                this.Filter = this.Filter != null
                    ? this.Filter.And(filter)
                    : PredicateBuilder.True<T>().And(filter);
            }

            return this;
        }

        /// <summary>
        /// Adds to the expression tree as an 'Or' [Current Expression Tree] OR [<see cref="filter"/>]
        /// <para>Example: To filter articles by a specific author, OrWhere(result => result.Author == "Test")
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Author'
        /// </para>
        /// </summary>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> OrWhere(Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                this.Filter = this.Filter != null
                    ? this.Filter.Or(filter)
                    : PredicateBuilder.False<T>().Or(filter);
            }

            return this;
        }

        /// <summary>
        /// Expression in which all <see cref="filter"/> need to be true for the entire expression to be true. Each
        /// <see cref="TR"/> generates a new <see cref="filter"/>. So 100 terms, will generate 100 expressions, and all must be true for the entire expression to be true.
        /// <para>Adds to the expression tree as an 'And' [Current Expression Tree] And [<see cref="filter"/>]</para>
        /// <para>Example: To filter articles by tags, All([Tag1, Tag2, Tag3], (result, tag) => result.Tags.Contains(tag))
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Tags'
        /// </para>
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> All<TR>(IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            var enumerable = terms as TR[] ?? terms.ToArray();

            if (enumerable.Any() && filter != null)
            {
                var predicate = PredicateBuilder.True<T>();

                predicate =
                    enumerable
                        .Select(filter.Rewrite)
                        .Aggregate(predicate, (current, expression) => current.And(expression));

                this.Filter = this.Filter != null
                    ? this.Filter.And(predicate)
                    : PredicateBuilder.True<T>().And(predicate);
            }

            return this;
        }

        /// <summary>
        /// Expression in which all <see cref="filter"/> need to be true for the entire expression to be true. Each
        /// <see cref="TR"/> generates a new <see cref="filter"/>. So 100 terms, will generate 100 expressions, and all must be true for the entire expression to be true.
        /// <para>Adds to the expression tree as an 'Or' [Current Expression Tree] Or [<see cref="filter"/>]</para>
        /// <para>Example: To filter articles by tags, <code>OrAll([Tag1, Tag2, Tag3], (result, tag) => result.Tags.Contains(tag))</code>,
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Tags'
        /// </para>
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> OrAll<TR>(IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            var enumerable = terms as TR[] ?? terms.ToArray();

            if (enumerable.Any() && filter != null)
            {
                var predicate = PredicateBuilder.True<T>();

                predicate =
                    enumerable
                        .Select(filter.Rewrite)
                        .Aggregate(predicate, (current, expression) => current.And(expression));

                this.Filter = this.Filter != null
                    ? this.Filter.Or(predicate)
                    : PredicateBuilder.False<T>().Or(predicate);
            }

            return this;
        }

        /// <summary>
        /// Builds an expression tree by applying a filter expression to each item within a group, then combining each group's expression tree together
        /// <para>Adds to the current expression tree as an 'And' [Current Expression Tree] And [Group 1 Expression And Group 2 Expression] </para>
        /// <para>Each group's expression tree will be combined by using an 'Or', so any term within the group can be matched</para>
        /// <para>Example: To filter articles by tags, <code>ManyAny([[Tag1, Tag2, Tag3], [Tag1, Tag2]], (result, tag) => result.Tags.Contains(tag))</code>,
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Tags'
        /// </para>
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="groups">A grouping of terms</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> ManyAny<TR>(IEnumerable<IEnumerable<TR>> groups, Expression<Func<T, TR, bool>> filter)
        {
            var enumerable = groups as IEnumerable<TR>[] ?? groups.ToArray();

            if (enumerable.Any() && filter != null)
            {
                var predicate = PredicateBuilder.True<T>();

                foreach (var group in enumerable)
                {
                    var inner = PredicateBuilder.False<T>();

                    inner
                        = group
                            .Select(filter.Rewrite)
                            .Aggregate(inner, (current, expression) => current.Or(expression));

                    // Magic here
                    predicate = predicate.And(inner);
                }

                this.Filter = this.Filter != null
                    ? this.Filter.And(predicate)
                    : PredicateBuilder.True<T>().And(predicate);
            }

            return this;
        }

        /// <summary>
        /// Builds an expression tree by applying a filter expression to each item within a group, then combining each group's expression tree together
        /// <para>Adds to the current expression tree as an 'Or' [Current Expression Tree] Or [Group 1 Expression And Group 2 Expression] </para>
        /// <para>Each group's expression tree will be combined by using an 'Or', so any term within the group can be matched</para>
        /// <para>Example: To filter articles by tags, <code>OrManyAny([[Tag1, Tag2, Tag3], [Tag4, Tag5]], (result, tag) => result.Tags.Contains(tag))</code>,
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Tags'
        /// </para>
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="groups">A grouping of terms</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> OrManyAny<TR>(IEnumerable<IEnumerable<TR>> groups, Expression<Func<T, TR, bool>> filter)
        {
            var enumerable = groups as IEnumerable<TR>[] ?? groups.ToArray();

            if (enumerable.Any() && filter != null)
            {
                var predicate = PredicateBuilder.True<T>();

                foreach (var group in enumerable)
                {
                    var inner = PredicateBuilder.False<T>();

                    inner
                        = group
                            .Select(filter.Rewrite)
                            .Aggregate(inner, (current, expression) => current.Or(expression));

                    // Magic here
                    predicate = predicate.And(inner);
                }

                this.Filter = this.Filter != null
                    ? this.Filter.Or(predicate)
                    : PredicateBuilder.False<T>().Or(predicate);
            }

            return this;
        }

        /// <summary>
        /// Expression in which only one <see cref="filter"/> needs to be true for the entire expression to be true. Each
        /// <see cref="TR"/> generates a new <see cref="filter"/>. So 100 terms, will generate 100 expressions, and only 1 needs to be true for the entire expression to be true.
        /// <para>Adds to the expression tree as an 'And' [Current Expression Tree] And [<see cref="filter"/>]</para>
        /// <para>Example: To filter articles by tags, <code>Any([Tag1, Tag2, Tag3], (result, tag) => result.Tags.Contains(tag))</code>,
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Tags'
        /// </para>
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> Any<TR>(IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            var enumerable = terms as TR[] ?? terms.ToArray();

            if (enumerable.Any() && filter != null)
            {
                var predicate = PredicateBuilder.False<T>();

                predicate =
                    enumerable
                        .Select(filter.Rewrite)
                        .Aggregate(predicate, (current, expression) => current.Or(expression));

                this.Filter = this.Filter != null
                    ? this.Filter.And(predicate)
                    : PredicateBuilder.True<T>().And(predicate);
            }

            return this;
        }

        /// <summary>
        /// Expression in which only one <see cref="filter"/> needs to be true for the entire expression to be true. Each
        /// <see cref="TR"/> generates a new <see cref="filter"/>. So 100 terms, will generate 100 expressions, and only 1 needs to be true for the entire expression to be true.
        /// <para>Adds to the expression tree as an 'Or' [Current Expression Tree] Or [<see cref="filter"/>]</para>
        /// <para>Example: To filter articles by tags, <code>Any([Tag1, Tag2, Tag3], (result, tag) => result.Tags.Contains(tag))</code>,
        /// where result is your <see cref="SearchResultItem"/> that contains a property of 'Tags'
        /// </para>
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> OrAny<TR>(IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            var enumerable = terms as TR[] ?? terms.ToArray();

            if (enumerable.Any() && filter != null)
            {
                var predicate = PredicateBuilder.False<T>();

                predicate =
                    enumerable
                        .Select(filter.Rewrite)
                        .Aggregate(predicate, (current, expression) => current.Or(expression));

                this.Filter = this.Filter != null
                    ? this.Filter.Or(predicate)
                    : PredicateBuilder.False<T>().Or(predicate);
            }

            return this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="Any{TR}"/> expression
        /// to the current expression tree
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> IfAny<TR>(bool condition, IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            return condition ? this.Any(terms, filter) : this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="OrAny{TR}"/> expression
        /// to the current expression tree
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> IfOrAny<TR>(bool condition, IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            return condition ? this.OrAny(terms, filter) : this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="Any{TR}"/> expression
        /// to the current expression tree
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> IfAll<TR>(bool condition, IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            return condition ? this.All(terms, filter) : this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="Any{TR}"/> expression
        /// to the current expression tree
        /// </summary>
        /// <typeparam name="TR">The type of Term</typeparam>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="terms">Terms that will be passed to the filter expression</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> IfOrAll<TR>(bool condition, IEnumerable<TR> terms, Expression<Func<T, TR, bool>> filter)
        {
            return condition ? this.OrAll(terms, filter) : this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="Where"/> expression
        /// to the current expression tree
        /// </summary>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> IfWhere(bool condition, Expression<Func<T, bool>> filter)
        {
            return condition ? this.Where(filter) : this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="OrWhere"/> expression
        /// to the current expression tree
        /// </summary>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public virtual ExpressionBuilder<T> IfOrWhere(bool condition, Expression<Func<T, bool>> filter)
        {
            return condition ? this.OrWhere(filter) : this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="ManyAny{TR}"/> expression
        /// to the current expression tree
        /// </summary>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="groups">A grouping of terms</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> IfManyAny<TR>(bool condition, IEnumerable<IEnumerable<TR>> groups,
            Expression<Func<T, TR, bool>> filter)
        {
            return condition ? this.ManyAny(groups, filter) : this;
        }

        /// <summary>
        /// If the <see cref="condition"/> evalutes to <c>True</c> at runtime, will add the <see cref="OrManyAny{TR}"/> expression
        /// to the current expression tree
        /// </summary>
        /// <param name="condition">If <c>True</c> will add to the expression tree at runtime.</param>
        /// <param name="groups">A grouping of terms</param>
        /// <param name="filter">Filter Expression</param>
        /// <returns><see cref="ExpressionBuilder{T}"/></returns>
        public ExpressionBuilder<T> IfOrManyAny<TR>(bool condition, IEnumerable<IEnumerable<TR>> groups,
            Expression<Func<T, TR, bool>> filter)
        {
            return condition ? this.OrManyAny(groups, filter) : this;
        }
    }
}