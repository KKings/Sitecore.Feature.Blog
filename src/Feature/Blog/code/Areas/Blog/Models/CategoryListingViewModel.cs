namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System;
    using System.Collections.Generic;
    using Collections;

    public class CategoryListingViewModel
    {
        /// <summary>
        /// Gets the Rendering Title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the Url/Tag Pairs
        /// </summary>
        public IEnumerable<CategoryViewModel> Categories { get; private set; }

        public CategoryListingViewModel(string title, IEnumerable<CategoryViewModel> categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            this.Title = title;
            this.Categories = categories;
        }
    }
}