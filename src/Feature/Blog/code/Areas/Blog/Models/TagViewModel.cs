namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using Items;

    public class TagViewModel
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Convert an <see cref="TagItem"/> to a <see cref="TagViewModel"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator TagViewModel(TagItem item)
        {
            if (item == null)
            {
                return null;
            }

            var model = new TagViewModel
            {
                Url = item.Url,
                Name = item.TagName
            };

            return model;
        }
    }
}