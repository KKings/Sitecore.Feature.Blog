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
        public virtual HttpContextBase HttpContextBase => ServiceLocator.ServiceProvider.GetService<HttpContextBase>();
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

            if (contextResolverArgs.BlogContext.IsWithinBlog)
            {
                this.BlogContextRepository.SaveContext(contextResolverArgs.BlogContext);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public virtual bool IsRequestForPhysicalFile(string filePath)
        {
            return File.Exists(this.HttpContextBase.Server.MapPath(filePath));         
        }
    }
}