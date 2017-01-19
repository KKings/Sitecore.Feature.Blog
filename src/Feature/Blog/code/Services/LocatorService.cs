namespace Sitecore.Feature.Blog.Services
{
    using System.Linq;
    using Buckets.Extensions;
    using Buckets.Managers;
    using Data.Items;
    using Extensions;
    using Models;

    public class LocatorService : ILocatorService
    {
        /// <summary>
        /// Gets the Root Path for the Site
        /// </summary>
        public virtual string RootPath { get; } = Context.Site.RootPath;

        public virtual Item GetParentBlog(Item item)
        {
            if (item.IsDerived(Blog.TemplateId) && item.IsABucket())
            {
                return item;
            }

            return item?.Axes.GetAncestors().FirstOrDefault(BucketManager.IsBucket) ?? item?.Database.GetItem(this.RootPath);         
        }
    }
}