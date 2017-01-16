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