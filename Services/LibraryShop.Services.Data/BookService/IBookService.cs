namespace LibraryShop.Services.Data.BookService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LibraryShop.Data.Models;
    using LibraryShop.Web.ViewModels.Book;

    public interface IBookService
    {
        Task AddAsync(AddBookFormModel inputBook);

        IEnumerable<GenerViewModel> GetAllGeners();

        IEnumerable<string> GetGenerName();

        AboutBookViewModel GetDetails(AboutBookViewModel bookInput);

        AboutBookViewModel GetDetailsByImage(string imageUrl);

        ServiceModelSorting All(
            string author,
            string gener,
            string title,
            string serchTherm,
            BooksSortedEnumerator sorting,
            int currentPage,
            int booksPerPage = 6);

        int GetTotalBooks();

        IEnumerable<string> GetAllBooksTitles();

        IEnumerable<BookListingViewModel> ByDealer(string userId);

        EditbookFormModel GetById(int id, string userId);

        Task UpdateAsync(int id, EditbookFormModel book);

        Task DeleteAsync(int id);

        List<string> GetAllAuthorsNames();
    }
}
