namespace Sitecore.Feature.Blog.Pipelines.BlogLocalDatasource
{
    using System;
    using Data;
    using Sitecore.Pipelines;

    public class LocalDatasourceArgs : PipelineArgs
    {
        /// <summary>
        /// Gets the FIlter
        /// </summary>
        public string Filter { get; private set; }

        /// <summary>
        /// The Item Id
        /// </summary>
        public ID ItemId { get; private set; }

        /// <summary>
        /// Gets or sets the result of the pipeline
        /// </summary>
        public string Result { get; set; } = String.Empty;

        public LocalDatasourceArgs(ID itemId, string filter)
        {
            this.Filter = filter;
            this.ItemId = itemId;
        }
    }
}