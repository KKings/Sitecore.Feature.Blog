
namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ContentSearch.Linq;
    using Data;
    using Domain;
    using Search;

    public interface IBlogRepository
    {
        IBlog Get(ID id);

        IBlog Get(string slug);

        IEnumerable<BlogSearchResultItem> All(Expression<Func<BlogSearchResultItem, object>> sorting, bool descending);

        FacetResults Archives();

        IEnumerable<BlogSearchResultItem> Related(IBlog blog);
    }
}