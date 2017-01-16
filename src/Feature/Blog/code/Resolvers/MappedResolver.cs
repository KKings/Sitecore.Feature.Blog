namespace Sitecore.Feature.Blog.Resolvers
{
    using System;
    using System.Collections.Generic;
    using Tokens;

    public class MappedResolver
    {
        /// <summary>
        /// Gets the Mapped Tokens
        /// </summary>
        public IList<MappedToken> Tokens { get; private set; }

        /// <summary>
        /// Gets the Resolver taht resolved the tokens
        /// </summary>
        public IResolver ResolvedBy { get; private set; }

        public MappedResolver(IResolver resolvedBy, IList<MappedToken> tokens)
        {
            if (resolvedBy == null)
            {
                throw new ArgumentNullException(nameof(resolvedBy));
            }

            this.ResolvedBy = resolvedBy;
            this.Tokens = tokens;
        }
    }
}