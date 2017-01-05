namespace Sitecore.Feature.Blog.Search
{
    using ContentSearch;
    using ContentSearch.SearchTypes;
    using Domain;

    public class TagSearchResultItem : SearchResultItem, ITag
    {
        [IndexField("tag_name")]
        public string TagName { get; set; }
    }
}