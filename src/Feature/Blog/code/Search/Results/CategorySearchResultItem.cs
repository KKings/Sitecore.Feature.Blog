namespace Sitecore.Feature.Blog.Search.Results
{
    using ContentSearch;
    using ContentSearch.SearchTypes;
    using Domain;

    public class CategorySearchResultItem : SearchResultItem
    {
        [IndexField("category_name")]
        public string CategoryName { get; set; }
    }
}