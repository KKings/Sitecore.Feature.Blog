namespace Sitecore.Feature.Blog.Items
{
    using System;
    using Data.Items;
    using Data.Managers;
    using Extensions;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogPostItem : CustomItem
    {
        public BlogPostItem(Item innerItem) : base(innerItem) { }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="BlogPostItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator BlogPostItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogPost.TemplateId) ? null : new BlogPostItem(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// Gets the Blog Post Title
        /// </summary>
        public string Title => this.InnerItem[BlogPost.TitleFieldId];

        /// <summary>
        /// 
        /// </summary>
        public string Summary => this.InnerItem.FieldToHtml(BlogPost.SummaryFieldId);

        /// <summary>
        /// 
        /// </summary>
        public string Body => this.InnerItem.FieldToHtml(BlogPost.BodyFieldId);

        /// <summary>
        /// 
        /// </summary>
        public DateTime PublishDate => this.InnerItem.FieldToDateTime(BlogPost.PublishDateFieldId);
    }
}