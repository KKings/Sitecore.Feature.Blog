namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using Domain;
    using Items;

    public interface IBlogService
    {
        IEnumerable<BlogPostItem> All();

        IEnumerable<Archive> Archives();

        IEnumerable<BlogPostItem> Related(BlogPostItem blog);
    }
}