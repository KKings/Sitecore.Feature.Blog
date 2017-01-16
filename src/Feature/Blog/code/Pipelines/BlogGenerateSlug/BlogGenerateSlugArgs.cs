namespace Sitecore.Feature.Blog.Pipelines.BlogGenerateSlug
{
    using Data.Items;
    using global::Sitecore.Pipelines;

    public class BlogGenerateSlugArgs : PipelineArgs
    {
        /// <summary>
        /// Gets the Item
        /// </summary>
        public virtual Item Item { get; private set; }

        /// <summary>
        /// Gets or sets the Slug
        /// </summary>
        public virtual string Slug { get; set; }

        public BlogGenerateSlugArgs(Item item)
        {
            this.Item = item;
        }
    }
}