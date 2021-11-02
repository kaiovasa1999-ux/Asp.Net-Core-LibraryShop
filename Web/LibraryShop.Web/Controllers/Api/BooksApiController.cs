namespace LibraryShop.Web.Controllers.Api
{
    using LibraryShop.Services.Data.BookService;
    using LibraryShop.Web.ViewModels.Book;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/books")]
    public class BooksApiController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksApiController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public ServiceModelSorting GetAll([FromQuery] AllBooksListingAndSortingViewModel query)
        {
           return this.bookService.All(
                query.Author, query.Gener, query.Title, query.SearchTherm, query.Sorting, query.CurrentPage, AllBooksListingAndSortingViewModel.BooksPerPage);
        }
    }
}
