namespace Sitecore.Feature.Blog.Tokens
{
    using System;

    public class MappedToken
    {
        /// <summary>
        /// Gets the parsed value of the Token
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the original token that provided the parsing
        /// </summary>
        public IToken Token { get; private set; }

        public MappedToken(string value, IToken token)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            this.Value = value;
            this.Token = token;
        }
    }
}