﻿namespace Sitecore.Feature.Blog.Search.ComputedFields
{
    using System.Linq;
    using ContentSearch;
    using ContentSearch.ComputedFields;
    using Extensions;
    using Items;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogAuthorSlugs : IComputedIndexField
    {
        /// <summary>
        /// The field name, set from configuration
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// The return type, set from configuration
        /// </summary>
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = indexable as SitecoreIndexableItem;

            if (item?.Item == null
                || !item.Item.IsDerived(BlogPost.TemplateId))
            {
                return null;
            }

            var blogPost = (BlogPostItem)item.Item;

            var authors = blogPost.Authors.Select(author => author.Slug).ToList();
            
            return authors.Any() ? authors : null;
        }
    }
}