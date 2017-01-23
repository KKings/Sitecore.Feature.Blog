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
namespace Sitecore.Feature.Blog.Pipelines.BlogAbstractLinkProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BlogAbstractUrlResolver;
    using Data;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Providers;
    using Repositories;
    using Sitecore.Links;

    public class BlogAbstractUrlMapper
    {
        /// <summary>
        /// Gets the Blog Context Repository
        /// </summary>
        public virtual IBlogContextRepository Repository { get { return ServiceLocator.ServiceProvider.GetService<IBlogContextRepository>(); } }

        /// <summary>
        /// Gets the Database Provider
        /// </summary>
        public virtual IDatabaseProvider DatabaseProvider { get {  return ServiceLocator.ServiceProvider.GetService<IDatabaseProvider>(); } }

        /// <summary>
        /// Gets or sets the permalinks
        /// </summary>
        public List<string> Permalinks { get; set; } = new List<string>();

        public void Process(BlogAbstractLinkMapperArgs args)
        {
            if (!this.Permalinks.Any() || !args.Properties.Any())
            {
                return;
            }

            var context = this.Repository.GetContext();

            if (context == null || context.Blog == ID.Null)
            {
                return;
            }

            var blog = this.DatabaseProvider.Context.GetItem(context.Blog);

            var permalink = this.Permalinks.FirstOrDefault(link => args.Properties.All(prop => link.Contains(prop.Key)));

            if (String.IsNullOrEmpty(permalink))
            {
                return;
            }

            var blogUrl = LinkManager.GetItemUrl(blog, args.Options);
            var path = args.Properties.Keys.Aggregate(permalink, (link, key) => link.Replace(key, $"{args.Properties[key]}"));

            args.IsResolved = true;
            args.Url = $"{blogUrl}{path}";
        }
    }
}