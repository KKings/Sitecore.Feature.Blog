namespace Sitecore.Feature.Blog.Pipelines.BlogUrlResolver
{
    public class MapToBlogContext
    {
        public void Process(BlogUrlResolverArgs args)
        {
            if (!args.BlogContext.IsWithinBlog || args.MappedResolver == null)
            {
                return;
            }

            foreach (var token in args.MappedResolver.Tokens)
            {
                token.Token.MapToContext(args.BlogContext, token.Value);
            }
        }
    }
}