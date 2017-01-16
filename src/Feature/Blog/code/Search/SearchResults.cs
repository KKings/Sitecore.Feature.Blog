namespace Sitecore.Feature.Blog.Search
{
    using System.Collections.Generic;
    using Data.Items;

    public class SearchResults<T> where T : CustomItem
    {
        /// <summary>
        /// Gets the Total number of results
        /// </summary>
        public int Total { get; set; } = 0;

        /// <summary>
        /// Gets the Results
        /// </summary>
        public IEnumerable<T> Results { get; set; }

        public SearchResults(int total, IEnumerable<T> results)
        {
            this.Total = total;
            this.Results = results ?? new List<T>();
        }
    }
}