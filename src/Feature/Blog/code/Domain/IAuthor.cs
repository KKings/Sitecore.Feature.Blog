namespace Sitecore.Feature.Blog.Domain
{
    public interface IAuthor
    {
        string FullName { get; }

        string Title { get; }

        string Biography { get; }
    }
}