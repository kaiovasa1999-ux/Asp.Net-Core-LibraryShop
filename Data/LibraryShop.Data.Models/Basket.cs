namespace LibraryShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LibraryShop.Data.Common.Models;

    public class Basket : BaseDeletableModel<string>
    {
        public Basket()
        {
            this.BooksInsBasket = new HashSet<Book>();
            this.Id = Guid.NewGuid().ToString();
        }

        public double TotalPrice { get; set; }

        public string UserId { get; set; }

        public ICollection<Book> BooksInsBasket { get; set; }
    }
}
