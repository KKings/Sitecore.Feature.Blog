namespace Sitecore.Feature.Blog.FieldTypes
{
    using System;
    using Abstractions;
    using Buckets.FieldTypes;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Pipelines.BlogLocalDatasource;

    public class BlogMultilistWithSearch : BucketList
    {
        /// <summary>
        /// 
        /// </summary>
        protected virtual BaseCorePipelineManager Pipeline {
            get { return ServiceLocator.ServiceProvider.GetService<BaseCorePipelineManager>(); } }

        protected override string MakeFilterQueryable(string locationFilter)
        {
            if (!String.IsNullOrEmpty(locationFilter) && locationFilter.StartsWith("$"))
            {
                var args = new LocalDatasourceArgs(Data.ID.Parse(this.ItemID), locationFilter);

                this.Pipeline.Run("blog.localDatasource", args);

                if (!String.IsNullOrEmpty(args.Result))
                {
                    return args.Result;
                }
            }

            return base.MakeFilterQueryable(locationFilter);
        }
    }
}