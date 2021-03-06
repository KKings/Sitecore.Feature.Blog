﻿// MIT License
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ContentSearch.Linq;
    using Data;
    using Domain;
    using Items;
    using Providers;
    using Repositories;
    using Search;
    using Search.Results;
    using Models;

    public class TagService : ITagService
    {
        /// <summary>
        /// The Tag Repository
        /// </summary>
        private readonly IContentRespository<TagSearchResultItem> tagRepository;

        /// <summary>
        /// The Blog Post Repository
        /// </summary>
        private readonly IContentRespository<BlogSearchResultItem> postRepository;

        /// <summary>
        /// The database provider
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        public TagService(IContentRespository<TagSearchResultItem> tagRepository, 
            IContentRespository<BlogSearchResultItem> postRepository,
            IDatabaseProvider databaseProvider)
        {
            this.tagRepository = tagRepository;
            this.postRepository = postRepository;
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Gets all Tags within the BlogContext
        /// </summary>
        /// <returns>List of Tag Items</returns>
        public virtual IEnumerable<TagItem> All(BlogContext context)
        {
            var query = new SearchQuery<TagSearchResultItem>
            {
                Queries = new ExpressionBuilder<TagSearchResultItem>()
                                .IfWhere(context != null, m => m.Paths.Contains(context.Blog))
                                .Build(),
                Filters = new ExpressionBuilder<TagSearchResultItem>()
                                .Where(result => result.TemplateIds.Contains(BlogTag.TemplateId))
                                .Where(result => result.Name != "__Standard Values")
                                .Build(),
                Sorts = new[]
                {
                    new SortExpression<TagSearchResultItem>(result => result.TagName)
                }
            };

            var searchResults = this.tagRepository.Query(query);

            return searchResults.Hits.Select(result => (TagItem)result.Document.GetItem());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual IEnumerable<TagItem> AllCloud(BlogContext context)
        {
            var query = new SearchQuery<BlogSearchResultItem>
            {
                Queries = new ExpressionBuilder<BlogSearchResultItem>()
                    .IfWhere(context != null, m => m.Paths.Contains(context.Blog))
                    .Build(),
                Filters = new ExpressionBuilder<BlogSearchResultItem>()
                    .Where(result => result.TemplateIds.Contains(BlogPost.TemplateId))
                    .Where(result => result.Name != "__Standard Values")
                    .Build()
            };

            var searchResults = this.postRepository.Facet(query, post => post.Tags);

            var facet = searchResults.Categories.FirstOrDefault(m => m.Name == "tags");

            if (facet == null || !facet.Values.Any())
            {
                return new TagItem[0];
            }

            var max = facet.Values.Max(m => m.AggregateCount);
            var min = facet.Values.Min(m => m.AggregateCount);

            return
                facet.Values
                    .Select(f => new { item = (TagItem)this.databaseProvider.Context.GetItem(ID.Parse(f.Name)), facet = f })
                    .Where(f => f.item != null)
                    .Select(
                        tag =>
                        {
                            tag.item.Weight = this.CalculateWeight(max, min, 5, 1, tag.facet);
                            return tag.item;
                        });
        }

        /// <summary>
        /// Calculates the weight of a facet relative to all other facets
        /// </summary>
        /// <param name="max">The max count</param>
        /// <param name="min">The min count</param>
        /// <param name="sizeMax">The maximum the weight can be</param>
        /// <param name="sizeMin">The minimum the weight can be</param>
        /// <param name="facetValue">Raw Facet</param>
        /// <returns>Weight between the <c>max</c> and <c>min</c></returns>
        public virtual int CalculateWeight(int max, int min, int sizeMax, int sizeMin, FacetValue facetValue)
        {
            if (facetValue == null)
            {
                return 0;
            }

            return (int)Math.Ceiling(facetValue.AggregateCount == min ? sizeMin : (facetValue.AggregateCount / (double)max) * (sizeMax - sizeMin) + sizeMin);
        }
    }
}