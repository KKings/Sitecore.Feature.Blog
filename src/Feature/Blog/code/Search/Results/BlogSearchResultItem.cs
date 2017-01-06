namespace Sitecore.Feature.Blog.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ContentSearch;
    using ContentSearch.Converters;
    using ContentSearch.SearchTypes;
    using Data;
    using Domain;

    public class BlogSearchResultItem : SearchResultItem, IBlog
    {
        [IndexField("post_title")]
        public string PostTitle { get; set; }
        
        [IndexField("summary")]
        public string Summary { get; set; }

        [IndexField("body")]
        public string Body { get; set; }

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
    }
}