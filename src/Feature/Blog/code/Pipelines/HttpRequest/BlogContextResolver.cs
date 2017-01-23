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
    using Models;

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
                || (Context.PageMode.IsNormal && Context.Item != null && !Context.Item.IsDerived(Blog.TemplateId)))
            {
                return;
            }

            var contextResolverArgs = new BlogContextArgs(args.Url.FilePath, Context.Item);

            CorePipeline.Run("blog.resolveContext", contextResolverArgs);

            // If we determine that we are within a blog, we need to save the context for controllers/views to use later
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