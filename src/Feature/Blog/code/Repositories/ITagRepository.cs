namespace Sitecore.Feature.Blog.Repositories
{
    using System.Collections.Generic;
    using ContentSearch.SearchTypes;
    using Domain;
    using Search;

    public interface ITagRepository<T> where T : SearchResultItem
    {
        IEnumerable<T> All();

        IEnumerable<T> Query(SearchQuery<T> searchQuery);
    }
}