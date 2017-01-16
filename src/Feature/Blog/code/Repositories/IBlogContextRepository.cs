using Sitecore.Feature.Blog.Domain;

namespace Sitecore.Feature.Blog.Repositories
{
    public interface IBlogContextRepository
    {
        BlogContext GetContext();

        void SaveContext(BlogContext blogContext);
    }
}