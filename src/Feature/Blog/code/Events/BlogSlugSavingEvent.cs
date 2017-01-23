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
namespace Sitecore.Feature.Blog.Events
{
    using System;
    using Data.Items;
    using Diagnostics;
    using Extensions;
    using Sitecore.Events;
    using Sitecore.Pipelines;
    using Pipelines.BlogGenerateSlug;
    using Models;

    public class BlogSlugSavingEvent
    {
        /// <summary>
        /// Gets the database
        /// </summary>
        public string Database { get; set; }

        public void OnItemSaving(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            var item = Event.ExtractParameter(args, 0) as Item;

            if (item?.Database == null || !item.IsDerived(BlogSlug.TemplateId) || item.Name == "__Standard Values")
            {
                return;
            }

            if (String.CompareOrdinal(item.Database.Name, this.Database) != 0)
            {
                return;
            }
            
            var slug = item[BlogSlug.SlugFieldId];

            if (!String.IsNullOrEmpty(slug))
            {
                return;
            }

            var slugArgs = new BlogGenerateSlugArgs(item);

            CorePipeline.Run("blog.generateSlug", slugArgs);

            item[BlogSlug.SlugFieldId] = slugArgs.Slug;
        }
    }
}