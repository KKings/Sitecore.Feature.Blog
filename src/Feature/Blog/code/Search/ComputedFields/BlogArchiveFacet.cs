namespace Sitecore.Feature.Blog.Search.ComputedFields
{
    using System;
    using ContentSearch;
    using ContentSearch.ComputedFields;
    using Extensions;
    using Items;
    using Models;

    public class BlogArchiveFacet : IComputedIndexField
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

            return blogPost.PublishDate != DateTime.MinValue
                ? $"{blogPost.PublishDate.Month} {blogPost.PublishDate.Year}"
                : "Unknown";
        }
    }
}