namespace Sitecore.Feature.Blog.Providers
{
    using Abstractions;
    using Data;

    public class DefaultDatabaseProvider : IDatabaseProvider
    {
        /// <summary>
        /// Default Database Name if there is no Context
        /// </summary>
        public virtual string DefaultDatabaseName => "web";

        /// <summary>
        /// Default Database Name if there is no Context
        /// </summary>
        public virtual string DefaultContentDatabaseName => "master";

        /// <summary>
        /// Context Database
        /// </summary>
        public virtual Database Context
        {
            get { return global::Sitecore.Context.Database ?? this.BaseFactory.GetDatabase(this.DefaultDatabaseName); }
        }

        /// <summary>
        /// Context Content Database
        /// </summary>
        public virtual Database ContentContext
        {
            get { return global::Sitecore.Context.ContentDatabase ?? this.BaseFactory.GetDatabase(this.DefaultContentDatabaseName); }
        }

        /// <summary>
        /// Default Configuration Factory
        /// </summary>
        public readonly BaseFactory BaseFactory;

        public DefaultDatabaseProvider(BaseFactory defaultFactory)
        {
            this.BaseFactory = defaultFactory;
        }
    }
}