// MIT License
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

    public class DefaultContentRepository<T> : DefaultResultRepository<T>, IContentRespository<T> where T : SearchResultItem
    {
        public DefaultContentRepository(IIndexProvider indexProvider) : base(indexProvider) { }

        public virtual ContentSearch.Linq.SearchResults<T> Query(ISearchQuery<T> searchQuery)
        {
            var queryable = this.GetQueryable();
            
            queryable = this.ApplyQueries(queryable, searchQuery);
            queryable = this.ApplySorting(queryable, searchQuery.Sorts);
            queryable = this.ApplyPaging(queryable, searchQuery.Paging);

            var results = this.GetResults(queryable);

            return results;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual FacetResults Facet<TKey>(ISearchQuery<T> query, params Expression<Func<T, TKey>>[] selectors)
        {
            var queryable = this.GetQueryable();   

            queryable = this.ApplyQueries(queryable, query);
            queryable = this.ApplySorting(queryable, query.Sorts);
            queryable = this.ApplyPaging(queryable, query.Paging);

            queryable = selectors.Aggregate(queryable, (current, selector) => current.FacetOn(selector));

            var results = this.GetFacetResults(queryable);

            return results;
        }
    }
}