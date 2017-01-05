namespace Sitecore.Feature.Blog.Items
{
    using System;
    using Globalization;

    public class ImageMedia
    {
        /// <summary>
        /// Gets or sets the Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Alt
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Gets or sets the Border
        /// </summary>
        public string Border { get; set; }

        /// <summary>
        /// Gets or sets the Class
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the MediaId
        /// </summary>
        public Guid MediaId { get; set; }

        /// <summary>
        /// Gets or sets the Src
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// Gets or sets the Media Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Language
        /// </summary>
        public Language Language { get; set; }
    }
}