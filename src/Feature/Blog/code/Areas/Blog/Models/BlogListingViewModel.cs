namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System.Collections.Generic;
    using Domain;

    public class BlogListingViewModel
    {
        public BlogListingViewModel(IEnumerable<IBlog> blogs)
        {
            this.Blogs = blogs ?? new List<IBlog>();
        }

        /// <summary>
        /// Gets the Blogs
        /// </summary>
        public IEnumerable<IBlog> Blogs { get; private set; }
    }
}