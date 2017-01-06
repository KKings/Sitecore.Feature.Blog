
namespace Sitecore.Feature.Blog.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Domain;
    using Items;
    using Repositories;
    using Search;

    public class BlogService : IBlogService
    {
        private readonly IBlogRepository<BlogSearchResultItem> repository;

        public BlogService(IBlogRepository<BlogSearchResultItem> repository)
        {
            this.repository = repository;
        }

        public virtual IEnumerable<BlogPostItem> All()
        {
            var query = new SearchQuery<BlogSearchResultItem>
            {
                Sorts = new[]
                {
                    new SortExpression<BlogSearchResultItem>(result => result.PublishDate,
                        SortExpression<BlogSearchResultItem>.Sorting.Descending),
                }
            };

            var posts = this.repository.Query(query);

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

        public IEnumerable<BlogPostItem> Related(BlogPostItem postItem)
        {
            var query = new SearchQuery<BlogSearchResultItem>
            {
                Queries = new ExpressionBuilder<BlogSearchResultItem>()
                    .Where(m => m.ItemId != postItem.ID)
                    .And(and => and
                        .IfAny(postItem.Tags.Any(), postItem.Tags.Select(item => item.ID), (result, id) => result.Tags.Contains(id))
                        .IfOrAny(postItem.Categories.Any(), postItem.Categories.Select(item => item.ID),
                        (result, id) => result.Categories.Contains(id)))
                    .Build(),
                Paging = new Paging
                {
                    Display = 4
                }
            };

            var posts = this.repository.Query(query);

            var items = posts as BlogSearchResultItem[] ?? posts.ToArray();

            return !items.Any() ? new BlogPostItem[0] : items.Select(post => (BlogPostItem)post.GetItem());
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