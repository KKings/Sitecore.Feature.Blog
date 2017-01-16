namespace Sitecore.Feature.Blog.Services
{
    using System.Collections.Generic;
    using Areas.Blog.Models;

    public interface IPaginationService
    {
        IEnumerable<PageViewModel> GeneratePages(int total, int display, int currentPage);
    }
}