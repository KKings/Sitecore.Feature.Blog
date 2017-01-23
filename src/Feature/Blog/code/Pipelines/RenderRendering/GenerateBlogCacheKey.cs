// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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