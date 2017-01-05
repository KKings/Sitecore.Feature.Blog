namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ContentSearch.Linq;
    using ContentSearch.SearchTypes;
    using Data;
    using Domain;
    using Providers;
    using Search;
    using Sitecore.Feature.Blog.Feature.Blog;

    /// <summary>
    /// 
    /// </summary>
    public class BlogRepository : ContentSearchRepository, IBlogRepository
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
            var queryable = this.GetQueryable<BlogSearchResultItem>();

            queryable = queryable.Where(result => result.ItemId == id)
                                 .Where(result => result.TemplateId == BlogPost.TemplateId);

            queryable = queryable.Take(1);

            var results =  this.GetResults(queryable);

            return results.Hits.Any() ? results.Hits.First().Document : null;
        }

        public virtual IBlog Get(string slug)
        {
            var queryable = this.GetQueryable<BlogSearchResultItem>();

            queryable = queryable.Where(result => result.Name == slug)
                                 .Where(result => result.TemplateId == BlogPost.TemplateId);

            queryable = queryable.Take(1);

            var results = this.GetResults(queryable);

            return results.Hits.Any() ? results.Hits.First().Document : null;
        }

        public virtual IEnumerable<BlogSearchResultItem> All(Expression<Func<BlogSearchResultItem, object>> sorting = null, bool descending = false)
        {
            var queryable = this.GetQueryable<BlogSearchResultItem>();

            queryable = queryable.Where(result => result.TemplateId == BlogPost.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            if (sorting != null)
            {
                queryable = descending ? queryable.OrderByDescending(sorting) : queryable.OrderBy(sorting);
            }

            var results = this.GetResults(queryable);

            return results.Hits.Select(m => m.Document);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual FacetResults Archives()
        {
            var queryable = this.GetQueryable<BlogSearchResultItem>();

            queryable = queryable.Where(result => result.TemplateId == BlogPost.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            queryable = queryable.FacetOn(m => m.ArchiveMonth);

            var results = this.GetFacetResults(queryable);

            return results;
        }

        public virtual IEnumerable<BlogSearchResultItem> Related(IBlog blog)
        {
            throw new NotImplementedException();
        }
    }
}