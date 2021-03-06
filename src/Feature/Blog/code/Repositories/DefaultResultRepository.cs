﻿// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ContentSearch.Linq;
    using ContentSearch.SearchTypes;
    using Providers;
    using Search;

    public abstract class DefaultResultRepository<T> : IResultRepository<T> where T : SearchResultItem
    {
        protected readonly IIndexProvider IndexProvider;

        protected DefaultResultRepository(IIndexProvider indexProvider)
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
        public virtual ContentSearch.Linq.SearchResults<T> GetResults(IQueryable<T> queryable)
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

        public virtual IQueryable<T> ApplyQueries(IQueryable<T> queryable, ISearchQuery<T> searchQuery)
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