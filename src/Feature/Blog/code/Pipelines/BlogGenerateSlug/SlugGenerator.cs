namespace Sitecore.Feature.Blog.Pipelines.BlogGenerateSlug
{
    using System;
    using System.Text.RegularExpressions;

    public abstract class SlugGenerator
    {
        public virtual string GenerateSlug(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var regex = new Regex("[^a-zA-Z0-9 -]");

            return regex.Replace(value, String.Empty).Replace(" ", "-").ToLower();
        }
    }
}