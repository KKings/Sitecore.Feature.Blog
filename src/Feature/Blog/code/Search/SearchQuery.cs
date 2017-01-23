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
namespace Sitecore.Feature.Blog.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ContentSearch.SearchTypes;

    public class SearchQuery<T> : ISearchQuery<T> where T : SearchResultItem
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