﻿namespace LibraryShop.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using LibraryShop.Data;
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

        public BookController(
            IBookService bookService,
            ApplicationDbContext data,
            IDealerService dealerService)
        {
            this.bookService = bookService;
            this.data = data;
            this.dealerService = dealerService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddBookFormModel();
            viewModel.Geners = this.bookService.GetAllGeners();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddBookFormModel inputBook)
        {
            var userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                inputBook.Geners = this.bookService.GetAllGeners();
                return this.View(inputBook);
            }

            var userIsDealer = this.data.Dealers.Any(d => d.UserId == userId);
            if (!userIsDealer)
            {
                return this.RedirectToAction("ErrorPage", "View");
            }

            var delerid = this.dealerService.GetDealerIByItUserId(userId);

            var bookData = new Book()
            {
                Author = inputBook.Author,
                Title = inputBook.Title,
                DealerId = delerid,
                Pages = inputBook.Pages,
                ImageUrl = inputBook.ImageUrl,
                Price = inputBook.Price,
                Description = inputBook.Description,
                GenreId = inputBook.GenerID,
            };

            await this.data.Books.AddAsync(bookData);
            await this.data.SaveChangesAsync();

            /////await this.bookService.AddAsync(inputBook);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
