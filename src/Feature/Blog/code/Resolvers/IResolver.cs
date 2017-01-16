namespace Sitecore.Feature.Blog.Resolvers
{
    using System.Collections.Generic;
    using Data;
    using Data.Items;
    using Domain;
    using Tokens;

    public interface IResolver
    {
        string Template { get; set; }

        ID TemplateId { get; }

        List<string> Permalinks { get; set; }

        string GetItemUrl(Item item);

        IList<MappedToken> Tokenize(string url);

        Item Resolve(BlogContext blogContext, IList<MappedToken> tokens);
    }
}
