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
    using Data.Items;
    using Data.Managers;
    using Extensions;
    using Models;

    public class CategoryItem : CustomItem
    {
        public CategoryItem(Item innerItem) : base(innerItem) { }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="CategoryItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator CategoryItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogCategory.TemplateId) ? null : new CategoryItem(item);
        }

        /// <summary>
        /// Gets the Item Url
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// The blog category name
        /// </summary>
        public string CategoryName => String.IsNullOrEmpty(this.InnerItem[BlogCategory.CategoryNameFieldId]) ? this.InnerItem.Name : this.InnerItem[BlogCategory.CategoryNameFieldId];

        /// <summary>
        /// Gets the Slug
        /// </summary>
        public string Slug => this.InnerItem[BlogCategory.SlugFieldId];
    }
}