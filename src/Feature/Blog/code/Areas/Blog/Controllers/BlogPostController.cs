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
        public virtual ActionResult RelatedPostListing()
        {
            var blogPost = (BlogPostItem)Mvc.Presentation.RenderingContext.Current.ContextItem;
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var posts = this.blogPostService.Related(blogPost);

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
                title: title,
                archives: archives.Select(result => new ArchiveViewModel
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
        /// <returns></returns>
        public virtual ActionResult BlogTagsListing()
        {
            var blogContext = this.contextRepository.GetContext();
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);

            var tags = this.tagService.All(blogContext);

            var viewModel = new TagsListingViewModel(
                title: title, 
                tags: tags.Select(tag => new TagViewModel(tag.Url, tag.TagName)));

            return this.View(viewModel);
        }

        /// <summary>
        /// Renders the Blog Categories Listing
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult BlogCategoriesListing()
        {
            var blogContext = this.contextRepository.GetContext();
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);

            var categories = this.categoryService.All(blogContext);

            var viewModel = new CategoryListingViewModel(
                title: title,
                categories: categories.Select(cat => new CategoryViewModel(cat.Url, cat.CategoryName)));

            return this.View(viewModel);
        }
    }
}