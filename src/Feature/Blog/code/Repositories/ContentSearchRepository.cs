
namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ContentSearch.Linq;
    using ContentSearch.Linq.Utilities;
    using ContentSearch.SearchTypes;
    using Providers;
    using Search;

    public abstract class ContentSearchRepository<T> : IResultRepository<T> where T : SearchResultItem
    {
        protected readonly IIndexProvider IndexProvider;

        protected ContentSearchRepository(IIndexProvider indexProvider)
        {
            this.IndexProvider = indexProvider;
        }

        public virtual IQueryable<T> GetQueryable() => this.IndexProvider.SearchContext.GetQueryable<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public virtual SearchResults<T> GetResults(IQueryable<T> queryable)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable));
            }

            return queryable.GetResults();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public virtual FacetResults GetFacetResults(IQueryable<T> queryable)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable));
            }

            return queryable.GetFacets();
        }

        public virtual IQueryable<T> ApplyQueries(IQueryable<T> queryable, SearchQuery<T> searchQuery)
        {
            queryable = this.ApplyQuery(queryable, searchQuery.Queries);
            queryable = this.ApplyFilter(queryable, searchQuery.Filters);

            return queryable;
        }

        public virtual IQueryable<T> ApplyQuery(IQueryable<T> queryable, Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            return queryable;
        }


        public virtual IQueryable<T> ApplyFilter(IQueryable<T> queryable, Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                queryable = queryable.Filter(filter);
            }

            return queryable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public virtual IQueryable<T> ApplySorting(IQueryable<T> queryable, IEnumerable<SortExpression<T>> sorts)
        {
            var enumerable = sorts as SortExpression<T>[] ?? sorts.ToArray();

            if (!enumerable.Any())
            {
                return queryable;
            }

            var expressions = enumerable.Reverse();

            var sortExpressions = expressions as SortExpression<T>[] ?? expressions.ToArray();

            var orderByExpression = sortExpressions.First();

            var orderedQueryable = orderByExpression.Direction == SortExpression<T>.Sorting.Ascending
                ? queryable.OrderBy(orderByExpression.Expression)
                : queryable.OrderByDescending(orderByExpression.Expression);

            return sortExpressions.Skip(1)
                             .Aggregate(orderedQueryable,
                                 (current, expression) =>
                                     expression.Direction == SortExpression<T>.Sorting.Ascending
                                         ? current.ThenBy(expression.Expression)
                                         : current.ThenByDescending(expression.Expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public virtual IQueryable<T> ApplyPaging(IQueryable<T> queryable, Paging paging)
        {
            if (paging.StartingPosition > 0)
            {
                queryable = queryable.Skip(paging.StartingPosition);
            }

            if (paging.Display > 0)
            {
                queryable = queryable.Take(paging.Display);
            }

            return queryable;
        }

        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.IndexProvider?.Dispose();
            }
        }

        #endregion
    }
}