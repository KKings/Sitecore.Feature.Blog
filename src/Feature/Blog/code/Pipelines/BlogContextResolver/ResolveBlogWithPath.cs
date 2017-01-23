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
namespace Sitecore.Feature.Blog.Pipelines.BlogContextResolver
{
    using System;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class ResolveBlogWithPath
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