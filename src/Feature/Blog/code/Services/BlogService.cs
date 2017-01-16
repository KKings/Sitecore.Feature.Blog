namespace Sitecore.Feature.Blog.Services
{
    using System;
    using System.Linq;
    using Data.Items;
    using Extensions;
    using Items;
    using Providers;
    using Sitecore.Feature.Blog.Feature.Blog;

    public class BlogService : IBlogService
    {
        /// <summary>
        /// The Database Provider
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// The Start Path of the Site
        /// </summary>
        public virtual string StartPath => Context.Site.StartPath;

        public BlogService(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Resolves the Parent Blog based on a Url
        /// </summary>
        /// <param name="url">The Url</param>
        /// <returns>The Parent Blog, can be null.</returns>
        public virtual BlogItem ResolveBlog(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return null;
            }

            var parts = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (!parts.Any())
            {
                return null;
            }

            var temp = parts.ToArray();
            var index = 0;

            Item blog = null;

            while (index != temp.Length)
            {
                var path = temp.Take(index + 1).Aggregate(this.StartPath, (seed, part) => $"{seed}/{part}");
                var item = this.databaseProvider.Context.GetItem(path);

                if (item != null && item.IsDerived(Blog.TemplateId))
                {
                    blog = item;
                    break;
                }

                index++;
            }

            return blog;
        }
    }
}