namespace Sitecore.Feature.Blog.Tokens
{
    using System;
    using System.Linq.Expressions;
    using Search.Results;
    using Data.Items;
    using Domain;
    using Models;

    public class SlugToken : IToken
    {
        /// <summary>
        /// The regex expression to match the individual token
        /// </summary>
        public string Regex { get; } = "[^\\/]+";

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
        public virtual Expression<Func<SearchResultItem, string, bool>> Filter { get { return (result, slug) => result["slug"] == slug ; } }
        
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
                    context.Slug = value;
                };
            }
        }

        /// <summary>
        /// Maps the Token to the Permalink
        /// </summary>
        public virtual Func<Item, object> MapToPermalink { get; } = (item) => item[BlogSlug.SlugFieldId];

        /// <summary>
        /// Adds the value of this token to the <see cref="BlogContext"/>
        /// </summary>
        public virtual Action<BlogContext, object> AddToPropertyBag { get { return (context, value) => context.Properties[this.Token] = value; } }
    }
}