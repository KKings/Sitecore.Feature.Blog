namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using Data.Items;
    using Domain;
    using Items;

    public interface ICategoryService
    {
        IEnumerable<CategoryItem> All(BlogContext context);
    }
}