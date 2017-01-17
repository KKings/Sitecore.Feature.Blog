namespace Sitecore.Feature.Blog.Pipelines.BlogContextResolver
{
    using System;
    using Data.Items;
    using Domain;
    using global::Sitecore.Pipelines;

    public class BlogContextArgs : PipelineArgs
    {
        /// <summary>
        /// Gets the requested File path
        /// </summary>
        public virtual string FilePath { get; private set; }

        /// <summary>
        /// Gets the Blog Context
        /// </summary>
        public virtual BlogContext BlogContext { get; } = new BlogContext();

        /// <summary>
        /// Gets the Context Item
        /// <para>Can be null</para>
        /// </summary>
        public virtual Item ContextItem { get; }

        public BlogContextArgs(string filePath, Item contextItem)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            this.FilePath = filePath;
            this.ContextItem = contextItem;
        }
    }
}