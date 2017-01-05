namespace Sitecore.Feature.Blog.Providers
{
    using Abstractions;
    using Configuration;
    using Data;

    public class DefaultDatabaseProvider : IDatabaseProvider
    {
        /// <summary>
        /// Default Database Name if there is no Context
        /// </summary>
        public virtual string DefaultDatabaseName => "web";

        /// <summary>
        /// Context Database for the Index
        /// </summary>
        public virtual Database Context
        {
            get { return global::Sitecore.Context.Database ?? this.BaseFactory.GetDatabase(this.DefaultDatabaseName); }
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