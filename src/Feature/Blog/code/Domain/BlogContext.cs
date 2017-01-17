namespace Sitecore.Feature.Blog.Domain
{
    using System;
    using System.Collections.Generic;
    using Data;

    public class BlogContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets if the current request is within a Blog
        /// </summary>
        public bool IsWithinBlog { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public ID Blog { get; set; } = ID.Null;

        /// <summary>
        /// 
        /// </summary>
        public string PathFromBlog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArchiveYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArchiveMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; } = 1;
    }
}