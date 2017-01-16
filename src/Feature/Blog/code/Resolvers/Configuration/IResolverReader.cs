namespace Sitecore.Feature.Blog.Resolvers.Configuration
{
    using System.Collections.Generic;

    public interface IResolverReader
    {
        /// <summary>
        /// Gets all configured resolvers
        /// </summary>
        IList<IResolver> Read();
    }
}