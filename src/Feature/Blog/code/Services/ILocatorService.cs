namespace Sitecore.Feature.Blog.Services
{
    using Data.Items;

    public interface ILocatorService
    {
        Item GetParentBlog(Item item);
    }
}