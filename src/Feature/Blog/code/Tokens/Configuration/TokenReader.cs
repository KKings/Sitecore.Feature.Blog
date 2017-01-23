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
namespace Sitecore.Feature.Blog.Tokens.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Abstractions;
    using ContentSearch.SearchTypes;

    public class TokenReader : ITokenReader
    {
        /// <summary>
        /// Top Level Configuration Node Path for Caches
        /// </summary>
        private const string Path = "/sitecore/blog/tokens";

        /// <summary>
        /// Configuration Node Path for each Cache
        /// </summary>
        private const string NodePath = "token";

        /// <summary>
        /// Implementation of Base Factory
        /// </summary>
        private readonly BaseFactory baseFactory;

        /// <summary>
        /// Implementation of Base Log
        /// </summary>
        private readonly BaseLog baseLog;

        public TokenReader(BaseFactory configFactory, BaseLog logger)
        {
            this.baseFactory = configFactory;
            this.baseLog = logger;
        }

        public virtual IList<IToken> Read()
        {
            var root = this.baseFactory.GetConfigNode(TokenReader.Path);

            if (root == null)
            {
                this.baseLog.Debug($"Unable to find token configuration at {TokenReader.Path}", this);
                return new IToken[0];
            }

            var nodes = root.SelectNodes(TokenReader.NodePath);

            if (nodes == null || nodes.Count == 0)
            {
                this.baseLog.Debug($"No tokens have been configured at {TokenReader.NodePath}", this);
                return new IToken[0];
            }

            return (from XmlNode node in nodes
                    select (IToken)this.baseFactory.CreateObject(node, true))
                .ToList();
        }
    }
}