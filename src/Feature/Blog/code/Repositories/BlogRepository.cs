namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ContentSearch.Linq;
    using Data;
    using Domain;
    using Providers;
    using Search;
    using Sitecore.Feature.Blog.Feature.Blog;

    /// <summary>
    /// 
    /// </summary>
    public class BlogRepository<T> : ContentSearchRepository<T>, IBlogRepository<T> where T : BlogSearchResultItem
    {
        public BlogRepository(IIndexProvider indexProvider) : base(indexProvider)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IBlog Get(ID id)
        {
            var query = new SearchQuery<T>
            {
                Queries = new ExpressionBuilder<T>().Where(m => m.ItemId == id).Build(),
                Filters = new ExpressionBuilder<T>().Where(m => m.TemplateId == BlogPost.TemplateId).Build(),
                Paging = new Paging
                {
                    Display = 1
                }
            };

            return this.Query(query).FirstOrDefault();
        }

        public virtual IBlog Get(string slug)
        {
            var query = new SearchQuery<T>
            {
                Queries = new ExpressionBuilder<T>().Where(m => m.Name == slug).Build(),
                Filters = new ExpressionBuilder<T>().Where(m => m.TemplateId == BlogPost.TemplateId).Build(),
                Paging = new Paging
                {
                    Display = 1
                }
            };

            return this.Query(query).FirstOrDefault();
        }
        
        public virtual IEnumerable<T> Query(SearchQuery<T> searchQuery)
        {
            var queryable = this.GetQueryable();

            queryable = queryable.Where(result => result.TemplateId == BlogPost.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            queryable = this.ApplyQueries(queryable, searchQuery);
            queryable = this.ApplySorting(queryable, searchQuery.Sorts);
            queryable = this.ApplyPaging(queryable, searchQuery.Paging);

            var results = this.GetResults(queryable);

            return results.Hits.Select(m => m.Document);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual FacetResults Archives()
        {
            var queryable = this.GetQueryable();

            queryable = queryable.Where(result => result.TemplateId == BlogPost.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            queryable = queryable.FacetOn(m => m.ArchiveMonth);

            var results = this.GetFacetResults(queryable);

            return results;
        }
        }
}