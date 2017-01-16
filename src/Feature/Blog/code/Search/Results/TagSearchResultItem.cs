namespace Sitecore.Feature.Blog.Search.Results
{
    using ContentSearch;
    using ContentSearch.SearchTypes;

    public class TagSearchResultItem : SearchResultItem
    {
        [IndexField("tag_name")]
        public string TagName { get; set; }
    }
}