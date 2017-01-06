
namespace Sitecore.Feature.Blog.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data;
    using Data.Fields;
    using Data.Items;
    using Data.Managers;
    using Links;
    using Resources.Media;
    using Web.UI.WebControls;
    using ImageMedia = global::Sitecore.Feature.Blog.Items.ImageMedia;

    public static class ItemExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static string FieldToHtml(this Item item, ID fieldId) => FieldRenderer.Render(item, fieldId.ToString());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static DateTime FieldToDateTime(this Item item, ID fieldId)
        {
            var value = item[fieldId];
            var isoDate = DateUtil.IsoDateToDateTime(item[fieldId]);

            return value.EndsWith("Z", StringComparison.OrdinalIgnoreCase)
                ? DateUtil.ToServerTime(isoDate)
                : isoDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static ImageMedia FieldToImageMedia(this Item item, ID fieldId)
        {
            var field = (ImageField)item.Fields[fieldId];

            var height = 0;
            int.TryParse(field.Height, out height);
            var width = 0;
            int.TryParse(field.Width, out width);
            var hSpace = 0;
            int.TryParse(field.HSpace, out hSpace);
            var vSpace = 0;
            int.TryParse(field.VSpace, out vSpace);

            var image = new ImageMedia
            {
                Alt = field.Alt,
                Border = field.Border,
                Class = field.Class,
                Height = height,
                Width = width,
                Language = field.MediaLanguage,
                MediaId = field.MediaID.Guid
            };

            if (field.MediaItem != null)
            {
                var options = new MediaUrlOptions { AlwaysIncludeServerUrl = true };

                image.Src = MediaManager.GetMediaUrl(field.MediaItem, options);

                var fieldTitle = field.MediaItem.Fields["Title"];

                if (fieldTitle != null)
                {
                    image.Title = fieldTitle.Value;
                }
            }

            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static IEnumerable<Item> FieldListToItems(this Item item, ID fieldId)
        {
            var field = (MultilistField)item.Fields[fieldId];

            if (field == null)
            {
                return new Item[0];
            }

            return field.GetItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ItemUrl(this Item item)
        {
            var options = new UrlOptions { AlwaysIncludeServerUrl = true };

            return LinkManager.GetItemUrl(item, options);
        }

        /// <summary>
        /// Gets the Full Url of the Item
        /// <para>If null, returns empty</para>
        /// </summary>
        /// <param name="item">Media Item to get full url</param>
        /// <returns>Full Media Url</returns>
        public static string GetFullMediaUrl([NotNull] this Item item)
        {
            if (item == null || !item.Paths.IsMediaItem) return String.Empty;

            var mediaUrlOptions = new MediaUrlOptions
            {
                UseItemPath = false,
                AbsolutePath = true,
                AlwaysIncludeServerUrl = true
            };

            return MediaManager.GetMediaUrl(item, mediaUrlOptions);
        }

        /// <summary>
        /// Returns a bool indicating if the item inherits the given template
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="templateGuid">Template Guid</param>
        /// <returns>True if the item is or inherits from a template</returns>
        public static bool InheritsTemplate(this Item item, Guid templateGuid)
        {
            return item.InheritsTemplate(new ID(templateGuid));
        }

        /// <summary>
        /// Returns a bool indicating if the item inherits the given template
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="templateId">Template ID</param>
        /// <returns>True if the item is or inherits from a template</returns>
        public static bool InheritsTemplate(this Item item, ID templateId)
        {
            return item.Template.BaseTemplates.Any(t => t.ID.Equals(templateId)) || item.TemplateID.Equals(templateId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static bool IsDerived([NotNull] this Item item, [NotNull] ID templateId)
        {
            return TemplateManager.GetTemplate(item).IsDerived(templateId);
        }
    }
}