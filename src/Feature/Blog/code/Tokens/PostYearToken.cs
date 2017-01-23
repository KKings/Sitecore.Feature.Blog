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
namespace Sitecore.Feature.Blog.Tokens
{
    using System;
    using System.Linq.Expressions;
    using Data.Items;
    using Domain;
    using Items;
    using Search.Results;

    public class PostYearToken : IToken
    {
        /// <summary>
        /// The regex expression to match the individual token
        /// </summary>
        public string Regex { get; } = "\\d{4}";

        /// <summary>
        /// The Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The Friendly Name of the token
        /// </summary>
        public string Friendly { get; set; }

        /// <summary>
        /// Filters the Result of a query by the value of the token
        /// </summary>
        public virtual Expression<Func<SearchResultItem, string, bool>> Filter { get; } =
            (item, value) => item["blog_archive_year"] == value;

        /// <summary>
        /// Maps the Token to the <see cref="BlogContext"/>
        /// </summary>
        public virtual Action<BlogContext, string> MapToContext
        {
            get
            {
                return (context, value) =>
                {
                    this.AddToPropertyBag(context, value);
                    context.ArchiveYear = value;
                };
            }
        }

        /// <summary>
        /// Maps the Token to the Permalink
        /// </summary>
        public virtual Func<Item, object> MapToPermalink { get; } = item => ((BlogPostItem)item).PublishDate.Year;

        /// <summary>
        /// Adds the value of this token to the <see cref="BlogContext"/>
        /// </summary>
        public virtual Action<BlogContext, object> AddToPropertyBag
        {
            get { return (context, value) => context.Properties[this.Token] = value; }
        }
    }
}