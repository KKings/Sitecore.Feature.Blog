namespace Sitecore.Feature.Blog.Resolvers
{
    using System.Collections.Generic;
    using Data.Items;

    public interface IResolverService
    {
        string GetItemUrl(Item item);

        string GetAbstractUrl(IDictionary<string, object> properties);

        MappedResolver Resolve(string url);
    }
}