namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    public class PageViewModel
    {
        /// <summary>
        /// Gets if the current page is being currently displayed
        /// </summary>
        public bool IsCurrent { get; set; } = false;

        /// <summary>
        /// Gets the Url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the page number
        /// </summary>
        public int Page { get; set; } = 1;

        public PageViewModel(string url, int page)
        {
            this.Url = url;
            this.Page = page;
        }
    }
}