namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System.Collections.Generic;
    using Items;

    public class RelatedPostListingViewModel
    {
        /// <summary>
        /// Gets the Title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the Posts
        /// </summary>
        public IEnumerable<BlogPostItem> Posts { get; private set; }

        public RelatedPostListingViewModel(string title, IEnumerable<BlogPostItem> posts)
        {
            this.Title = title;
            this.Posts = posts ?? new BlogPostItem[0];
        }
    }
}