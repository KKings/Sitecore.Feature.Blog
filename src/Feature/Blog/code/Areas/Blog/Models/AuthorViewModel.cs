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
    using Domain;
    using Items;

    public class AuthorViewModel
    {
        public string Fullname { get; set; }

        public string Url { get; set; }

        public string Biography { get; set; }

        public ImageMedia ProfileImage { get; set; }

        /// <summary>
        /// Convert an <see cref="AuthorItem"/> to a <see cref="AuthorViewModel"/>
        /// </summary>
        /// <param name="item">The Item</param>
        public static implicit operator AuthorViewModel(AuthorItem item)
        {
            if (item == null)
            {
                return null;
            }

            var model = new AuthorViewModel
            {
                Url = item.Url,
                Fullname = item.FullName,
                Biography = item.Biography,
                ProfileImage = item.ProfileImage
            };

            return model;
        }
    }
}