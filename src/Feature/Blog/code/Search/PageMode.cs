namespace Sitecore.Feature.Blog.Search
{
    /// <summary>
    /// PageMode for determing how to get results
    /// Pager, indicates that you will use a page computed variable
    /// Start, indicates that you will pass in the starting position
    /// </summary>
    public enum PageMode
    {
        Pager,
        Start
    }
}