namespace Sitecore.Feature.Blog.Extensions
{
    using System.Linq;
    using global::Sitecore;
    using Data;
    using Data.Templates;

    /// <summary>
    /// Sitecore Template Extensions
    /// </summary>
    public static class TemplateExtensions
    {
        /// <summary>
        /// Determines if the template is derived from another template by template id
        /// </summary>
        /// <param name="template">The template</param>
        /// <param name="templateId">The template id</param>
        /// <returns><c>true</c> if the template is derived from another template</returns>
        public static bool IsDerived([NotNull] this Template template, [NotNull] ID templateId)
        {
            return template.ID == templateId || template.GetBaseTemplates().Any(baseTemplate => baseTemplate.IsDerived(templateId));
        }
    }
}
