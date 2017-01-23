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
namespace Sitecore.Feature.Blog.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Items;
    using Domain;
    using Extensions;
    using Providers;
    using Repositories;
    using Search;
    using Search.Results;
    using Tokens;

    public class DefaultResolver : IResolver
    {
        /// <summary>
        /// The Token Service
        /// </summary>
        protected readonly ITokenService TokenService;

        /// <summary>
        /// The Content Repository
        /// </summary>
        protected readonly IContentRespository<SearchResultItem> ContentRespository;

        /// <summary>
        /// The Database Provider
        /// </summary>
        protected readonly IDatabaseProvider DatabaseProvider;

        /// <summary>
        /// Gets or sets the Template that this resolver is bound to
        /// <para>Set through configuration</para>
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Gets the Template Id
        /// <para>Takes the <see cref="Template"/> and converts to an <see cref="ID"/></para>
        /// </summary>
        public ID TemplateId
        {
            get
            {
                if (String.IsNullOrEmpty(this.Template))
                {
                    return ID.Null;
                }

                ID value;
                ID.TryParse(this.Template, out value);

                return value;
            }
        }

        /// <summary>
        /// Gets or sets the permalinks that can be resolved using this <see cref="DefaultResolver"/>
        /// </summary>
        public List<string> Permalinks { get; set; } = new List<string>();

        public DefaultResolver(ITokenService tokenService,
            IContentRespository<SearchResultItem> contentRespository,
            IDatabaseProvider databaseProvider)
        {
            this.TokenService = tokenService;
            this.ContentRespository = contentRespository;
            this.DatabaseProvider = databaseProvider;
        }

        /// <summary>
        /// Gets the Item url from the Item
        /// </summary>
        /// <param name="item">The Item</param>
        /// <returns>The Item Url or <see cref="String.Empty"/></returns>
        public virtual string GetItemUrl(Item item)
        {
            var permalink = this.Permalinks.FirstOrDefault();

            if (String.IsNullOrEmpty(permalink))
            {
                return String.Empty;
            }

            var temp = permalink;

            var tokens = this.TokenService.Parse(permalink);

            temp = tokens.Aggregate(temp, (current, token) => current.Replace(token.Token, $"{token.MapToPermalink(item)}"));

            return temp;
        }

        /// <summary>
        /// Tokenizes the Url and maps the tokens to the url
        /// <para>Uses the configured permalinks</para>
        /// </summary>
        /// <param name="url">The Url</param>
        /// <returns>List of <see cref="MappedToken"/> from the Url</returns>
        public virtual IList<MappedToken> Tokenize(string url)
        {
            foreach (var permalink in this.Permalinks)
            {
                var maps = this.TokenService.Parse(permalink, url);

                if (maps.Any())
                {
                    return maps;
                }
            }

            return new MappedToken[0];
        }

        /// <summary>
        /// Resolves the Item by using the Blog Context and Mapped Tokens
        /// </summary>
        /// <param name="blogContext">The Blog Context</param>
        /// <param name="mappedTokens">The mapped tokens</param>
        /// <returns>The Item the path resolves to</returns>
        public virtual Item Resolve(BlogContext blogContext, IList<MappedToken> mappedTokens)
        {
            var builder = new ExpressionBuilder<SearchResultItem>();

            foreach (var map in mappedTokens.Where(map => map.Token.Filter != null))
            {
                builder.Where(map.Token.Filter.Rewrite(map.Value));
            }

            builder.Where(r => r.Paths.Contains(blogContext.Blog))
                   .IfWhere(!String.IsNullOrEmpty(this.Template), r => r.TemplateIds.Contains(this.TemplateId));

            var query = new SearchQuery<SearchResultItem>
            {
                Paging = new Paging
                {
                    Display = 1
                },
                Queries = builder.Build()
            };
            
            var searchResults = this.ContentRespository.Query(query).FirstOrDefault();

            return searchResults?.Document.GetItem();
        }
    }
}