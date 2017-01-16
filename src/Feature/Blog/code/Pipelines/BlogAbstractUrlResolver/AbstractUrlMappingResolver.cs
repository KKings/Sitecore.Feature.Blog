namespace Sitecore.Feature.Blog.Pipelines.BlogAbstractUrlResolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using DependencyInjection;
    using global::Sitecore.Links;
    using Microsoft.Extensions.DependencyInjection;
    using Providers;
    using Repositories;

    public class AbstractUrlMappingResolver
    {
        /// <summary>
        /// Gets the Blog Context Repository
        /// </summary>
        public virtual IBlogContextRepository Repository { get { return ServiceLocator.ServiceProvider.GetService<IBlogContextRepository>(); } }

        /// <summary>
        /// Gets the Database Provider
        /// </summary>
        public virtual IDatabaseProvider DatabaseProvider { get {  return ServiceLocator.ServiceProvider.GetService<IDatabaseProvider>(); } }

        /// <summary>
        /// Gets or sets the permalinks
        /// </summary>
        public List<string> Permalinks { get; set; } = new List<string>();

        public void Process(BlogAbstractUrlResolverArgs args)
        {
            if (!this.Permalinks.Any() || !args.Properties.Any())
            {
                return;
            }

            var context = this.Repository.GetContext();

            if (context == null || context.Blog == ID.Null)
            {
                return;
            }

            var blog = this.DatabaseProvider.Context.GetItem(context.Blog);

            var permalink = this.Permalinks.FirstOrDefault(link => args.Properties.All(prop => link.Contains(prop.Key)));

            if (String.IsNullOrEmpty(permalink))
            {
                return;
            }

            var blogUrl = LinkManager.GetItemUrl(blog, args.Options);
            var path = args.Properties.Keys.Aggregate(permalink, (link, key) => link.Replace(key, $"{args.Properties[key]}"));

            args.IsResolved = true;
            args.Url = $"{blogUrl}{path}";
        }
    }
}