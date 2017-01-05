namespace Sitecore.Feature.Blog.Domain
{
    using System;
    using System.Collections.Generic;
    using Data;

    public interface IBlog
    {
        string PostTitle { get; }
        string Summary { get; }
        string Body { get; }
        DateTime PublishDate { get; }

        string Url { get; }

        IList<ID> Authors { get; }
        IList<ID> Categories { get; }
        IList<ID> Tags { get; }
    }
}