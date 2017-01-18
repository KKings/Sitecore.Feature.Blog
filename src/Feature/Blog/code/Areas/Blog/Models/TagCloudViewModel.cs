namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    public class TagCloudViewModel
    {
        /// <summary>
        /// Gets or sets the Tag Cloud weight
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// Gets or sets the Tag Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; private set; }

        public TagCloudViewModel(int weight, string name, string url)
        {
            this.Weight = weight;
            this.Name = name;
            this.Url = url;
        }
    }
}