using System.Collections.Generic;

namespace Sitecore.Feature.Blog.Tokens
{
    public interface ITokenService
    {
        IList<IToken> Parse(string permalink);

        IList<MappedToken> Parse(string permalink, string url);
    }
}