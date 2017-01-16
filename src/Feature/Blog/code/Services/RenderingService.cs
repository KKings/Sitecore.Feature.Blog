namespace Sitecore.Feature.Blog.Services
{
    using System;
    using Data;
    using Mvc.Presentation;
    using Providers;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class RenderingService : IRenderingService
    {
        /// <summary>
        /// The Database Provider
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        public RenderingService(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Gets the Rendering Title
        /// </summary>
        /// <param name="renderingContext">The Rendering Context</param>
        /// <returns>The Title</returns>
        public virtual string GetTitle(RenderingContext renderingContext)
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

        /// <summary>
        /// Gets the PostsPerPage
        /// </summary>
        /// <param name="renderingContext">The Rendering Context</param>
        /// <returns>The results per page that should be displayed</returns>
        public virtual int PostsPerPage(RenderingContext renderingContext)
        {
            var postsPerPage = renderingContext?.Rendering.Parameters[BlogListingParameters.PostsPerPageFieldName];
            int page;

            if (String.IsNullOrEmpty(postsPerPage) || !Int32.TryParse(postsPerPage, out page))
            {
                return 10;
            }

            return page;
        }
    }
}