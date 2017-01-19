﻿
namespace Sitecore.Feature.Blog.Search.ComputedFields
{
    using ContentSearch;
    using ContentSearch.ComputedFields;
    using Extensions;
    using Items;
    using Models;
    using System.Linq;

    public class BlogCategoryNames : IComputedIndexField
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

            var tags = blogPost.Categories.Select(author => author.CategoryName).ToList();

            return tags.Any() ? tags : null;
        }
    }
}