namespace Sitecore.Feature.Blog.Domain
{
    public class Archive
    {
        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        public Archive(string url, string title)
        {
            this.Url = url;
            this.Title = title;
        }
    }
}