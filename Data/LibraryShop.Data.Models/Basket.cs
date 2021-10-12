namespace LibraryShop.Data.Models
{
    using System.Collections.Generic;

    using LibraryShop.Data.Common.Models;

    public class Basket : BaseDeletableModel<int>
    {
        public Basket()
        {
            this.BooksInsBasket = new HashSet<Book>();
        }

        public int TotalPrice { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Book> BooksInsBasket { get; set; }
    }
}
