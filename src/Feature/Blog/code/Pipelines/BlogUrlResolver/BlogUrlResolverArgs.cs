namespace Sitecore.Feature.Blog.Pipelines.BlogUrlResolver
{
    using Domain;
    using global::Sitecore.Pipelines;
    using Resolvers;

    public class BlogUrlResolverArgs : PipelineArgs
    {
        /// <summary>
        /// Gets the BlogContext
        /// </summary>
        public BlogContext BlogContext { get; }

        /// <summary>
        /// Gets the Resolver
        /// </summary>
        public MappedResolver MappedResolver { get; set; }

        public BlogUrlResolverArgs(BlogContext blogContext)
        {
            this.BlogContext = blogContext;
        }
    }
}