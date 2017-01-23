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
    using System.Linq;
    using Data.Items;
    using Extensions;
    using Items;
    using Providers;
    using Models;

    public class BlogService : IBlogService
    {
        /// <summary>
        /// The Database Provider
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// The Start Path of the Site
        /// </summary>
        public virtual string StartPath => Context.Site.StartPath;

        public BlogService(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Resolves the Parent Blog based on a Url
        /// </summary>
        /// <param name="url">The Url</param>
        /// <returns>The Parent Blog, can be null.</returns>
        public virtual BlogItem ResolveBlog(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return null;
            }

            var parts = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (!parts.Any())
            {
                return null;
            }

            var temp = parts.ToArray();
            var index = 0;

            Item blog = null;

            while (index != temp.Length)
            {
                var path = temp.Take(index + 1).Aggregate(this.StartPath, (seed, part) => $"{seed}/{part}");
                var item = this.databaseProvider.Context.GetItem(path);

                if (item != null && item.IsDerived(Blog.TemplateId))
                {
                    blog = item;
                    break;
                }

                index++;
            }

            return blog;
        }
    }
}