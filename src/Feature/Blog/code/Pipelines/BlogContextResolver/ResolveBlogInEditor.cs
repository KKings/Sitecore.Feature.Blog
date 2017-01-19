namespace Sitecore.Feature.Blog.Pipelines.BlogContextResolver
{
    using DependencyInjection;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Models;

    public class ResolveBlogInEditor
    {
        /// <summary>
        /// Gets if the current request is within the Experience Editor
        /// <para>Either Experience Editor or Preview</para>
        /// </summary>
        public virtual bool IsInEditingMode { get { return Context.PageMode.IsExperienceEditor || Context.PageMode.IsPreview; } }

        /// <summary>
        /// Gets the locator service
        /// </summary>
        public virtual ILocatorService LocatorService { get { return ServiceLocator.ServiceProvider.GetService<ILocatorService>();  } }

        public void Process(BlogContextArgs args)
        {
            if (args.ContextItem == null || !this.IsInEditingMode)
            {
                return;
            }

            var blog = this.LocatorService.GetParentBlog(args.ContextItem);

            if (blog == null || !blog.IsDerived(Blog.TemplateId))
            {
                return;
            }

            var context = args.BlogContext;

            context.Blog = blog.ID;
            context.IsWithinBlog = true;

            args.AbortPipeline();
        }
    }
}