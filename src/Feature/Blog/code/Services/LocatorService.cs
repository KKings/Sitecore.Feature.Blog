﻿// MIT License
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
namespace Sitecore.Feature.Blog.Services
{
    using System.Linq;
    using Buckets.Extensions;
    using Buckets.Managers;
    using Data.Items;
    using Extensions;
    using Models;

    public class LocatorService : ILocatorService
    {
        /// <summary>
        /// Gets the Root Path for the Site
        /// </summary>
        public virtual string RootPath { get; } = Context.Site.RootPath;

        public virtual Item GetParentBlog(Item item)
        {
            if (item.IsDerived(Blog.TemplateId) && item.IsABucket())
            {
                return item;
            }

            return item?.Axes.GetAncestors().FirstOrDefault(BucketManager.IsBucket) ?? item?.Database.GetItem(this.RootPath);         
        }
    }
}