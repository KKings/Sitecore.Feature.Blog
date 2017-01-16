namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using Domain;
    using Items;

    public interface ITagService
    {
        IEnumerable<TagItem> All(BlogContext context);
    }
}