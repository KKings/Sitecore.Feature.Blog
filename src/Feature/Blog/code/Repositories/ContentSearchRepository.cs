
namespace Sitecore.Feature.Blog.Repositories
{
    using System;
    using System.Linq;
    using ContentSearch.Linq;
    using ContentSearch.SearchTypes;
    using Providers;

    public abstract class ContentSearchRepository : IResultRepository
    {
        protected readonly IIndexProvider IndexProvider;

        protected ContentSearchRepository(IIndexProvider indexProvider)
        {
            this.IndexProvider = indexProvider;
        }

        public virtual IQueryable<T> GetQueryable<T>() => this.IndexProvider.SearchContext.GetQueryable<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public virtual SearchResults<T> GetResults<T>(IQueryable<T> queryable) where T : SearchResultItem
        {
            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable));
            }

            return queryable.GetResults();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public virtual FacetResults GetFacetResults<T>(IQueryable<T> queryable) where T : SearchResultItem
        {
            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable));
            }

            return queryable.GetFacets();
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
                this.IndexProvider?.Dispose();
            }
        }

        #endregion
    }
}