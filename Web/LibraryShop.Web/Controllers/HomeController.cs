namespace LibraryShop.Web.Controllers
{
    using System.Diagnostics;

    using LibraryShop.Services.Data.BookService;
    using LibraryShop.Services.Data.HomeService;
    using LibraryShop.Web.ViewModels;
    using LibraryShop.Web.ViewModels.Book;
    using LibraryShop.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly IBookService bookService;

        public HomeController(IHomeService homeService, IBookService bookService)
        {
            this.bookService = bookService;
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var viewModel = new IndexPageStatisticsViewModel();
            viewModel = this.homeService.GetStatistics();
            viewModel.BookImage = this.homeService.GetBooksImagesRandom();
            viewModel.Books = this.homeService.GetAllBooksInfo();
            viewModel.Authors = this.bookService.GetAllAuthorsNames();

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ////new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier }
            return this.View();
        }
    }
}
