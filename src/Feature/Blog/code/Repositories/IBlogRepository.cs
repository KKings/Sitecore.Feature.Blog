
namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using ContentSearch.Linq;
    using Data;
    using Domain;
    using Search;

    public interface IBlogRepository<T> where T : BlogSearchResultItem
    {
        IBlog Get(ID id);

        IBlog Get(string slug);

        IEnumerable<T> Query(SearchQuery<T> query);

        FacetResults Archives();
    }
}