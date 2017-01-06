namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ContentSearch.Linq;
    using ContentSearch.SearchTypes;
    using Search;

    public interface IResultRepository<T> : IDisposable where T : SearchResultItem
    {
        IQueryable<T> GetQueryable();

        SearchResults<T> GetResults(IQueryable<T> queryable);

        FacetResults GetFacetResults(IQueryable<T> queryable);

        IQueryable<T> ApplyQueries(IQueryable<T> queryable, SearchQuery<T> searchQuery);

        IQueryable<T> ApplyQuery(IQueryable<T> queryable, Expression<Func<T, bool>> filter);

        IQueryable<T> ApplyFilter(IQueryable<T> queryable, Expression<Func<T, bool>> filter);

        IQueryable<T> ApplySorting(IQueryable<T> queryable, IEnumerable<SortExpression<T>> sorts);

        IQueryable<T> ApplyPaging(IQueryable<T> queryable, Paging paging);
    }
}