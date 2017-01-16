namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System;
    using System.Collections.Generic;
    using Collections;

    public class TagsListingViewModel
    {
        /// <summary>
        /// Gets the Rendering Title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the Url/Tag Pairs
        /// </summary>
        public IEnumerable<TagViewModel> Tags { get; private set; }

        public TagsListingViewModel(string title, IEnumerable<TagViewModel> tags)
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