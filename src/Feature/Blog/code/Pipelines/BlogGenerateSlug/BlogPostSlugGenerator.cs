namespace Sitecore.Feature.Blog.Pipelines.BlogGenerateSlug
{
    using System;
    using Extensions;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogPostSlugGenerator : SlugGenerator
    {
        public void Process(BlogGenerateSlugArgs args)
        {
            if (!args.Item.IsDerived(BlogPost.TemplateId))
            {
                return;
            }

            var slug = this.GenerateSlug(args.Item.Name);

            if (!String.IsNullOrEmpty(slug))
            {
                args.Slug = slug;
                args.AbortPipeline();
            }
        }
    }
}