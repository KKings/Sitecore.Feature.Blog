namespace Sitecore.Feature.Blog.Pipelines.BlogContextResolver
{
    using System;
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

        public BlogContextArgs(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            this.FilePath = filePath;
        }
    }
}