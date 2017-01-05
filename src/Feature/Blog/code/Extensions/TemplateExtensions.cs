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
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static bool IsDerived([NotNull] this Template template, [NotNull] ID templateId)
        {
            return template.ID == templateId || template.GetBaseTemplates().Any(baseTemplate => baseTemplate.IsDerived(templateId));
        }
    }
}
