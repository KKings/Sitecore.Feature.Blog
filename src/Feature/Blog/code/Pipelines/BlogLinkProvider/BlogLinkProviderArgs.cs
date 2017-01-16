namespace Sitecore.Feature.Blog.Pipelines.BlogLinkProvider
{
    using Data.Items;
    using Domain;
    using global::Sitecore.Links;
    using global::Sitecore.Pipelines;

    public class BlogLinkProviderArgs : PipelineArgs
    {
        public bool IsResolved { get; set; } = false;

        public string Url { get; set; }

        public virtual BlogContext BlogContext { get; set; }

        public virtual Item Item { get; private set; }

        public virtual UrlOptions Options { get; private set; }

        public BlogLinkProviderArgs(Item item, UrlOptions options)
        {
            this.Item = item;
            this.Options = options;
        }
    }
}