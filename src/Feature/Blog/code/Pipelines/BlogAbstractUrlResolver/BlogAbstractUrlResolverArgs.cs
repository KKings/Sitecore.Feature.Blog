namespace Sitecore.Feature.Blog.Pipelines.BlogAbstractUrlResolver
{
    using System.Collections.Generic;
    using Data;
    using global::Sitecore.Links;
    using global::Sitecore.Pipelines;

    public class BlogAbstractUrlResolverArgs : PipelineArgs
    {
        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets if the Url has been resolved
        /// </summary>
        public bool IsResolved { get; set; } = false;

        /// <summary>
        /// Gets the url property mappings
        /// </summary>
        public IDictionary<string, object> Properties { get; private set; }

        /// <summary>
        /// Get or sets the Url Options
        /// </summary>
        public UrlOptions Options { get; private set; }

        public BlogAbstractUrlResolverArgs(IDictionary<string, object> properties, UrlOptions options)
        {
            this.Properties = properties;
            this.Options = options ?? UrlOptions.DefaultOptions;
        }
    }
}