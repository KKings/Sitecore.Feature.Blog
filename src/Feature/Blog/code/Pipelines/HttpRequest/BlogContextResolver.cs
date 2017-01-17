namespace Sitecore.Feature.Blog.Pipelines.HttpRequest
{
    using System.IO;
    using System.Web;
    using DependencyInjection;
    using Extensions;
    using global::Sitecore.Pipelines;
    using global::Sitecore.Pipelines.HttpRequest;
    using Microsoft.Extensions.DependencyInjection;
    using Pipelines.BlogContextResolver;
    using Repositories;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogContextResolver : HttpRequestProcessor
    {
        /// <summary>
        /// HttpContext Abstraction
        /// </summary>
        public virtual HttpContextBase HttpContextBase => ServiceLocator.ServiceProvider.GetService<HttpContextBase>();

        /// <summary>
        /// The BlogContext Repository
        /// </summary>
        public virtual IBlogContextRepository BlogContextRepository => ServiceLocator.ServiceProvider.GetService<IBlogContextRepository>();

        public override void Process(HttpRequestArgs args)
        {
            if (args.LocalPath.StartsWith("/sitecore")
                || args.Url.FilePath.StartsWith("/sitecore")
                || this.IsRequestForPhysicalFile(args.Url.FilePath)
                || (Context.Item != null && !Context.Item.IsDerived(Blog.TemplateId)))
            {
                return;
            }

            var contextResolverArgs = new BlogContextArgs(args.Url.FilePath);

            CorePipeline.Run("blog.resolveContext", contextResolverArgs);

            // If we determine tha twe are within a blog, we need to save the context for controllers/views to use later
            if (contextResolverArgs.BlogContext.IsWithinBlog)
            {
                this.BlogContextRepository.SaveContext(contextResolverArgs.BlogContext);
            }
        }

        /// <summary>
        /// Determines if the request is for a file on the file system
        /// </summary>
        /// <param name="filePath">The File Path</param>
        /// <returns><c>true</c> if the request is for a file system file</returns>
        public virtual bool IsRequestForPhysicalFile(string filePath)
        {
            return File.Exists(this.HttpContextBase.Server.MapPath(filePath));         
        }
    }
}