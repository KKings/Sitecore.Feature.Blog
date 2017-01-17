namespace Sitecore.Feature.Blog.Items
{
    using Data.Items;
    using Data.Managers;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class PageHeaderItem : CustomItem
    {
        public PageHeaderItem(Item innerItem) : base(innerItem) { }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="PageHeaderItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator PageHeaderItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(PageHeader.TemplateId) ? null : new PageHeaderItem(item);
        }
    }
}