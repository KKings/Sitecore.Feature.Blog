namespace Sitecore.Feature.Blog.Extensions
{
    using System;
    using System.Web;
    using Abstractions;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Helpers;

    /// <summary>
    /// Extensions to <see cref="SitecoreHelper"/>
    /// </summary>
    public static class SitecoreHelperExtensions
    {
        /// <summary>
        /// Translates a specific key from the dictionary
        /// </summary>
        /// <param name="helper">The Sitecore Helper</param>
        /// <param name="key">The Key</param>
        /// <returns></returns>
        public static HtmlString Text(this SitecoreHelper helper, string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return new HtmlString(String.Empty);
            }

            var translator = ServiceLocator.ServiceProvider.GetService<BaseTranslate>();

            var text =  translator.Text(key);

            return new HtmlString(text);
        }
    }
}