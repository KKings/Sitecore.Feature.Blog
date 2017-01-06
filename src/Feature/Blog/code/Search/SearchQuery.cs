namespace Sitecore.Feature.Blog.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ContentSearch.SearchTypes;

    public class SearchQuery<T> where T : SearchResultItem
    {
        /// <summary>
        /// Gets the Queries to be used within the Where clause
        /// </summary>
        public virtual Expression<Func<T, bool>> Queries { get; set; }
        
        /// <summary>
        /// Gets the Queries to be used within the Filter clause
        /// </summary>
        public virtual Expression<Func<T, bool>> Filters { get; set; }

        /// <summary>
        /// Gets the Sorting Expression to be used within the Order By clause
        /// </summary>
        public virtual IEnumerable<SortExpression<T>> Sorts { get; set; } = new SortExpression<T>[0];

        /// <summary>
        /// Gets the Paging to be used within the Skip/Take clauses
        /// </summary>
        public virtual Paging Paging { get; set; } = new Paging();
    }
}