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

            this.TempData["Message"] = "Book added succesfuly!";

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
            var totalBooks = this.bookService.GetTotalBooks();
            var booksTitles = this.bookService.GetAllBooksTitles();

            query.Books = queryResult.Books;
            query.Geners = booksGenres;
            query.TotalBooks = totalBooks;
            query.Titles = booksTitles;

            return this.View(query);
        }

        [HttpPost]
        public IActionResult FindByImage(string image)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            var book = this.bookService.GetDetailsByImage(image);
            return this.View(book);
        }

        [Authorize]
        public IActionResult EditBook(int id)
        {
            var userId = this.User.GetId();
            var inputModel = this.bookService.GetById(id, this.User.GetId());
            inputModel.Id = id;
            var bookDealer = inputModel.BookDealerUserId;
            inputModel.Geners = this.bookService.GetAllGeners();

            if (!this.dealerService.IsTheSameDealer(bookDealer))
            {
                this.TempData["WrongUser"] = "This book is not added by you!";

                return this.RedirectToAction("Become", "Dealer");
            }

            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditBook(int id, EditbookFormModel book)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var bookDealer = book.BookDealerUserId;
            var userId = this.User.GetId();

            if (!this.dealerService.IsTheSameDealer(userId))
            {
                this.TempData["WrongUser"] = "This book is not added by you!";

                return this.RedirectToAction("Become", "Dealer");
            }

            await this.bookService.UpdateAsync(id, book);

            return this.RedirectToAction(nameof(this.AboutBook), new { id });
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await this.bookService.DeleteAsync(id);
            this.TempData["BookDeleted"] = "Book was delete succesfuly!";
            return this.RedirectToAction(nameof(this.AboutBook));
        }
    }
}
