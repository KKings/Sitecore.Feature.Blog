namespace Sitecore.Feature.Blog.Pipelines.BlogGenerateSlug
{
    using System;
    using Extensions;
    using Models;

    public class BlogTagSlugGenerator : SlugGenerator
    {
        public void Process(BlogGenerateSlugArgs args)
        {
            if (!args.Item.IsDerived(BlogTag.TemplateId))
            {
                return;
            }

            var slug = this.GenerateSlug(args.Item[BlogTag.TagNameFieldId]);

            if (!String.IsNullOrEmpty(slug))
            {
                args.Slug = slug;
                args.AbortPipeline();
            }
        }
    }
}