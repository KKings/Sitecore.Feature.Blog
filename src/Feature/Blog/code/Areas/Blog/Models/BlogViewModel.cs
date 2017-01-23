// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Items;

    public class BlogViewModel
    {
        /// <summary>
        /// Gets or sets if the view should display the body
        /// </summary>
        public bool DisplayBody { get; set; } = false;

        /// <summary>
        /// Gets or sets the Post Title
        /// </summary>
        public string PostTitle { get; set; }

        /// <summary>
        /// Gets or sets the Post Summary
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the Post Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the Published Date
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the Post Permalink
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the the Post Thumbnail
        /// </summary>
        public ImageMedia Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the Post Authors
        /// </summary>
        public IList<AuthorViewModel> Authors { get; set; } = new AuthorViewModel[0];

        /// <summary>
        /// Gets or sets the Post Categories
        /// </summary>
        public IList<CategoryViewModel> Categories { get; set; } = new CategoryViewModel[0];

        /// <summary>
        /// Gets or sets the Post Tags
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