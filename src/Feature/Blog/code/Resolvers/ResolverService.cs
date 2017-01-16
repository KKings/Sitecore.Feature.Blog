namespace Sitecore.Feature.Blog.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Configuration;
    using Data.Items;
    using Extensions;
    using global::Sitecore.Pipelines;
    using Pipelines.BlogAbstractUrlResolver;

    public class ResolverService : IResolverService
    {
        /// <summary>
        /// Gets the configured <see cref="IResolver"/>
        /// </summary>
        public virtual IList<IResolver> Resolvers { get; }

        public ResolverService(IResolverReader reader)
        {
            this.Resolvers = reader.Read() ?? new IResolver[0];
        }

        /// <summary>
        /// Gets the Item Url by calling the <see cref="IResolver.GetItemUrl"/> of the resolver that the item is derived from
        /// </summary>
        /// <param name="item">The Item</param>
        /// <returns>The Url</returns>
        public virtual string GetItemUrl(Item item)
        {
            var resolver = this.Resolvers.FirstOrDefault(r => item.IsDerived(r.TemplateId));

            return resolver?.GetItemUrl(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual string GetAbstractUrl(IDictionary<string, object> properties)
        {
            // Null here indincates that default options will be used
            var args = new BlogAbstractUrlResolverArgs(properties, null);

            CorePipeline.Run("blog.resolveAbstractUrl", args);

            return args.IsResolved ? args.Url : String.Empty;
        }

        public virtual MappedResolver Resolve(string url)
        {
            return
                (this.Resolvers.Select(resolver => new { resolver, tokens = resolver.Tokenize(url) })
                     .Where(@t => @t.tokens.Any())
                     .Select(@t => new MappedResolver(@t.resolver, @t.tokens)))
                .FirstOrDefault();
        }
    }
}