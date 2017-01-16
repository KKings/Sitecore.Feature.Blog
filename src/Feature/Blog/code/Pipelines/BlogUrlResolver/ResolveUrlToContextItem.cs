namespace Sitecore.Feature.Blog.Pipelines.BlogUrlResolver
{
    public class ResolveUrlToContextItem
    {
        public void Process(BlogUrlResolverArgs args)
        {
            if (!args.BlogContext.IsWithinBlog || args.MappedResolver == null)
            {
                return;
            }

            var item = args.MappedResolver.ResolvedBy.Resolve(args.BlogContext, args.MappedResolver.Tokens);

            if (item != null)
            {
                Context.Item = item;
            }
        }
    }
}