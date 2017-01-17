namespace Sitecore.Feature.Blog.Repositories
{
    using Caching;
    using Domain;

    public class BlogContextRepository : IBlogContextRepository
    {
        private readonly BaseTransientCache transientCache;

        private const string ContextKey = "blog_context";

        public BlogContextRepository(BaseTransientCache transientCache)
        {
            this.transientCache = transientCache;
        }

        /// <summary>
        /// Save the blog context
        /// </summary>
        /// <param name="blogContext">The Blogcontext</param>
        public virtual void SaveContext(BlogContext blogContext)
        {
            this.transientCache?.Set(BlogContextRepository.ContextKey, blogContext);
        }

        /// <summary>
        /// Gets the blog context
        /// </summary>
        /// <returns>The Blog Context</returns>(
        public virtual BlogContext GetContext()
        {
            return this.transientCache?.Get<BlogContext>(BlogContextRepository.ContextKey) ?? new BlogContext();
        }
    }
}