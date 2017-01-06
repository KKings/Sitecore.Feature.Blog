namespace Sitecore.Feature.Blog.Search
{
    using System;
    using System.Linq.Expressions;
    using ContentSearch.SearchTypes;

    public class SortExpression<T> where T : SearchResultItem
    {
        public enum Sorting
        {
            Ascending,
            Descending
        }

        /// <summary>
        /// Gets the Sorting Expression
        /// </summary>
        public Expression<Func<T, object>> Expression { get; private set; }

        /// <summary>
        /// Gets the Sorting Direction
        /// </summary>
        public Sorting Direction { get; private set; }

        public SortExpression(Expression<Func<T, object>> expression, Sorting direction = Sorting.Ascending)
        {
            this.Expression = expression;
            this.Direction = direction;
        }
    }
}