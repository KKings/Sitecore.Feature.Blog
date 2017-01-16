namespace Sitecore.Feature.Blog.Tokens
{
    using System;
    using System.Linq.Expressions;
    using ContentSearch.SearchTypes;
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