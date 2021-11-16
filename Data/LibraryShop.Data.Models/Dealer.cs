namespace LibraryShop.Data.Models
{
    using System.Collections.Generic;

    using LibraryShop.Data.Common.Models;

    public class Dealer : BaseDeletableModel<int>
    {
        public Dealer()
        {
            this.BooksAdded = new HashSet<Book>();
        }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Book> BooksAdded { get; set; }

        public string UserId { get; set; }
    }
}
