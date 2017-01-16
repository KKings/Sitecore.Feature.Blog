namespace Sitecore.Feature.Blog.Pipelines.BlogContextResolver
{
    using System;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class ResolveBlog
    {
        /// <summary>
        /// The Blog Service
        /// </summary>
        protected virtual IBlogService BlogService => ServiceLocator.ServiceProvider.GetService<IBlogService>();

        public void Process(BlogContextArgs args)
        {
            var blog = this.BlogService.ResolveBlog(args.FilePath);

            if (blog == null)
            {
                args.AbortPipeline();
                return;
            }

            var context = args.BlogContext;
            context.IsWithinBlog = true;
            context.Blog = blog.ID;
            context.PathFromBlog = this.PathFromBlog(args.FilePath, blog.InnerItem.Paths.FullPath);
        }

        /// <summary>
        /// Gets the Path from the Blog
        /// </summary>
        /// <param name="filePath">Requested filepath</param>
        /// <param name="blogPath">Path to blog</param>
        /// <returns>Path from the blog</returns>
        public virtual string PathFromBlog(string filePath, string blogPath)
        {
            var temp = blogPath.ToLower().Replace(Context.Site.StartPath, String.Empty);
            return filePath.ToLower().Replace(temp, String.Empty);
        }
    }
}