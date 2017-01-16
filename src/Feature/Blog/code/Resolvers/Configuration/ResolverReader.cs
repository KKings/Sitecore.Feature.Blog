namespace Sitecore.Feature.Blog.Resolvers.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Abstractions;

    public class ResolverReader : IResolverReader
    {
        /// <summary>
        /// Top Level Configuration Node Path for Caches
        /// </summary>
        private const string Path = "/sitecore/blog/resolvers";

        /// <summary>
        /// Configuration Node Path for each Cache
        /// </summary>
        private const string NodePath = "resolver";

        /// <summary>
        /// Implementation of Base Factory
        /// </summary>
        private readonly BaseFactory baseFactory;

        /// <summary>
        /// Implementation of Base Log
        /// </summary>
        private readonly BaseLog baseLog;

        public ResolverReader(BaseFactory configFactory, BaseLog logger)
        {
            this.baseFactory = configFactory;
            this.baseLog = logger;
        }

        public virtual IList<IResolver> Read()
        {
            var root = this.baseFactory.GetConfigNode(ResolverReader.Path);

            if (root == null)
            {
                this.baseLog.Debug($"Unable to find token configuration at {ResolverReader.Path}", this);
                return new IResolver[0];
            }

            var nodes = root.SelectNodes(ResolverReader.NodePath);

            if (nodes == null || nodes.Count == 0)
            {
                this.baseLog.Debug($"No resolvers have been configured at {ResolverReader.NodePath}", this);
                return new IResolver[0];
            }
            
            return (from XmlNode node in nodes
                    select (IResolver)this.baseFactory.CreateObject(node, true))
                .ToList();
        }
    }
}