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
    using Data.Items;
    using Data.Managers;
    using Domain;
    using Extensions;
    using Models;

    public class AuthorItem : CustomItem
    {
        public AuthorItem(Item innerItem) : base(innerItem) {  }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="AuthorItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator AuthorItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogAuthor.TemplateId) ? null : new AuthorItem(item);
        }

        /// <summary>
        /// Gets the Item Url
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string Slug => this.InnerItem[BlogAuthor.SlugFieldId];

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string FullName => this.InnerItem[BlogAuthor.FullNameFieldId];

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string Title => this.InnerItem[BlogAuthor.TitleFieldId];

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string Biography => this.InnerItem.FieldToHtml(BlogAuthor.BiographyFieldId);

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public ImageMedia ProfileImage => this.InnerItem.FieldToImageMedia(BlogAuthor.ProfileImageFieldId);
    }
}