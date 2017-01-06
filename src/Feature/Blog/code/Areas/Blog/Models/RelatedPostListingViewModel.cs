namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System.Collections.Generic;
    using Items;

    public class RelatedPostListingViewModel
    {
        public string Title { get; private set; }

        public IEnumerable<BlogPostItem> Posts { get; private set; }

        public RelatedPostListingViewModel(string title, IEnumerable<BlogPostItem> posts)
        {
            this.Title = title;
            this.Posts = posts ?? new BlogPostItem[0];
        }
    }
}