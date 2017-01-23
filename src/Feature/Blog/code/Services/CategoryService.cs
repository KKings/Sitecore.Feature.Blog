// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Items;
    using Repositories;
    using Search;
    using Search.Results;
    using Models;

    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IContentRespository<CategorySearchResultItem> repository;

        public CategoryService(IContentRespository<CategorySearchResultItem> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all categories based on the <see cref="BlogContext"/>
        /// </summary>
        /// <param name="context">The Blog Context</param>
        /// <returns>List of all categories for the blog</returns>
        public virtual IEnumerable<CategoryItem> All(BlogContext context)
        {
            var query = new SearchQuery<CategorySearchResultItem>
            {
                Queries = new ExpressionBuilder<CategorySearchResultItem>()
                    .IfWhere(context != null, m => m.Paths.Contains(context.Blog))
                    .Build(),
                Filters = new ExpressionBuilder<CategorySearchResultItem>()
                    .Where(result => result.TemplateIds.Contains(BlogCategory.TemplateId))
                    .Where(result => result.Name != "__Standard Values")
                    .Build(),
                Sorts = new[]
                {
                    new SortExpression<CategorySearchResultItem>(result => result.CategoryName)
                }
            };

            var searchResults = this.repository.Query(query);

            return searchResults.Select(result => (CategoryItem)result.Document.GetItem());
        }
    }
}