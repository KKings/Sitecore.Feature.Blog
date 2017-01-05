
namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    public class OpenGraphMetadataViewModel
    {
        /// <summary>
        /// Gets the Open Graph Url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the Open Graph Title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the Open Graph Description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the Open Graph Type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets the Open Graph Image Url
        /// </summary>
        public string ImageUrl { get; private set; }

        public OpenGraphMetadataViewModel(string url, string title, string description, string type, string imageUrl)
        {
            this.Url = url;
            this.Title = title;
            this.Description = description;
            this.Type = type;
            this.ImageUrl = imageUrl;
        }
    }
}