namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public IEnumerable<Pair<string, string>> Tags { get; private set; }

        public TagsListingViewModel(string title, IEnumerable<Pair<string, string>> tags)
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