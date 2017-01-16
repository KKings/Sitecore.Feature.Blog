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