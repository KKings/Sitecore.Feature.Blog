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
namespace Sitecore.Feature.Blog.Caching
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public sealed class TransientCache : BaseTransientCache
    {
        /// <summary>
        /// Base Implementation of the HttpContext
        /// </summary>
        private readonly HttpContextBase httpContextBase;

        /// <summary>
        /// Items Key within the HttpContext
        /// </summary>
        public string ItemsKey { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Local Dictionary Cache
        /// <para>Takes into account if the items are cleared</para>
        /// </summary>
        internal ConcurrentDictionary<string, object> _localCache => this.httpContextBase?.Items[this.ItemsKey] as ConcurrentDictionary<string, object> ?? new ConcurrentDictionary<string, object>();

        public TransientCache(HttpContextBase httpContextBase) : this(httpContextBase, new List<KeyValuePair<string, object>>()) { }

        public TransientCache(HttpContextBase httpContextBase, IList<KeyValuePair<string, object>> collection)
        {
            this.httpContextBase = httpContextBase;

            // The transient cache can become null if used outside of a web request, lucene/solr indexing
            if (httpContextBase != null)
            {
                this.httpContextBase.Items[this.ItemsKey] = new ConcurrentDictionary<string, object>(collection);
            }
        }

        /// <summary>
        /// Gets a cache entry by a cache key
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache entry or <c>default</c></returns>
        public override T Get<T>(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)}, is null or empty.");
            }

            return this._localCache.ContainsKey(key) ? this._localCache[key] as T : default(T);
        }

        /// <summary>
        /// Sets a cache entry by a cache key
        /// </summary>
        /// <typeparam name="T">Typeof of Cache Entry</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Cache Entry</param>
        public override void Set<T>(string key, T value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)}, is null or empty.");
            }

            this._localCache.AddOrUpdate(key, value, (k, old) => value);

            this.httpContextBase.Items[this.ItemsKey] = this._localCache;
        }

        /// <summary>
        /// Removes a Cache Entry if it exists by key (if exists)
        /// </summary>
        /// <param name="key">Key</param>
        public override void Evict(string key)
        {
            if (!this.Contains(key))
            {
                return;
            }

            object obj;
            this._localCache.TryRemove(key, out obj);
            this.httpContextBase.Items[this.ItemsKey] = this._localCache;
        }

        /// <summary>
        /// Removes all Cache Entries
        /// </summary>
        public override void EvictAll()
        {
            if (this._localCache.Keys.Any())
            {
                this._localCache.Clear();
            }
        }

        /// <summary>
        /// Determines whether the Cache contains the key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns><c>True</c> if the cache contains the key</returns>
        public bool Contains(string key)
        {
            return this._localCache.ContainsKey(key);
        }
    }
}