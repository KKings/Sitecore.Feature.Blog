namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Providers;
    using Search;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class TagRepository : ContentSearchRepository, ITagRepository
    {
        public TagRepository(IIndexProvider indexProvider) : base(indexProvider) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITag> All()
        {
            var queryable = this.GetQueryable<TagSearchResultItem>();

            queryable = queryable.Where(result => result.TemplateId == BlogTag.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            queryable = queryable.OrderBy(result => result.TagName);

            var results = this.GetResults(queryable);

            return results.Hits.Any() ? results.Hits.Select(result => result.Document) : null;
        }
    }
}