
namespace Sitecore.Feature.Blog.Pipelines.BlogLinkProvider
{
    using DependencyInjection;
    using global::Sitecore.Links;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class ResolveByContext
    {
        public virtual ILocatorService LocatorService { get { return ServiceLocator.ServiceProvider.GetService<ILocatorService>(); } }

        public void Process(BlogLinkProviderArgs args)
        {
            if (args.IsResolved)
            {
                return;
            }

            var blog = this.LocatorService.GetParentBlog(args.Item);

            if (blog == null)
            {
                return;
            }
            
            var blogUrl = LinkManager.GetItemUrl(blog, args.Options);

            args.IsResolved = true;
            args.Url = $"{blogUrl}";
        }
    }
}