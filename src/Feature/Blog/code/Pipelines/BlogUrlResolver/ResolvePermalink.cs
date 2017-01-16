namespace Sitecore.Feature.Blog.Pipelines.BlogUrlResolver
{
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Resolvers;

    public class ResolvePermalink
    {
        public virtual IResolverService ResolverService => ServiceLocator.ServiceProvider.GetService<IResolverService>();

        public void Process(BlogUrlResolverArgs args)
        {
            if (!args.BlogContext.IsWithinBlog)
            {
                return;
            }

            var mappedResolver = this.ResolverService.Resolve(args.BlogContext.PathFromBlog);

            if (mappedResolver != null)
            {
                args.MappedResolver = mappedResolver;
            }
        }
    }
}