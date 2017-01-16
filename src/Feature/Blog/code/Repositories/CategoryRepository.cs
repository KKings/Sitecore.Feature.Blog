namespace Sitecore.Feature.Blog.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Providers;
    using Search;
    using Search.Results;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class CategoryRepository<T> : ContentSearchRepository<T>, ICategoryRepository<T> where T : CategorySearchResultItem
    {
        public CategoryRepository(IIndexProvider indexProvider) : base(indexProvider) { }

        public virtual IEnumerable<T> All()
        {
            var query = new SearchQuery<T>
            {
                Sorts = new[]
                {
                    new SortExpression<T>(result => result.CategoryName, SortExpression<T>.Sorting.Descending)
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

            queryable = queryable.Where(result => result.TemplateId == BlogCategory.TemplateId)
                                 .Where(result => result.Name != "__Standard Values");

            queryable = this.ApplyQueries(queryable, searchQuery);
            queryable = this.ApplySorting(queryable, searchQuery.Sorts);
            queryable = this.ApplyPaging(queryable, searchQuery.Paging);

            var results = this.GetResults(queryable);

            return results.Hits.Select(m => m.Document);
        }
    }
}