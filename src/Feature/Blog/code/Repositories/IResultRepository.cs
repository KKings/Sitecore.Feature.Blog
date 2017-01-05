namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Linq;
    using ContentSearch.Linq;
    using ContentSearch.SearchTypes;

    public interface IResultRepository : IDisposable
    {
        IQueryable<T> GetQueryable<T>();

        SearchResults<T> GetResults<T>(IQueryable<T> queryable) where T : SearchResultItem;

        FacetResults GetFacetResults<T>(IQueryable<T> queryable) where T : SearchResultItem;
    }
}