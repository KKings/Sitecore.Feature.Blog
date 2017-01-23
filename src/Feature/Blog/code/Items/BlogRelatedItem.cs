namespace Sitecore.Feature.Blog.Items
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Items;
    using Data.Managers;
    using Extensions;
    using Models;

    public class BlogRelatedItem : CustomItem
    {
        public BlogRelatedItem(Item innerItem) : base(innerItem) { }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="BlogRelatedItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator BlogRelatedItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogRelatedListing.TemplateId) ? null : new BlogRelatedItem(item);
        }

        /// <summary>
        /// Gets the Related Posts
        /// </summary>
        public IEnumerable<BlogPostItem> RelatedPostItems => this.InnerItem.FieldListToItems(BlogRelatedListing.RelatedBlogPostsFieldId).Select(item => (BlogPostItem)item);
    }
}