namespace Sitecore.Feature.Blog.Items
{
    using System;
    using Data.Items;
    using Data.Managers;
    using Extensions;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class TagItem : CustomItem
    {
        public TagItem(Item innerItem) : base(innerItem) { }


        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="AuthorItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator TagItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogTag.TemplateId) ? null : new TagItem(item);
        }

        /// <summary>
        /// Gets the Item Url
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// The blog tag name
        /// </summary>
        public string TagName => String.IsNullOrEmpty(this.InnerItem[BlogTag.TagNameFieldName]) ? this.InnerItem.Name : this.InnerItem[BlogTag.TagNameFieldName];
    }
}