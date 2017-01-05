
namespace Sitecore.Feature.Blog.Providers
{
    using Data;

    public interface IDatabaseProvider
    {
        /// <summary>
        /// Context Database
        /// </summary>
        Database Context { get; }
    }
}