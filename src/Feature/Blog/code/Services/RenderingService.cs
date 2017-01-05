namespace Sitecore.Feature.Blog.Services
{
    using System;
    using Data;
    using Mvc.Presentation;
    using Providers;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class RenderingService : IRenderingService
    {
        private readonly IDatabaseProvider databaseProvider;

        public RenderingService(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderingContext"></param>
        /// <returns></returns>
        public string GetTitle(RenderingContext renderingContext)
        {
            var itemIdRaw = renderingContext?.Rendering.Parameters[TitleParameters.RenderingTitleFieldName];
            ID itemId;

            if (String.IsNullOrEmpty(itemIdRaw) || !ID.TryParse(itemIdRaw, out itemId))
            {
                return String.Empty;
            }

            var item = this.databaseProvider.Context.GetItem(itemId);

            return item["Phrase"];
        }
    }
}