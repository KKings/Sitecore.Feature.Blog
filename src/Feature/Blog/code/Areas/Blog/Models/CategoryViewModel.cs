namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using Items;

    /// <summary>
    /// 
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        public CategoryViewModel(string url, string name)
        {
            this.Url = url;
            this.Name = name;
        }

        /// <summary>
        /// Convert an <see cref="CategoryItem"/> to a <see cref="CategoryViewModel"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator CategoryViewModel(CategoryItem item)
        {
            if (item == null)
            {
                return null;
            }

            var model = new CategoryViewModel(item.Url, item.CategoryName);

            return model;
        }

    }
}