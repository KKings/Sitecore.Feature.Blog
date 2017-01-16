namespace Sitecore.Feature.Blog.Pipelines.BlogContextResolver
{
    using BlogUrlResolver;
    using global::Sitecore.Pipelines;

    public class ResolveContextItem
    {
        public void Process(BlogContextArgs args)
        {
            if (Context.Item != null)
            {
                args.AbortPipeline();
            }

            const string pipeline = "blog.resolveUrl";

            var urlArgs = new BlogUrlResolverArgs(args.BlogContext);

            CorePipeline.Run(pipeline, urlArgs);
        }
    }
}