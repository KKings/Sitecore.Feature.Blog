namespace Sitecore.Feature.Blog.Areas.Blog.Controllers
{
    using System.Web.Mvc;
    using Items;
    using Models;
    using Mvc.Controllers;
    using Repositories;

    public class BlogPostController : Controller
    {
        private readonly IBlogRepository blogRepository;

        public BlogPostController(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public ActionResult BlogPostDetail()
        {
            var blogPost = (BlogPostItem)Mvc.Presentation.RenderingContext.Current.ContextItem;

            var viewModel = new BlogPostDetailViewModel
            {
                Title   = blogPost.Title, 
                Body    = blogPost.Body
            };

            return this.View(viewModel);
        }

        public ActionResult BlogPostCategories()
        {
            return this.View();
        }


        public ActionResult BlogPostRelatedPosts()
        {
            return this.View();
        }

        public ActionResult BlogArchivesListing()
        {
            return this.View();
        }

        public ActionResult BlogListing()
        {
            var blogs = this.blogRepository.All();

            var viewModel = new BlogListingViewModel(blogs);

            return this.View(viewModel);
        }

        public ActionResult BlogTagsListing()
        {
            return this.View();
        }

        public ActionResult BlogCategoriesListing()
        {
            return this.View();
        }
    }
}