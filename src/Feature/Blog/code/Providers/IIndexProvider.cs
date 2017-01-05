namespace Sitecore.Feature.Blog.Providers
{
    using System;
    using ContentSearch;

    public interface IIndexProvider : IDisposable
    {
        /// <summary>
        /// Content Search Context
        /// </summary>
        IProviderSearchContext SearchContext { get; }

        /// <summary>
        /// Configured Content Search Index
        /// </summary>
        ISearchIndex SearchIndex { get; }
    }
}