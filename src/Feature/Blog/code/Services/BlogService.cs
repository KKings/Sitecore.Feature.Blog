
namespace Sitecore.Feature.Blog.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Domain;
    using Items;
    using Repositories;

    public class BlogService : IBlogService
    {
        private readonly IBlogRepository repository;

        public BlogService(IBlogRepository repository)
        {
            this.repository = repository;
        }

        public virtual IEnumerable<BlogPostItem> All()
        {
            var posts = this.repository.All(m => m.PublishDate, true);

            return posts.Select(post => (BlogPostItem)post.GetItem());
        }

        public virtual IEnumerable<Archive> Archives()
        {
            var archives = this.repository.Archives();

            if (!archives.Categories.Any())
            {
                return new Archive[0];
            }

            var archive = archives.Categories.First();

            return archive.Values
                          .Where(x => x?.Name != null)
                          .Select(a =>
                          {
                              var separated = a.Name.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                              var name = $"{this.GetMonthName(separated[0])} {separated[1]}";

                              return new Archive
                              {
                                  Title = $"{name} ({a.AggregateCount})",
                                  Url = $"/{separated[1]}/{separated[0]}"
                              };
                          });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        public virtual string GetMonthName(string digit)
        {
            int number;

            return Int32.TryParse(digit, out number) ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(number) : "";
        }
    }
}