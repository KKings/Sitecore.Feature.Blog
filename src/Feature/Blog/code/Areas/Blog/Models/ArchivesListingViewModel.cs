namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System;
    using System.Collections.Generic;

    public class ArchivesListingViewModel
    {
        public string Title { get; private set; }

        public IEnumerable<ArchiveViewModel> Archives { get; private set; }

        public ArchivesListingViewModel(string title, IEnumerable<ArchiveViewModel> archives)
        {
            if (archives == null)
            {
                throw new ArgumentNullException(nameof(archives));
            }

            this.Title = title;
            this.Archives = archives;
        }
    }
}