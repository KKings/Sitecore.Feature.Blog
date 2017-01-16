namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ContentSearch.Linq;
    using ContentSearch.SearchTypes;
    using Data;
    using Search;

    public interface IBlogRepository<T> where T : SearchResultItem
    {
        T Get(ID id);

        T Get(string slug);

        ISearchQuery<T> MakeQuery();

        IEnumerable<T> Query(ISearchQuery<T> query);

        FacetResults Archives<TKey>(ISearchQuery<T> query, Expression<Func<T, TKey>> keySelector);
    }
}