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
namespace Sitecore.Feature.Blog.Pipelines.BlogLocalDatasource
{
    using System;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Providers;
    using Services;

    public class ReplaceBlogToken
    {
        /// <summary>
        /// The Database Provider
        /// </summary>
        protected virtual IDatabaseProvider DatabaseProvider { get  { return ServiceLocator.ServiceProvider.GetService<IDatabaseProvider>(); } }

        /// <summary>
        /// The Locator Service
        /// </summary>
        protected virtual ILocatorService LocatorService { get { return ServiceLocator.ServiceProvider.GetService<ILocatorService>(); } }

        public void Process(LocalDatasourceArgs args)
        {
            if (String.IsNullOrEmpty(args.Filter) || !args.Filter.StartsWith("$blog"))
            {
                return;
            }

            var item = this.DatabaseProvider.ContentContext.GetItem(args.ItemId);

            if (item == null)
            {
                return;
            }

            var result = this.LocatorService.GetParentBlog(item);

            if (result != null)
            {
                args.Result = result.ID.ToString();
            }
        }
    }
}