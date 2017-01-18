namespace Sitecore.Feature.Blog.Pipelines.RenderRendering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Pipelines.Response.RenderRendering;
    using Repositories;

    public class GenerateBlogCacheKey : RenderRenderingProcessor
    {
        /// <summary>
        /// The Blog Context Repository
        /// </summary>
        private IBlogContextRepository contextRepository { get {  return ServiceLocator.ServiceProvider.GetService<IBlogContextRepository>(); } }

        /// <summary>
        /// The Cache Key stored within the 'extra' parameters
        /// </summary>
        private const string VaryByBlogParameterKey = "VaryByBlog";

        /// <summary>
        /// The Cache Key stored within the 'extra' parameters
        /// </summary>
        private const string VaryByPathParameterKey = "VaryByBlogPath";

        public override void Process(RenderRenderingArgs args)
        {
            if (args.CacheKey == null)
            {
                return;
            }

            if (!args.Cacheable
                || Context.Site == null
                || !args.Rendering.Caching.Cacheable)
            {
                return;
            }

            var context = this.contextRepository.GetContext();
            var parts = new List<string>();

            if (args.Rendering.Parameters.Contains(GenerateBlogCacheKey.VaryByBlogParameterKey))
            {
                if (context != null && context.Blog != ID.Null)
                {
                    var key = $"blog={context.Blog.ToShortID()}";

                    parts.Add(key);
                }
            }

            if (args.Rendering.Parameters.Contains(GenerateBlogCacheKey.VaryByPathParameterKey))
            {
                if (context != null && context.Blog != ID.Null)
                {
                    var key = $"blogpath={context.PathFromBlog}";

                    parts.Add(key);
                }
            }

            if (parts.Any())
            {
                var key = String.Join("&", parts);
                args.CacheKey += $"&{key}";
            }
        }
    }
}