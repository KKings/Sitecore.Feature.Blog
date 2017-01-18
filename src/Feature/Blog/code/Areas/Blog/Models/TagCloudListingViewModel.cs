namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System;
    using System.Collections.Generic;

    public class TagCloudListingViewModel
    {
        /// <summary>
        /// Gets the Rendering Title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the Url/Tag Pairs
        /// </summary>
        public IEnumerable<TagCloudViewModel> Tags { get; private set; }

        public TagCloudListingViewModel(string title, IEnumerable<TagCloudViewModel> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            this.Title = title;
            this.Tags = tags;
        }
    }
}