namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using Domain;
    using Items;
    using Search;

    public interface IBlogPostService
    {
        SearchResults<BlogPostItem> Recent(BlogContext context, int display);

        SearchResults<BlogPostItem> All(BlogContext context, int display);

        IEnumerable<Archive> Archives(BlogContext context);

        SearchResults<BlogPostItem> Related(BlogPostItem postItem, int display);
    }
}