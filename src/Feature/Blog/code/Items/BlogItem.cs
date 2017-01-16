namespace Sitecore.Feature.Blog.Items
{
    using Data.Items;
    using Data.Managers;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogItem : CustomItem
    {
        public BlogItem(Item innerItem) : base(innerItem) { }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="BlogItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator BlogItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(Blog.TemplateId) ? null : new BlogItem(item);
        }
    }
}