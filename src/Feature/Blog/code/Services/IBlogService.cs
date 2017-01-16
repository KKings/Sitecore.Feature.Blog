namespace Sitecore.Feature.Blog.Services
{
    using Items;

    public interface IBlogService
    {
        BlogItem ResolveBlog(string url);
    }
}
