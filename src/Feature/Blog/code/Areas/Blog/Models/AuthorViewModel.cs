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