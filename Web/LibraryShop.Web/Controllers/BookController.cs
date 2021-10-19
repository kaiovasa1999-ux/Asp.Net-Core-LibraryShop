 namespace LibraryShop.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LibraryShop.Data;
    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Services.Data.BookService;
    using LibraryShop.Services.Data.Dealer;
    using LibraryShop.Web.ClaimsExtnesions;
    using LibraryShop.Web.ViewModels.Book;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly ApplicationDbContext data;
        private readonly IDealerService dealerService;
        private readonly IDeletableEntityRepository<Book> bookRepository;

        public BookController(
            IBookService bookService,
            ApplicationDbContext data,
            IDealerService dealerService,
            IDeletableEntityRepository<Book> bookRepository)
        {
            this.bookService = bookService;
            this.data = data;
            this.dealerService = dealerService;
            this.bookRepository = bookRepository;
        }

        [Authorize]
        public IActionResult Add()
        {
            var isDealer = this.dealerService.GetDealerIByItUserId(this.User.GetId());

            if (isDealer == 0)
            {
                return this.RedirectToAction("Error", "Dealer");
            }

            var viewModel = new AddBookFormModel();
            viewModel.Geners = this.bookService.GetAllGeners();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddBookFormModel inputBook)
        {
            var isDealer = this.dealerService.GetDealerIByItUserId(this.User.GetId());

            if (isDealer == 0)
            {
                return this.RedirectToAction("ErrorPage", "View");
            }

            var userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                inputBook.Geners = this.bookService.GetAllGeners();
                return this.View(inputBook);
            }

            var delerId = this.dealerService.GetDealerIByItUserId(userId);

            var bookData = new Book()
            {
                Author = inputBook.Author,
                Title = inputBook.Title,
                DealerId = delerId,
                Pages = inputBook.Pages,
                ImageUrl = inputBook.ImageUrl,
                YearCreated = inputBook.YearCreated,
                Price = inputBook.Price,
                Description = inputBook.Description,
                GenreId = inputBook.GenerID,
            };

            await this.data.Books.AddAsync(bookData);
            await this.data.SaveChangesAsync();

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult All([FromQuery] AllBooksListingAndSortingViewModel query)
        {
            var queryResult = this.bookService.All(
                query.Author,
                query.Gener,
                query.Title,
                query.SearchTherm,
                query.Sorting,
                query.CurrentPage,
                AllBooksListingAndSortingViewModel.BooksPerPage);

            var booksGenres = this.bookService.GetGenerName();
            var booksTitles = this.bookService.GetAllBooksTitles();
            var totalBooks = this.bookService.GetTotalBooks();

            query.Books = queryResult.Books;
            query.Geners = booksGenres;
            query.TotalBooks = totalBooks;
            query.Titles = booksTitles;

            return this.View(query);
        }

        public IActionResult AboutBook(AboutBookViewModel bookInput)
        {
            var book = this.bookService.GetDetails(bookInput);
            return this.View(book);
        }

        [Authorize]
        public IActionResult MyBooks(MyBooksViewModel inputBook)
        {
            string userId = this.User.GetId();
            inputBook.MyBooks = this.bookService.ByDealer(userId);

            return this.View(inputBook);
        }
    }
}
