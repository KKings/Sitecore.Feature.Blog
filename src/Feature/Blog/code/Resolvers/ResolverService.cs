// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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
            var args = new BlogAbstractLinkMapperArgs(properties, null);

            CorePipeline.Run("blog.generateAbstractUrl", args);

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