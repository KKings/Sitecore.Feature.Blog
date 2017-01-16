namespace Sitecore.Feature.Blog.Search.Results
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ContentSearch;
    using ContentSearch.Converters;
    using ContentSearch.SearchTypes;
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