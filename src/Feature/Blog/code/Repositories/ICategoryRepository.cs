
namespace Sitecore.Feature.Blog.Repositories
{
    using System.Collections.Generic;
    using Domain;

    public interface ICategoryRepository
    {
        IEnumerable<ICategory> All();
    }
}