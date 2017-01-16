namespace Sitecore.Feature.Blog.Events
{
    using System;
    using Data.Items;
    using Diagnostics;
    using Extensions;
    using global::Sitecore.Events;
    using global::Sitecore.Pipelines;
    using Pipelines.BlogGenerateSlug;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogSlugSavingEvent
    {
        /// <summary>
        /// Gets the database
        /// </summary>
        public string Database { get; set; }

        public void OnItemSaving(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            var item = Event.ExtractParameter(args, 0) as Item;

            if (item?.Database == null || !item.IsDerived(BlogSlug.TemplateId) || item.Name == "__Standard Values")
            {
                return;
            }

            if (String.CompareOrdinal(item.Database.Name, this.Database) != 0)
            {
                return;
            }
            
            var slug = item[BlogSlug.SlugFieldId];

            if (!String.IsNullOrEmpty(slug))
            {
                return;
            }

            var slugArgs = new BlogGenerateSlugArgs(item);

            CorePipeline.Run("blog.generateSlug", slugArgs);

            item[BlogSlug.SlugFieldId] = slugArgs.Slug;
        }
    }
}