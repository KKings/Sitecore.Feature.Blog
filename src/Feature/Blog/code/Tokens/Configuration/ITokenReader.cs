namespace Sitecore.Feature.Blog.Tokens.Configuration
{
    using System.Collections.Generic;
    using ContentSearch.SearchTypes;

    public interface ITokenReader
    {
        /// <summary>
        /// Gets all configured tokens
        /// </summary>
        IList<IToken> Read();
    }
}