namespace Sitecore.Feature.Blog.Areas.Blog
{
    using System.Web.Mvc;

    public class BlogAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Blog";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Sitecore.Feature.Blog",
                "api/blog/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}