
namespace Sitecore.Feature.Blog.Providers
{
    using ContentSearch;
    using System;
    using System.Collections.Generic;

    public class DefaultIndexProvider : IIndexProvider
    {
        /// <summary>
        /// The Search Context on the Search Index for Searching the Index
        /// </summary>
        private IProviderSearchContext searchContext;

        /// <summary>
        /// The Search Context on the Search Index for Searching the Index
        /// </summary>
        public virtual IProviderSearchContext SearchContext
        {
            get { return this.searchContext ?? (this.searchContext = this.SearchIndex.CreateSearchContext()); }
        }

        /// <summary>
        /// The Search Index for the Search Manager
        /// </summary>
        private ISearchIndex _searchIndex;

        /// <summary>
        /// The Search Index for the Search Manager
        /// </summary>
        public virtual ISearchIndex SearchIndex
        {
            get
            {
                return this._searchIndex ??
                       (this._searchIndex = ContentSearchManager.GetIndex(this.IndexLookup[this.databaseProvider.Context.Name]));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDictionary<string, string> IndexLookup
            => new Dictionary<string, string> { { "master", "sitecore_master_index" }, { "web", "sitecore_web_index" } };

        /// <summary>
        /// Database Provider Implementation for finding the correct Database
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        public DefaultIndexProvider(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.SearchContext?.Dispose();
            }
        }

        #endregion
    }
}