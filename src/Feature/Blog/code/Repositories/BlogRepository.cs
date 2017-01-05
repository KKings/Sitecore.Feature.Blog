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
        public IBlog Get(ID id)
        {
            var queryable = this.GetQueryable<BlogSearchResultItem>();

            queryable = queryable.Where(result => result.ItemId == id)
                                 .Where(result => result.TemplateId == BlogPost.TemplateId);

            queryable = queryable.Take(1);

            var results =  this.GetResults(queryable);

            return results.Hits.Any() ? results.Hits.First().Document : null;
        }

        public IBlog Get(string slug)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBlog> All()
        {
            var queryable = this.GetQueryable<BlogSearchResultItem>();

            queryable = queryable.Where(result => result.TemplateId == BlogPost.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");
            
            var results = this.GetResults(queryable);

            return results.Hits.Select(m => m.Document);
        }

        public IEnumerable<IBlog> Related(IBlog blog)
        {
            throw new NotImplementedException();
        }
    }
}