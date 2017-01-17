﻿namespace Sitecore.Feature.Blog.Tokens
{
    using System;
    using System.Linq.Expressions;
    using ContentSearch.SearchTypes;
    using Data.Items;
    using Domain;
    using Items;

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