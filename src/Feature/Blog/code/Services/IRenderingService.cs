
namespace Sitecore.Feature.Blog.Services
{
    using Mvc.Presentation;

    public interface IRenderingService
    {
        string GetTitle(RenderingContext renderingContext);
    }
}
