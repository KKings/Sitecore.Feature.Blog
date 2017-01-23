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
    using DependencyInjection;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Models;

    public class ResolveBlogInEditor
    {
        /// <summary>
        /// Gets if the current request is within the Experience Editor
        /// <para>Either Experience Editor or Preview</para>
        /// </summary>
        public virtual bool IsInEditingMode { get { return Context.PageMode.IsExperienceEditor || Context.PageMode.IsPreview; } }

        /// <summary>
        /// Gets the locator service
        /// </summary>
        public virtual ILocatorService LocatorService { get { return ServiceLocator.ServiceProvider.GetService<ILocatorService>();  } }

        public void Process(BlogContextArgs args)
        {
            if (args.ContextItem == null || !this.IsInEditingMode)
            {
                return;
            }

            var blog = this.LocatorService.GetParentBlog(args.ContextItem);

            if (blog == null || !blog.IsDerived(Blog.TemplateId))
            {
                return;
            }

            var context = args.BlogContext;

            context.Blog = blog.ID;
            context.IsWithinBlog = true;

            args.AbortPipeline();
        }
    }
}