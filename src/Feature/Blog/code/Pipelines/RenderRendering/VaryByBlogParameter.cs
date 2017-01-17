namespace Sitecore.Feature.Blog.Pipelines.RenderRendering
{
    using System;
    using Data;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Pipelines.Response.RenderRendering;
    using Repositories;

    public class VaryByBlogParameter : RenderRenderingProcessor
    {
        /// <summary>
        /// The Blog Context Repository
        /// </summary>
        private IBlogContextRepository contextRepository { get {  return ServiceLocator.ServiceProvider.GetService<IBlogContextRepository>(); } }

        /// <summary>
        /// The Cache Key stored within the 'extra' parameters
        /// </summary>
        private const string VaryByBlogParameterKey = "VaryByBlog";

        public override void Process(RenderRenderingArgs args)
        {
            if (args.CacheKey == null)
            {
                return;
            }

            if (!args.Cacheable
                || Context.Site == null
                || !args.Rendering.Parameters.Contains(VaryByBlogParameter.VaryByBlogParameterKey)
                || !args.Rendering.Caching.Cacheable
                || !args.Rendering.Caching.VaryByData)
            {
                return;
            }

            var context = this.contextRepository.GetContext();

            if (context == null || context.Blog == ID.Null)
            {
                return;
            }

            var key = $"blog={context.Blog.ToShortID()}&path={context.PathFromBlog}";

            args.CacheKey += $"&{key}";
        }
    }
}