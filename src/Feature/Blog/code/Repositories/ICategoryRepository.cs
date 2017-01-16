
namespace Sitecore.Feature.Blog.Repositories
{
    using System.Collections.Generic;
    using Search;
    using Search.Results;

    public interface ICategoryRepository<T> where T : CategorySearchResultItem
    {
        IEnumerable<T> All();

        IEnumerable<T> Query(SearchQuery<T> searchQuery);
    }
}