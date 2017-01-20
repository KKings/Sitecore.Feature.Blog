namespace Sitecore.Feature.Blog.Pipelines.BlogLocalDatasource
{
    using System;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Providers;
    using Services;

    public class ReplaceBlogToken
    {
        /// <summary>
        /// The Database Provider
        /// </summary>
        protected virtual IDatabaseProvider DatabaseProvider { get  { return ServiceLocator.ServiceProvider.GetService<IDatabaseProvider>(); } }

        /// <summary>
        /// The Locator Service
        /// </summary>
        protected virtual ILocatorService LocatorService { get { return ServiceLocator.ServiceProvider.GetService<ILocatorService>(); } }

        public void Process(LocalDatasourceArgs args)
        {
            if (String.IsNullOrEmpty(args.Filter) || !args.Filter.StartsWith("$blog"))
            {
                return;
            }

            var item = this.DatabaseProvider.ContentContext.GetItem(args.ItemId);

            if (item == null)
            {
                return;
            }

            var result = this.LocatorService.GetParentBlog(item);

            if (result != null)
            {
                args.Result = result.ID.ToString();
            }
        }
    }
}