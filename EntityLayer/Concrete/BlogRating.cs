﻿namespace EntityLayer.Concrete
{
    public class BlogRating
    {
        public int BlogRatingID { get; set; }

        public int BlogID { get; set; }

        public int BlogTotalRating { get; set; }

        public int BlogRatingCount { get; set; }
    }
}
