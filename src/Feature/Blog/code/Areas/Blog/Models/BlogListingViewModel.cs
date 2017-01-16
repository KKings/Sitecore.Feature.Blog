namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Items;
    using Search;

    public class BlogListingViewModel
    {
        /// <summary>
        /// Gets the Results
        /// </summary>
        public SearchResults<BlogPostItem> SearchResults { get; }

        /// <summary>
        /// Gets or sets the Paging View Moddel
        /// </summary>
        public PaginationViewModel Paging { get; set; }

        /// <summary>
        /// Gets the Results as View Models
        /// </summary>
        public virtual IEnumerable<BlogViewModel> Posts { get { return this.SearchResults.Results.Any() ? this.SearchResults.Results.Select(result => (BlogViewModel)result) : new BlogViewModel[0]; } }

        public BlogListingViewModel(SearchResults<BlogPostItem> results, PaginationViewModel paging)
        {
            this.SearchResults = results ?? new SearchResults<BlogPostItem>(0, new BlogPostItem[0]);
            this.Paging = paging;
        }
    }
}