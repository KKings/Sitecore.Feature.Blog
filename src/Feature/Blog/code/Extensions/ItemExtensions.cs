// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
namespace Sitecore.Feature.Blog.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Fields;
    using Data.Items;
    using Data.Managers;
    using global::Sitecore.Links;
    using Resources.Media;
    using Web.UI.WebControls;
    using ImageMedia = Domain.ImageMedia;

    public static class ItemExtensions
    {
        /// <summary>
        /// Renders an Html Field using Sitecore Field Render
        /// </summary>
        /// <param name="item">The Item</param>
        /// <param name="fieldId">The Id of the Field</param>
        /// <returns>Html Markup</returns>
        public static string FieldToHtml(this Item item, ID fieldId) => FieldRenderer.Render(item, fieldId.ToString());

        /// <summary>
        /// Gets a DateTime from a Field
        /// </summary>
        /// <param name="item">The Item</param>
        /// <param name="fieldId">The Id of the Field</param>
        /// <returns>Iso DateTime</returns>
        public static DateTime FieldToDateTime(this Item item, ID fieldId)
        {
            var value = item[fieldId];
            var isoDate = DateUtil.IsoDateToDateTime(item[fieldId]);

            return value.EndsWith("Z", StringComparison.OrdinalIgnoreCase)
                ? DateUtil.ToServerTime(isoDate)
                : isoDate;
        }

        /// <summary>
        /// Gets a <see cref="ImageMedia"/> from a Image Fields
        /// </summary>
        /// <param name="item">The Item</param>
        /// <param name="fieldId">The Id of the Field</param>
        /// <returns>Metadata about the Image</returns>
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
        /// Gets the Items from a Multilist Field
        /// </summary>
        /// <param name="item">The Item</param>
        /// <param name="fieldId">The Id of the Field</param>
        /// <returns>List of Items</returns>
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
        /// Generates the Item Url
        /// </summary>
        /// <param name="item">The Item</param>
        /// <returns>The Url</returns>
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
        /// Checks if the Item is derived from a template
        /// </summary>
        /// <param name="item">The Item</param>
        /// <param name="templateId">The Template Id</param>
        /// <returns><c>true</c> if the item is derived from a template</returns>
        public static bool IsDerived([NotNull] this Item item, [NotNull] ID templateId)
        {
            return TemplateManager.GetTemplate(item).IsDerived(templateId);
        }
    }
}