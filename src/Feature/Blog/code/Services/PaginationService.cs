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
    using System.Collections.Generic;
    using Areas.Blog.Models;
    using Repositories;
    using Resolvers;

    public class PaginationService :IPaginationService
    {
        /// <summary>
        /// The Blog Context Repository
        /// </summary>
        private readonly IBlogContextRepository repository;

        /// <summary>
        /// The Resolver Service
        /// </summary>
        private readonly IResolverService resolverService;

        public PaginationService(IBlogContextRepository repository, IResolverService resolverService)
        {
            this.repository = repository;
            this.resolverService = resolverService;
        }

        /// <summary>
        /// Generates the Pages
        /// </summary>
        /// <param name="total">The total results</param>
        /// <param name="display">The number to display</param>
        /// <param name="currentPage">The current page</param>
        /// <returns>List of Pages</returns>
        public IEnumerable<PageViewModel> GeneratePages(int total, int display, int currentPage)
        {
            if (total < display)
            {
                return new PageViewModel[0];
            }

            var blogContext = this.repository.GetContext();
            var viewModels = new List<PageViewModel>();

            var pages = Math.Ceiling((decimal)total / display);
            var properties = new Dictionary<string, object>(blogContext.Properties);

            for (var page = 1; page <= pages; page++)
            {
                properties["$page"] = page;
                
                var url = this.resolverService.GetAbstractUrl(properties);

                var model = new PageViewModel(url, page)
                {
                    IsCurrent = currentPage == page
                };

                viewModels.Add(model);
            }

            return viewModels;
        }
    }
}