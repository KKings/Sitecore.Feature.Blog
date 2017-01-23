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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Configuration;

    public class TokenService : ITokenService
    {
        /// <summary>
        /// The configured tokens
        /// </summary>
        private IList<IToken> Tokens { get; }
        
        public TokenService(ITokenReader configurationReader)
        {
            this.Tokens = configurationReader.Read();
        }

        /// <summary>
        /// Finds all tokens within a permalink
        /// </summary>
        /// <param name="permalink">The Permalink</param>
        /// <returns>List of tokens</returns>
        public virtual IList<IToken> FindTokens(string permalink)
        {
            var matches = Regex.Matches(permalink, "([^/]+)");

            if (matches.Count == 0)
            {
                return new IToken[0];
            }

            return 
                matches.Cast<Match>()
                       .Select(match => this.Tokens.FirstOrDefault(t => t.Token == match.Value))
                       .Where(token => token != null)
                       .ToList();
        }

        /// <summary>
        /// Parses the permalink
        /// </summary>
        /// <param name="permalink">The permalink</param>
        /// <returns>List of tokens</returns>
        public virtual IList<IToken> Parse(string permalink)
        {
            return this.FindTokens(permalink);
        }

        /// <summary>
        /// Parses the permalink
        /// </summary>
        /// <param name="permalink">The permalink</param>
        /// <param name="url">The url</param>
        /// <returns>List of tokens</returns>
        public virtual IList<MappedToken> Parse(string permalink, string url)
        {
            var tokens = this.FindTokens(permalink);

            var pattern = this.ToRegex(permalink, tokens);

            var matches = Regex.Matches(url, pattern);

            if (matches.Count == 0)
            {
                return new MappedToken[0];
            }

            var match = matches[0];

            return tokens.Select(token => new MappedToken(match.Groups[token.Friendly].Value, token)).ToArray();
        }

        /// <summary>
        /// Generates a regex expression to match tokens found to the permalink 
        /// </summary>
        /// <param name="permalink">The permalink</param>
        /// <param name="tokens">The mapped tokens</param>
        /// <returns>Regex expression used to match a url to the permalink</returns>
        public virtual string ToRegex(string permalink, IList<IToken> tokens)
        {
            if (!tokens.Any())
            {
                return permalink;
            }
            
            var pattern = tokens.Where(token => Regex.Escape(permalink).Contains(token.Token))
                                .Aggregate(permalink, (current, token) => current.Replace(token.Token, $"(?<{token.Friendly}>(?:{token.Regex}))"));

            return $"^{pattern}$";
        }
    }
}