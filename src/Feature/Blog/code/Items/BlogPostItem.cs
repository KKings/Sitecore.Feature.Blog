namespace Sitecore.Feature.Blog.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Items;
    using Data.Managers;
    using Extensions;
    using Search;
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
        /// Gets the Item Url
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// Gets the Blog Post Title
        /// </summary>
        public string Title => this.InnerItem[BlogPost.PostTitleFieldName];

        /// <summary>
        /// Gets the Summary as Rendered Html
        /// </summary>
        public ImageMedia Thumbnail => this.InnerItem.FieldToImageMedia(BlogPost.ThumbnailFieldId);

        /// <summary>
        /// Gets the Summary as Rendered Html
        /// </summary>
        public string Summary => this.InnerItem.FieldToHtml(BlogPost.SummaryFieldId);

        /// <summary>
        /// Gets the Body as Rendered Html
        /// </summary>
        public string Body => this.InnerItem.FieldToHtml(BlogPost.BodyFieldId);

        /// <summary>
        /// Gets the Published Date
        /// </summary>
        public DateTime PublishDate => this.InnerItem.FieldToDateTime(BlogPost.PublishDateFieldId);

        /// <summary>
        /// 
        /// </summary>
        public IList<AuthorItem> Authors => this.InnerItem.FieldListToItems(BlogPost.AuthorsFieldId).Select(item => (AuthorItem)item).ToArray();

        /// <summary>
        /// 
        /// </summary>
        public IList<TagItem> Tags => this.InnerItem.FieldListToItems(BlogPost.TagsFieldId).Select(item => (TagItem)item).ToArray();

        /// <summary>
        /// 
        /// </summary>
        public IList<CategoryItem> Categories => this.InnerItem.FieldListToItems(BlogPost.CategoriesFieldId).Select(item => (CategoryItem)item).ToArray();
    }
}