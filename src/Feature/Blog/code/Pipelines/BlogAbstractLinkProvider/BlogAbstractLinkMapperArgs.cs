﻿// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
namespace Sitecore.Feature.Blog.Pipelines.BlogAbstractUrlResolver
{
    using System.Collections.Generic;
    using Data;
    using global::Sitecore.Links;
    using global::Sitecore.Pipelines;

    public class BlogAbstractLinkMapperArgs : PipelineArgs
    {
        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets if the Url has been resolved
        /// </summary>
        public bool IsResolved { get; set; } = false;

        /// <summary>
        /// Gets the url property mappings
        /// </summary>
        public IDictionary<string, object> Properties { get; private set; }

        /// <summary>
        /// Get or sets the Url Options
        /// </summary>
        public UrlOptions Options { get; private set; }

        public BlogAbstractLinkMapperArgs(IDictionary<string, object> properties, UrlOptions options)
        {
            this.Properties = properties;
            this.Options = options ?? UrlOptions.DefaultOptions;
        }
    }
}