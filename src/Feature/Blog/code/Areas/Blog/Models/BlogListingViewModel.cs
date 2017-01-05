namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System.Collections.Generic;

    public class BlogListingViewModel
    {
        public BlogListingViewModel(IEnumerable<BlogViewModel> blogs)
        {
            this.Blogs = blogs ?? new List<BlogViewModel>();
        }

        /// <summary>
        /// Gets the Blogs
        /// </summary>
        public IEnumerable<BlogViewModel> Blogs { get; private set; } 
    }
}