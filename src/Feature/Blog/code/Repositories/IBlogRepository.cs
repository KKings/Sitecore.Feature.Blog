
namespace Sitecore.Feature.Blog.Repositories
{
    using System.Collections.Generic;
    using Data;
    using Domain;

    public interface IBlogRepository
    {
        IBlog Get(ID id);

        IBlog Get(string slug);

        IEnumerable<IBlog> All();

        IEnumerable<IBlog> Related(IBlog blog);
    }
}