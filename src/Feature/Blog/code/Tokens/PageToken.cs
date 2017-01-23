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
    using Search.Results;
    using Data.Items;
    using Domain;

    public class PageToken : IToken
    {
        public string Regex { get; } = "\\d{1,4}";

        /// <summary>
        /// Gets or sets the Token
        /// <para>Typically set by configuration</para>
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the Friendly Name of this token
        /// <para>Used for finding the results through regex, should not include any special characters</para>
        /// <para>Typically set by configuration</para>
        /// </summary>
        public string Friendly { get; set; }

        /// <summary>
        /// Filters out results based on a value of the token
        /// </summary>
        public virtual Expression<Func<SearchResultItem, string, bool>> Filter { get; } = null;

        /// <summary>
        /// Maps the Token to the <see cref="BlogContext"/>
        /// </summary>
        public virtual Action<BlogContext, string> MapToContext
        {
            get
            {
                return (context, value) =>
                {
                    var page = 1;
                    Int32.TryParse(value, out page);
                    context.Page = page;
                    this.AddToPropertyBag(context, page);
                };
            }
        }

        /// <summary>
        /// Maps the Token to a permalink
        /// </summary>
        public virtual Func<Item, object> MapToPermalink { get; } = null;

        /// <summary>
        /// Adds the value of this token to the <see cref="BlogContext"/>
        /// </summary>
        public virtual Action<BlogContext, object> AddToPropertyBag { get { return (context, value) => context.Properties[this.Token] = value; } }
    }
}