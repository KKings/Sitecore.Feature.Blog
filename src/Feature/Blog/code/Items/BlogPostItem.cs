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
namespace Sitecore.Feature.Blog.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Items;
    using Data.Managers;
    using Domain;
    using Extensions;
    using Models;

    public class BlogPostItem : CustomItem
    {
        public BlogPostItem(Item innerItem) : base(innerItem) { }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="BlogPostItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator BlogPostItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogPost.TemplateId) ? null : new BlogPostItem(item);
        }

        /// <summary>
        /// Gets the Item Url
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// Gets the Blog Post Title
        /// </summary>
        public string Title => this.InnerItem[BlogPost.PostTitleFieldName];

        /// <summary>
        /// Gets the Summary as Rendered Html
        /// </summary>
        public ImageMedia Thumbnail => this.InnerItem.FieldToImageMedia(BlogPost.ThumbnailFieldId);

        /// <summary>
        /// Gets the Summary as Rendered Html
        /// </summary>
        public ImageMedia FullThumbnail => this.InnerItem.FieldToImageMedia(BlogPost.ThumbnailFieldId);

        /// <summary>
        /// Gets the Summary as Rendered Html
        /// </summary>
        public string Summary => this.InnerItem.FieldToHtml(BlogPost.SummaryFieldId);

        /// <summary>
        /// Gets the Body as Rendered Html
        /// </summary>
        public string Body => this.InnerItem.FieldToHtml(BlogPost.BodyFieldId);

        /// <summary>
        /// Gets the Published Date
        /// </summary>
        public DateTime PublishDate => this.InnerItem.FieldToDateTime(BlogPost.PublishDateFieldId);

        /// <summary>
        /// Gets the Author Items
        /// </summary>
        public IList<AuthorItem> Authors => this.InnerItem.FieldListToItems(BlogPost.AuthorsFieldId).Select(item => (AuthorItem)item).ToArray();

        /// <summary>
        /// Gets the Tag Items
        /// </summary>
        public IList<TagItem> Tags => this.InnerItem.FieldListToItems(BlogPost.TagsFieldId).Select(item => (TagItem)item).ToArray();

        /// <summary>
        /// Gets the Category Items
        /// </summary>
        public IList<CategoryItem> Categories => this.InnerItem.FieldListToItems(BlogPost.CategoriesFieldId).Select(item => (CategoryItem)item).ToArray();

        /// <summary>
        /// Gets the Unique Slug
        /// </summary>
        public string Slug => this.InnerItem[BlogPost.SlugFieldId];
    }
}