
namespace Sitecore.Feature.Blog.Pipelines.BlogLinkProvider
{
    using System;
    using DependencyInjection;
    using Extensions;
    using global::Sitecore.Links;
    using Microsoft.Extensions.DependencyInjection;
    using Resolvers;
    using Services;
    using Models;

    public class ResolveByItem
    {
        public virtual IResolverService ResolverService { get { return ServiceLocator.ServiceProvider.GetService<IResolverService>(); } }

        public virtual ILocatorService LocatorService { get { return ServiceLocator.ServiceProvider.GetService<ILocatorService>(); } }

        public void Process(BlogLinkProviderArgs args)
        {
            if (args.Item == null)
            {
                return;
            }

            if (!args.Item.IsDerived(BlogPost.TemplateId)
                && !args.Item.IsDerived(BlogTag.TemplateId)
                && !args.Item.IsDerived(BlogCategory.TemplateId)
                && !args.Item.IsDerived(BlogAuthor.TemplateId))
            {
                args.AbortPipeline();
                return;
            }

            var blog = this.LocatorService.GetParentBlog(args.Item);

            if (blog == null)
            {
                return;
            }
            
            var path = this.ResolverService.GetItemUrl(args.Item);

            if (String.IsNullOrEmpty(path))
            {
                return;
            }

            var blogUrl = LinkManager.GetItemUrl(blog, args.Options);

            args.IsResolved = true;
            args.Url = $"{blogUrl}{path}";
        }
    }
}