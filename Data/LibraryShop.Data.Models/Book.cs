namespace LibraryShop.Data.Models
{
    using System;

    using LibraryShop.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        public string Author { get; set; }

        public int Pages { get; set; }

        public int Price { get; set; }

        public DateTime YearCreated { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public int DealerId { get; set; }

        public Dealer Dealer { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
