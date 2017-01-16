namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Linq.Expressions;
    using ContentSearch.Linq;
    using ContentSearch.SearchTypes;
    using Search;

    public interface IContentRespository<T>: IDisposable where T : SearchResultItem
    {
        ContentSearch.Linq.SearchResults<T> Query(ISearchQuery<T> query);

        FacetResults Facet<TKey>(ISearchQuery<T> query, params Expression<Func<T, TKey>>[] selectors);
    }
}