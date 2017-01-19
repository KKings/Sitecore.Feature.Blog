
namespace Sitecore.Feature.Blog.Items
{
    using Data.Items;
    using Data.Managers;
    using Domain;
    using Extensions;
    using Models;

    public class AuthorItem : CustomItem
    {
        public AuthorItem(Item innerItem) : base(innerItem) {  }

        /// <summary>
        /// Convert an <see cref="Item"/> to a <see cref="AuthorItem"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator AuthorItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);

            return !template.InheritsFrom(BlogAuthor.TemplateId) ? null : new AuthorItem(item);
        }

        /// <summary>
        /// Gets the Item Url
        /// </summary>
        public string Url => this.InnerItem.ItemUrl();

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string Slug => this.InnerItem[BlogAuthor.SlugFieldId];

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string FullName => this.InnerItem[BlogAuthor.FullNameFieldId];

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string Title => this.InnerItem[BlogAuthor.TitleFieldId];

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public string Biography => this.InnerItem.FieldToHtml(BlogAuthor.BiographyFieldId);

        /// <summary>
        /// Gets the Author Title
        /// </summary>
        public ImageMedia ProfileImage => this.InnerItem.FieldToImageMedia(BlogAuthor.ProfileImageFieldId);
    }
}