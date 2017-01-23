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
        /// Index Lookup
        /// </summary>
        public virtual IDictionary<string, string> IndexLookup => new Dictionary<string, string> { { "master", "sitecore_master_index" }, { "web", "sitecore_web_index" } };

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