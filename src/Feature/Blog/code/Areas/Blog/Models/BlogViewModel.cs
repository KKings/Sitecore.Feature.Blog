
namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Managers;
    using Items;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string PostTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ImageMedia Thumbnail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<AuthorViewModel> Authors { get; set; } = new AuthorViewModel[0];

        /// <summary>
        /// 
        /// </summary>
        public IList<CategoryViewModel> Categories { get; set; } = new CategoryViewModel[0];

        /// <summary>
        /// 
        /// </summary>
        public IList<TagViewModel> Tags { get; set; } = new TagViewModel[0];

        /// <summary>
        /// Convert an <see cref="BlogPostItem"/> to a <see cref="BlogViewModel"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator BlogViewModel(BlogPostItem item)
        {
            if (item == null)
            {
                return null;
            }

            var model = new BlogViewModel
            {
                Url = item.Url,
                Body = item.Body,
                Summary = item.Summary,
                PostTitle = item.Title,
                PublishDate = item.PublishDate,
                Thumbnail = item.Thumbnail,
                Authors = item.Authors.Select(author => (AuthorViewModel)author).ToArray(),
                Categories = item.Categories.Select(cat => (CategoryViewModel)cat).ToArray(),
                Tags = item.Tags.Select(tag => (TagViewModel)tag).ToArray()
            };

            return model;
        }
    }
}