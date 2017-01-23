// MIT License
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
namespace Sitecore.Feature.Blog.Areas.Blog.Models
{
    using Items;

    /// <summary>
    /// 
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        public CategoryViewModel(string url, string name)
        {
            this.Url = url;
            this.Name = name;
        }

        /// <summary>
        /// Convert an <see cref="CategoryItem"/> to a <see cref="CategoryViewModel"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator CategoryViewModel(CategoryItem item)
        {
            if (item == null)
            {
                return null;
            }

            var model = new CategoryViewModel(item.Url, item.CategoryName);

            return model;
        }
    }
}