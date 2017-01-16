namespace Sitecore.Feature.Blog.Tokens
{
    using System;
    using Domain;

    public class AuthorToken : SlugToken
    {
        /// <summary>
        /// Maps the Token to the <see cref="BlogContext"/>
        /// </summary>
        public override Action<BlogContext, string> MapToContext
        {
            get
            {
                return (context, value) =>
                {
                    this.AddToPropertyBag(context, value);
                    context.AuthorName = value;
                };
            }
        }
    }
}