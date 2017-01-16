namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using Domain;
    using Items;
    using Search;

    public interface IBlogPostService
    {
        SearchResults<BlogPostItem> All(BlogContext context, int display);

        IEnumerable<Archive> Archives(BlogContext context);

        SearchResults<BlogPostItem> Related(BlogPostItem postItem);
    }
}