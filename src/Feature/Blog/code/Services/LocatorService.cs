namespace Sitecore.Feature.Blog.Services
{
    using System.Linq;
    using Buckets.Managers;
    using Data.Items;

    public class LocatorService : ILocatorService
    {
        /// <summary>
        /// Gets the Root Path for the Site
        /// </summary>
        public virtual string RootPath { get; } = Context.Site.RootPath;

        public virtual Item GetParentBlog(Item item)
        {
            return item.Axes.GetAncestors().FirstOrDefault(BucketManager.IsBucket) ?? item.Database.GetItem(this.RootPath);         
        }
    }
}