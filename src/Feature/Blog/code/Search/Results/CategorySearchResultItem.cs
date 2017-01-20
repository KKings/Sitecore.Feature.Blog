namespace Sitecore.Feature.Blog.Search.Results
{
    using ContentSearch;

    public class CategorySearchResultItem : SearchResultItem
    {
        [IndexField("category_name")]
        public string CategoryName { get; set; }
    }
}