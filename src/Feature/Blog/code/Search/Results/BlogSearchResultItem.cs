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
namespace Sitecore.Feature.Blog.Search.Results
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ContentSearch;
    using ContentSearch.Converters;
    using Data;

    public class BlogSearchResultItem : SearchResultItem
    {
        [IndexField("post_title")]
        public string PostTitle { get; set; }
        
        [IndexField("summary")]
        public string Summary { get; set; }

        [IndexField("body")]
        public string Body { get; set; }

        [IndexField("blog_archive_facet")]
        public string ArchiveFacet { get; set; }

        [IndexField("blog_archive_month")]
        public string ArchiveMonth { get; set; }

        [IndexField("blog_archive_year")]
        public string ArchiveYear { get; set; }

        [IndexField("publish_date")]
        public DateTime PublishDate { get; set; }

        [IndexField("authors")]
        [TypeConverter(typeof(IndexFieldEnumerableConverter))]
        public IList<ID> Authors { get; set; } = new List<ID>();

        [IndexField("categories")]
        [TypeConverter(typeof(IndexFieldEnumerableConverter))]
        public IList<ID> Categories { get; set; } = new List<ID>();

        [IndexField("tags")]
        [TypeConverter(typeof(IndexFieldEnumerableConverter))]
        public IList<ID> Tags { get; set; } = new List<ID>();

        [IndexField("blog_tags_parsed")]
        public IList<string> TagNames { get; set; } = new List<string>();

        [IndexField("blog_categories_parsed")]
        public IList<string> CategoryNames { get; set; } = new List<string>();

        [IndexField("blog_authors_parsed")]
        public IList<string> AuthorNames { get; set; } = new List<string>();

        [IndexField("blog_tags_slugs_parsed")]
        public IList<string> TagSlugs { get; set; } = new List<string>();

        [IndexField("blog_categories_slugs_parsed")]
        public IList<string> CategorySlugs { get; set; } = new List<string>();

        [IndexField("blog_authors_slugs_parsed")]
        public IList<string> AuthorSlugs { get; set; } = new List<string>();
    }
}