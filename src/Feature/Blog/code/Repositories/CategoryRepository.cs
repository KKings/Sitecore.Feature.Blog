namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Providers;
    using Search;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class CategoryRepository : ContentSearchRepository, ICategoryRepository
    {
        public CategoryRepository(IIndexProvider indexProvider) : base(indexProvider) { }

        public IEnumerable<ICategory> All()
        {
            var queryable = this.GetQueryable<CategorySearchResultItem>();

            queryable = queryable.Where(result => result.TemplateId == BlogCategory.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            queryable = queryable.OrderBy(result => result.CategoryName);

            var results = this.GetResults(queryable);

            return results.Hits.Any() ? results.Hits.Select(result => result.Document) : null;
        }
    }
}