namespace Sitecore.Feature.Blog.Pipelines.BlogLinkProvider
{
    using Data.Items;
    using Domain;
    using Sitecore.Links;
    using Sitecore.Pipelines;

    public class BlogLinkProviderArgs : PipelineArgs
    {
        /// <summary>
        /// Gets or sets if the Blog Link was able to be resolved
        /// </summary>
        public bool IsResolved { get; set; } = false;

        /// <summary>
        /// Gets or sets the Resolved Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the Blog Context
        /// </summary>
        public virtual BlogContext BlogContext { get; set; }

        /// <summary>
        /// Gets or sets the Item to Resolve
        /// </summary>
        public virtual Item Item { get; private set; }

        /// <summary>
        /// Gets or sets the Url Options
        /// </summary>
        public virtual UrlOptions Options { get; private set; }

        public BlogLinkProviderArgs(Item item, UrlOptions options)
        {
            this.Item = item;
            this.Options = options;
        }
    }
}