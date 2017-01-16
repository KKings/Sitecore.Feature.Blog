namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ContentSearch.SearchTypes;
    using Domain;
    using Providers;
    using Search;
    using Search.Results;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class TagRepository<T> : ContentSearchRepository<T>, ITagRepository<T> where T : TagSearchResultItem
    {
        public TagRepository(IIndexProvider indexProvider) : base(indexProvider) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> All()
        {
            var query = new SearchQuery<T>
            {
                Sorts = new[]
                {
                    new SortExpression<T>(result => result.TagName, SortExpression<T>.Sorting.Descending)
                }
            };

            var results = this.Query(query);

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query(SearchQuery<T> searchQuery)
        {
            var queryable = this.GetQueryable();

            queryable = queryable.Where(result => result.TemplateId == BlogTag.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            queryable = this.ApplyQueries(queryable, searchQuery);
            queryable = this.ApplySorting(queryable, searchQuery.Sorts);
            queryable = this.ApplyPaging(queryable, searchQuery.Paging);

            var results = this.GetResults(queryable);

            return results.Hits.Select(m => m.Document);
        }
    }
}