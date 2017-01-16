using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sitecore.ContentSearch.SearchTypes;

namespace Sitecore.Feature.Blog.Search
{
    public interface ISearchQuery<T> where T : SearchResultItem
    {
        Expression<Func<T, bool>> Filters { get; set; }
        Paging Paging { get; set; }
        Expression<Func<T, bool>> Queries { get; set; }
        IEnumerable<SortExpression<T>> Sorts { get; set; }
    }
}