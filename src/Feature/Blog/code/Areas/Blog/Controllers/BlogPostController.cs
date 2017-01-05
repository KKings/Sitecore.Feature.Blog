namespace Sitecore.Feature.Blog.Areas.Blog.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Collections;
    using Items;
    using Models;
    using Mvc.Controllers;
    using Repositories;
    using Services;

    public class BlogPostController : Controller
    {
        private readonly IBlogService blogService;
        private readonly ITagRepository tagRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IRenderingService renderingService;

        public BlogPostController(IBlogService blogService, 
            ITagRepository tagRepository, 
            ICategoryRepository categoryRepository,
            IRenderingService renderingService)
        {
            this.blogService = blogService;
            this.tagRepository = tagRepository;
            this.categoryRepository = categoryRepository;
            this.renderingService = renderingService;
        }

        public virtual ActionResult BlogPostDetail()
        {
            var blogPost = (BlogPostItem)Mvc.Presentation.RenderingContext.Current.ContextItem;

            return this.View("~/areas/blog/views/shared/BlogViewModel.cshtml",(BlogViewModel)blogPost);
        }

        public virtual ActionResult BlogPostCategories()
        {
            return this.View();
        }


        public virtual ActionResult BlogPostRelatedPosts()
        {
            return this.View();
        }

        public virtual ActionResult BlogArchivesListing()
        {
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var archives = this.blogService.Archives();

            var viewModel = new ArchivesListingViewModel(
                title: title,
                archives: archives.Select(result => new ArchiveViewModel
                {
                    Title = result.Title,
                    Url = result.Url
                }));

            return this.View(viewModel);
        }

        public virtual ActionResult BlogListing()
        {
            var posts = this.blogService.All();

            var viewModel = new BlogListingViewModel(posts.Select(post => (BlogViewModel)post));

            return this.View(viewModel);
        }

        public virtual ActionResult BlogTagsListing()
        {
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var tags = this.tagRepository.All();
            var viewModel = new TagsListingViewModel(
                title: title, 
                tags: tags.Select(tag => new Pair<string, string>($"/tags/{tag.TagName}", tag.TagName)));

            return this.View(viewModel);
        }

        public virtual ActionResult BlogCategoriesListing()
        {
            var title = this.renderingService.GetTitle(Mvc.Presentation.RenderingContext.CurrentOrNull);
            var categories = this.categoryRepository.All();

            var viewModel = new CategoryListingViewModel(
                title: title,
                categories: categories.Select(cat => new Pair<string, string>($"/category/{cat.CategoryName}", cat.CategoryName)));

            return this.View(viewModel);
        }
    }
}