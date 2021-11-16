namespace LibraryShop.Data.Models
{
    using System.Collections.Generic;

    using LibraryShop.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
