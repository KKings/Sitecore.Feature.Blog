namespace Sitecore.Feature.Blog.Search
{
    using System;
    using System.Linq.Expressions;
    using ContentSearch.SearchTypes;

    public class QueryExpression<T> where T: SearchResultItem
    {
        public enum Filter
        {
            And,
            Or
        }

        /// <summary>
        /// Gets the Expression to Query
        /// </summary>
        public Expression<Func<T, bool>> Expression { get; private set; }

        /// <summary>
        /// Gets the Filter to Apply to the Query
        /// </summary>
        public Filter FilterType { get; private set; }

        public QueryExpression(Expression<Func<T, bool>> expression, Filter filterType = Filter.And)
        {
            this.Expression = expression;
            this.FilterType = filterType;
        }
    }
}