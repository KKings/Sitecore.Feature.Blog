namespace Sitecore.Feature.Blog.Pipelines.BlogGenerateSlug
{
    using System;
    using Extensions;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogAuthorSlugGenerator : SlugGenerator
    {
        public void Process(BlogGenerateSlugArgs args)
        {
            if (!args.Item.IsDerived(BlogAuthor.TemplateId))
            {
                return;
            }

            var slug = this.GenerateSlug(args.Item[BlogAuthor.BiographyFieldId]);

            if (!String.IsNullOrEmpty(slug))
            {
                args.Slug = slug;
                args.AbortPipeline();
            }
        }
    }
}