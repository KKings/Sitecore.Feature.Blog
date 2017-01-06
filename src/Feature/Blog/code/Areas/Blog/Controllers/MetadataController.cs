namespace Sitecore.Feature.Blog.Areas.Blog.Controllers
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Items;
    using Models;
    using Mvc.Controllers;

    public class MetadataController : SitecoreController
    {
        public ActionResult OpenGraphMetadata()
        {
            var blogPost = (BlogPostItem)Mvc.Presentation.RenderingContext.Current.ContextItem;

            var url = blogPost.Url;
            var title = blogPost.Title;
            var description = Regex.Replace(blogPost.Summary, "<.*?>", String.Empty);
            var type = "article";
            var imageUrl = blogPost.Thumbnail?.Src;

            var viewModel = new OpenGraphMetadataViewModel(url, title, description, type, imageUrl);

            return this.View(viewModel);
        }
    }
}