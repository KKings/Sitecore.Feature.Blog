
namespace Sitecore.Feature.Blog.Providers
{
    using Data;

    public interface IDatabaseProvider
    {
        /// <summary>
        /// Context Database
        /// </summary>
        Database Context { get; }

        /// <summary>
        /// Gets the context content database
        /// </summary>
        Database ContentContext { get; }
    }
}