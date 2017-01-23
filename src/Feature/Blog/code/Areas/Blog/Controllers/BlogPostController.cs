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
namespace Sitecore.Feature.Blog.Areas.Blog.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Items;
    using Models;
    using Repositories;
    using Services;

    public class BlogPostController : Controller
    {
        /// <summary>
        /// The Blog Post Service
        /// </summary>
        private readonly IBlogPostService blogPostService;

        /// <summary>
        /// The Tag Service
        /// </summary>
        private readonly ITagService tagService;

        /// <summary>
        /// The Category Service
        /// </summary>
        private readonly ICategoryService categoryService;

        /// <summary>
        /// The Rendering Service
        /// </summary>
        private readonly IRenderingService renderingService;

        /// <summary>
        /// The Context Repository
        /// </summary>
        private readonly IBlogContextRepository contextRepository;

        /// <summary>
        /// The Pagination Service
        /// </summary>
        private readonly IPaginationService paginationService;

        public BlogPostController(IBlogPostService blogPostService,
            ITagService tagService,
            ICategoryService categoryService,
            IRenderingService renderingService,
            IBlogContextRepository contextRepository,
            IPaginationService paginationService)
        {
            this.blogPostService = blogPostService;
            this.tagService = tagService;
            this.categoryService = categoryService;
            this.renderingService = renderingService;
            this.contextRepository = contextRepository;
            this.paginationService = paginationService;
        }

        /// <summary>
        /// Renders the Blog Post Detail Rendering
        /// </summary>
        public virtual ActionResult BlogPostDetail()
        {
            var blogPost = (BlogPostItem)Mvc.Presentation.RenderingContext.Current.ContextItem;
            var viewModel = (BlogViewModel)blogPost;
            viewModel.DisplayBody = true;

            return this.View("~/areas/blog/views/shared/BlogViewModel.cshtml", viewModel);
        }

        /// <summary>
        /// Renders the Related Post Listing Rendering
        /// </summary>
        public virtual ActionResult BlogRelatedPostListing()
        {
            var relatedItem = (BlogRelatedItem)Mvc.Presentation.RenderingContext.Current.ContextItem;
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);

            var viewModel = new RelatedPostListingViewModel(title, relatedItem?.RelatedPostItems);
            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Dynamic Related Post Listing Rendering
        /// </summary>
        public virtual ActionResult BlogDynamicRelatedPostListing()
        {
            var blogPost = (BlogPostItem)Mvc.Presentation.RenderingContext.Current.ContextItem;
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var posts = this.blogPostService.Related(blogPost, 3);

            var viewModel = new RelatedPostListingViewModel(title, posts.Results);
            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Blog Archives Listing
        /// </summary>
        public virtual ActionResult BlogArchivesListing()
        {
            var blogContext = this.contextRepository.GetContext();
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var archives = this.blogPostService.Archives(blogContext);

            var viewModel = new ArchivesListingViewModel(
                title,
                archives.Select(result => new ArchiveViewModel
                {
                    Title = result.Title,
                    Url = result.Url
                }));

            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Blog Post Listing
        /// </summary>
        public virtual ActionResult BlogListing()
        {
            var blogContext = this.contextRepository.GetContext();
            var postsPerPage = this.renderingService.PostsPerPage(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var results = this.blogPostService.All(blogContext, postsPerPage);
            var pages = this.paginationService.GeneratePages(results.Total, postsPerPage, blogContext.Page);

            var viewModel = new BlogListingViewModel(results, new PaginationViewModel(pages));

            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Blog Tags Listing
        /// </summary>
        public virtual ActionResult BlogTagsListing()
        {
            var blogContext = this.contextRepository.GetContext();
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);

            var tags = this.tagService.All(blogContext);

            var viewModel = new TagsListingViewModel(
                title,
                tags.Select(tag => new TagViewModel(tag.Url, tag.TagName)));

            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Blog Categories Listing
        /// </summary>
        public virtual ActionResult BlogCategoriesListing()
        {
            var blogContext = this.contextRepository.GetContext();
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);

            var categories = this.categoryService.All(blogContext);

            var viewModel = new CategoryListingViewModel(
                title,
                categories.Select(cat => new CategoryViewModel(cat.Url, cat.CategoryName)));

            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Page Header for a Blog Item
        /// </summary>
        public virtual ActionResult BlogPageHeader()
        {
            var pageHeaderItem = (PageHeaderItem)Mvc.Presentation.RenderingContext.Current.ContextItem;

            return this.View(pageHeaderItem);
        }

        /// <summary>
        /// Renders the Tag Cloud
        /// </summary>
        public virtual ActionResult BlogTagCloud()
        {
            var blogContext = this.contextRepository.GetContext();
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);

            var tags = this.tagService.AllCloud(blogContext);

            var viewModel = new TagCloudListingViewModel(title,
                tags.Select(tag => new TagCloudViewModel(tag.Weight, tag.TagName, tag.Url)));

            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Recent Posts Listing
        /// </summary>
        public virtual ActionResult BlogRecentPostsListing()
        {
            var blogContext = this.contextRepository.GetContext();
            var postsPerPage = this.renderingService.PostsPerPage(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var posts = this.blogPostService.Recent(blogContext, postsPerPage);

            var viewModel = new RecentPostsListingViewModel(title, posts.Results);

            return this.View(viewModel);
        }
    }
}