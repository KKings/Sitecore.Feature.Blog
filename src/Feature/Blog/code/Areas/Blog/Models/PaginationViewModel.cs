namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System.Collections.Generic;

    public class PaginationViewModel
    {
        /// <summary>
        /// Gets the pages
        /// </summary>
        public IEnumerable<PageViewModel> Pages { get; set; }

        public PaginationViewModel(IEnumerable<PageViewModel> pages)
        {
            this.Pages = pages;
        }
    }
}