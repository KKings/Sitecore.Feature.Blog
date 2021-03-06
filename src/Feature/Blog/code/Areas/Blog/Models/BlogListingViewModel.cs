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
namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Items;
    using Search;

    public class BlogListingViewModel
    {
        /// <summary>
        /// Gets the Results
        /// </summary>
        public SearchResults<BlogPostItem> SearchResults { get; }

        /// <summary>
        /// Gets or sets the Paging View Moddel
        /// </summary>
        public PaginationViewModel Paging { get; set; }

        /// <summary>
        /// Gets the Results as View Models
        /// </summary>
        public virtual IEnumerable<BlogViewModel> Posts
        {
            get
            {
                return this.SearchResults.Results.Any()
                    ? this.SearchResults.Results.Select(result => (BlogViewModel)result)
                    : new BlogViewModel[0];
            }
        }

        public BlogListingViewModel(SearchResults<BlogPostItem> results, PaginationViewModel paging)
        {
            this.SearchResults = results ?? new SearchResults<BlogPostItem>(0, new BlogPostItem[0]);
            this.Paging = paging;
        }
    }
}