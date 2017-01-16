namespace Sitecore.Feature.Blog.Search
{
    public class Paging
    {
        /// <summary>
        /// Gets or sets the PageMode
        /// </summary>
        public virtual PageMode PageMode { get; set; } = PageMode.Pager;

        /// <summary>
        /// Gets or sets the Start
        /// </summary>
        public virtual int Page { get; set; } = 0;

        /// <summary>
        /// Gets or sets the returned results
        /// </summary>
        public virtual int Display { get; set; } = 10;

        /// <summary>
        /// Gets the calculated StartingPosition
        /// </summary>
        public virtual int StartingPosition
        {
            get
            {
                // If the PageMode is the Pager, we need to calculate the starting position
                if (this.PageMode == PageMode.Pager)
                {
                    return this.Page <= 1
                        ? 0
                        : (this.Page - 1) * this.Display;
                }

                return this.Page;
            }
        }
    }
}