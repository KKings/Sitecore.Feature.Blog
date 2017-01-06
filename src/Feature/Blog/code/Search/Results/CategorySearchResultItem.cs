namespace Sitecore.Feature.Blog.Search
{
    using ContentSearch;
    using ContentSearch.SearchTypes;
    using Domain;

    public class CategorySearchResultItem : SearchResultItem, ICategory
    {
        [IndexField("category_name")]
        public string CategoryName { get; set; }
    }
}