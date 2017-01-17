namespace Sitecore.Feature.Blog.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Domain;
    using Items;
    using Repositories;
    using Resolvers;
    using Search;
    using Search.Results;
    using Sitecore.Feature.Blog.Feature.Blog;
    using StringExtensions;

    public class BlogPostService : IBlogPostService
    {
        /// <summary>
        /// Gets the Repository
        /// </summary>
        private readonly IContentRespository<BlogSearchResultItem> repository;

        /// <summary>
        /// Gets the Resolver Service
        /// </summary>
        private readonly IResolverService resolverService;

        public BlogPostService(IContentRespository<BlogSearchResultItem> repository, 
            IResolverService resolverService)
        {
            this.repository = repository;
            this.resolverService = resolverService;
        }

        /// <summary>
        /// Gets all of the blog posts by the <see cref="BlogContext"/> limited to a display count
        /// </summary>
        /// <param name="context">The Blog Context</param>
        /// <param name="display">The number of posts to limit</param>
        /// <returns>The Results of the query</returns>
        public virtual SearchResults<BlogPostItem> All(BlogContext context, int display)
        {
            var query = new SearchQuery<BlogSearchResultItem>
            {
                Queries = new ExpressionBuilder<BlogSearchResultItem>()
                                .IfWhere(context != null, m => m.Paths.Contains(context.Blog))
                                .IfWhere(context != null && !context.ArchiveYear.IsNullOrEmpty(), m => m.ArchiveYear == context.ArchiveYear)
                                .IfWhere(context != null && !context.ArchiveMonth.IsNullOrEmpty(), m => m.ArchiveMonth == context.ArchiveMonth)
                                .IfWhere(context != null && !context.AuthorName.IsNullOrEmpty(), m => m.AuthorSlugs.Contains(context.AuthorName))
                                .IfWhere(context != null && !context.CategoryName.IsNullOrEmpty(), m => m.CategorySlugs.Contains(context.CategoryName))
                                .IfWhere(context != null && !context.TagName.IsNullOrEmpty(), m => m.TagSlugs.Contains(context.TagName)).Build(),
                Filters = new ExpressionBuilder<BlogSearchResultItem>().Where(m => m.TemplateId == BlogPost.TemplateId).Build(),
                Paging = new Paging
                {
                    PageMode = PageMode.Pager,
                    Page = context?.Page ?? 1,
                    Display = display
                },
                Sorts = new[]
                {
                    new SortExpression<BlogSearchResultItem>(result => result.PublishDate, SortExpression<BlogSearchResultItem>.Sorting.Descending),
                }
            };

            var searchResults = this.repository.Query(query);

            return new SearchResults<BlogPostItem>(searchResults.TotalSearchResults, searchResults.Hits.Select(m => (BlogPostItem)m.Document.GetItem()));
        }

        /// <summary>
        /// Gets the Archives using a Facet
        /// </summary>
        /// <param name="context">The Blog Context</param>
        /// <returns></returns>
        public virtual IEnumerable<Archive> Archives(BlogContext context)
        {
            if (context == null || context.Blog == ID.Null)
            {
                return new Archive[0];
            }

            var searchQuery = new SearchQuery<BlogSearchResultItem>
            {
                Queries = new ExpressionBuilder<BlogSearchResultItem>()
                    .Where(m => m.Paths.Contains(context.Blog))
                    .Where(result => result.ArchiveFacet != "unknown")
                    .Build(),
                Filters = new ExpressionBuilder<BlogSearchResultItem>().Where(m => m.TemplateId == BlogPost.TemplateId).Build()
            };

            var results = this.repository.Facet(searchQuery, m => m.ArchiveFacet);

            if (!results.Categories.Any())
            {
                return new Archive[0];
            }
            
            return results.Categories.First().Values.Where(value => value?.Name != null)
                          .Select(facet => new { values = facet.Name.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries), count = facet.AggregateCount})
                          .Where(facet => facet.values.Length == 2)
                          .Select(facet => new
                          {
                              month = facet.values[0],
                              count = facet.count,
                              year = facet.values[1],
                              monthName = this.GetMonthName(facet.values[0]),
                              url =
                                  this.resolverService.GetAbstractUrl(new Dictionary<string, object> { { "$month", facet.values[0] }, { "$year", facet.values[1] } })
                          })
                          .OrderByDescending(facet => facet.year).ThenByDescending(facet => facet.month)
                          .Select(archive => new Archive(archive.url, $"{archive.monthName} {archive.year} ({archive.count})"));

        }

        /// <summary>
        /// Gets all posts relating to a Blog Post
        /// </summary>
        /// <param name="postItem">The Blog Post</param>
        /// <returns>The Results</returns>
        public virtual SearchResults<BlogPostItem> Related(BlogPostItem postItem)
        {
            var categories = postItem.Categories;
            var tags = postItem.Tags;

            if (!categories.Any() && !tags.Any())
            {
                return new SearchResults<BlogPostItem>(0, new BlogPostItem[0]);
            }
            
            var query = new SearchQuery<BlogSearchResultItem>
            {
                Queries = new ExpressionBuilder<BlogSearchResultItem>()
                    .Where(m => m.ItemId != postItem.ID)
                    .And(and => and
                        .IfAny(tags.Any(), tags.Select(item => item.ID),                (result, id) => result.Tags.Contains(id))
                        .IfOrAny(categories.Any(), categories.Select(item => item.ID),  (result, id) => result.Categories.Contains(id)))
                    .Build(),
                Filters = new ExpressionBuilder<BlogSearchResultItem>().Where(m => m.TemplateId == BlogPost.TemplateId).Build(),
                Paging = new Paging
                {
                    Display = 4
                }
            };

            var searchResults = this.repository.Query(query);

            return new SearchResults<BlogPostItem>(searchResults.TotalSearchResults, searchResults.Hits.Select(m => (BlogPostItem)m.Document.GetItem()));
        }

        /// <summary>
        /// Gets the Month Name using the <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="digit">The month</param>
        /// <returns>The Month Name</returns>
        public virtual string GetMonthName(string digit)
        {
            int number;

            return Int32.TryParse(digit, out number) ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(number) : "";
        }
    }
}