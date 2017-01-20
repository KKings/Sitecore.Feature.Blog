namespace Sitecore.Feature.Blog.Search.Results
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using ContentSearch;
    using ContentSearch.Converters;
    using Data;

    public class SearchResultItem : ContentSearch.SearchTypes.SearchResultItem
    {
        [IndexField("_templates")]
        [TypeConverter(typeof(IndexFieldEnumerableConverter))]
        public IList<ID> TemplateIds { get; set; }
    }
}