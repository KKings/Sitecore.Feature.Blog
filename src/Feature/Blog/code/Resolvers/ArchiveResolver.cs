namespace Sitecore.Feature.Blog.Resolvers
{
    using System.Collections.Generic;
    using Data.Items;
    using Domain;
    using Providers;
    using Repositories;
    using Search.Results;
    using Tokens;

    public class ArchiveResolver : DefaultResolver
    {
        public ArchiveResolver(ITokenService tokenService,
            IContentRespository<SearchResultItem> contentRespository,
            IDatabaseProvider databaseProvider) : base(tokenService, contentRespository, databaseProvider)
        {
            
        }
        
        /// <summary>
        /// Resolves the Context Item to the Blog
        /// </summary>
        /// <param name="blogContext">The Blog Context</param>
        /// <param name="mappedTokens">The Tokens that were mapped to the url</param>
        /// <returns><see cref="Item"/> that represents the blog</returns>
        public override Item Resolve(BlogContext blogContext, IList<MappedToken> mappedTokens)
        {
            return this.DatabaseProvider.Context.GetItem(blogContext.Blog);
        }
    }
}