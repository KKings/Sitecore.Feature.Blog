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
namespace Sitecore.Feature.Blog.Services
{
    using System;
    using Data;
    using Mvc.Presentation;
    using Providers;
    using Models;

    public class RenderingService : IRenderingService
    {
        /// <summary>
        /// The Database Provider
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        public RenderingService(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Gets the Rendering Title
        /// </summary>
        /// <param name="renderingContext">The Rendering Context</param>
        /// <returns>The Title</returns>
        public virtual string GetTitle(RenderingContext renderingContext)
        {
            var itemIdRaw = renderingContext?.Rendering.Parameters[TitleParameters.RenderingTitleFieldName];
            ID itemId;

            if (String.IsNullOrEmpty(itemIdRaw) || !ID.TryParse(itemIdRaw, out itemId))
            {
                return String.Empty;
            }

            var item = this.databaseProvider.Context.GetItem(itemId);

            return (item != null) ? item["Phrase"] : String.Empty;
        }

        /// <summary>
        /// Gets the PostsPerPage
        /// </summary>
        /// <param name="renderingContext">The Rendering Context</param>
        /// <returns>The results per page that should be displayed</returns>
        public virtual int PostsPerPage(RenderingContext renderingContext)
        {
            var postsPerPage = renderingContext?.Rendering.Parameters[BlogListingParameters.PostsPerPageFieldName];
            int page;

            if (String.IsNullOrEmpty(postsPerPage) || !Int32.TryParse(postsPerPage, out page))
            {
                return 10;
            }

            return page;
        }
    }
}