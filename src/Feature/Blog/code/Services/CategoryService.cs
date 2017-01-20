namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Items;
    using Repositories;
    using Search;
    using Search.Results;
    using Models;

    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IContentRespository<CategorySearchResultItem> repository;

        public CategoryService(IContentRespository<CategorySearchResultItem> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all categories based on the <see cref="BlogContext"/>
        /// </summary>
        /// <param name="context">The Blog Context</param>
        /// <returns>List of all categories for the blog</returns>
        public virtual IEnumerable<CategoryItem> All(BlogContext context)
        {
            var query = new SearchQuery<CategorySearchResultItem>
            {
                Queries = new ExpressionBuilder<CategorySearchResultItem>()
                    .IfWhere(context != null, m => m.Paths.Contains(context.Blog))
                    .Build(),
                Filters = new ExpressionBuilder<CategorySearchResultItem>()
                    .Where(result => result.TemplateIds.Contains(BlogCategory.TemplateId))
                    .Where(result => result.Name != "__Standard Values")
                    .Build(),
                Sorts = new[]
                {
                    new SortExpression<CategorySearchResultItem>(result => result.CategoryName)
                }
            };

            var searchResults = this.repository.Query(query);

            return searchResults.Select(result => (CategoryItem)result.Document.GetItem());
        }
    }
}