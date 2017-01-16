namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Items;
    using Repositories;
    using Search;
    using Search.Results;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class TagService : ITagService
    {
        private readonly IContentRespository<TagSearchResultItem> repository;

        public TagService(IContentRespository<TagSearchResultItem> tagRepository)
        {
            this.repository = tagRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TagItem> All(BlogContext context)
        {
            var query = new SearchQuery<TagSearchResultItem>
            {
                Queries = new ExpressionBuilder<TagSearchResultItem>()
                                .IfWhere(context != null, m => m.Paths.Contains(context.Blog))
                                .Build(),
                Filters = new ExpressionBuilder<TagSearchResultItem>()
                                .Where(result => result.TemplateId == BlogTag.TemplateId)
                                .Where(result => result.Name != "__Standard Values")
                                .Build(),
                Sorts = new[]
                {
                    new SortExpression<TagSearchResultItem>(result => result.TagName)
                }
            };

            var searchResults = this.repository.Query(query);

            return searchResults.Hits.Select(result => (TagItem)result.Document.GetItem());
        }
    }
}