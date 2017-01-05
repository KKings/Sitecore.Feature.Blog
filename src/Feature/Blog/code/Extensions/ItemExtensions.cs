
namespace Sitecore.Feature.Blog.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data;
    using Data.Fields;
    using Data.Items;
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
                image.Src = MediaManager.GetMediaUrl(field.MediaItem);

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
        /// <returns></returns>
        public static string ItemUrl(this Item item)
        {
            var options = new UrlOptions { AlwaysIncludeServerUrl = true };

            return LinkManager.GetItemUrl(item, options);
        }
    }
}