namespace Sitecore.Feature.Blog.Items
{
    using System;
    using Data.Items;
    using Data.Managers;
    using Extensions;
    using Models;

    public class CategoryItem : CustomItem
    {
        public CategoryItem(Item innerItem) : base(innerItem) { }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="CategoryItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator CategoryItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogCategory.TemplateId) ? null : new CategoryItem(item);
        }

        /// <summary>
        /// Gets the Item Url
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// The blog category name
        /// </summary>
        public string CategoryName => String.IsNullOrEmpty(this.InnerItem[BlogCategory.CategoryNameFieldId]) ? this.InnerItem.Name : this.InnerItem[BlogCategory.CategoryNameFieldId];

        /// <summary>
        /// Gets the Slug
        /// </summary>
        public string Slug => this.InnerItem[BlogCategory.SlugFieldId];
    }
}