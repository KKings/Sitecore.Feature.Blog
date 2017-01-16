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