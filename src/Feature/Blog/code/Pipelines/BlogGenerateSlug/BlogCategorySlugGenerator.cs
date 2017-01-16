
namespace Sitecore.Feature.Blog.Pipelines.BlogGenerateSlug
{
    using System;
    using Extensions;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogCategorySlugGenerator : SlugGenerator
    {
        public void Process(BlogGenerateSlugArgs args)
        {
            if (!args.Item.IsDerived(BlogCategory.TemplateId))
            {
                return;
            }

            var slug = this.GenerateSlug(args.Item[BlogCategory.CategoryNameFieldId]);

            if (!String.IsNullOrEmpty(slug))
            {
                args.Slug = slug;
                args.AbortPipeline();
            }
        }
    }
}