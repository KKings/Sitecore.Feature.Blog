namespace Sitecore.Feature.Blog.Links
{
    using System;
    using Data.Items;
    using DependencyInjection;
    using global::Sitecore.Links;
    using global::Sitecore.Pipelines;
    using Microsoft.Extensions.DependencyInjection;
    using Pipelines.BlogLinkProvider;
    using Repositories;

    public class LinkProvider : global::Sitecore.Links.LinkProvider
    {
        /// <summary>
        /// Gets the Blog Context Repository
        /// </summary>
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private IBlogContextRepository Repository
        {
            get { return ServiceLocator.ServiceProvider.GetService<IBlogContextRepository>(); }
        }

        public override string GetItemUrl(Item item, UrlOptions options)
        {
            var args = new BlogLinkProviderArgs(item, options) { BlogContext = this.Repository.GetContext() };

            CorePipeline.Run("blog.linkProvider", args);

            if (args.IsResolved && !String.IsNullOrEmpty(args.Url))
            {
                return args.Url;
            }

            return base.GetItemUrl(item, options);
        }
    }
}