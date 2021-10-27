namespace LibraryShop.Web.Controllers
{
    using System.Diagnostics;

    using LibraryShop.Services.Data.HomeService;
    using LibraryShop.Web.ViewModels;
    using LibraryShop.Web.ViewModels.Book;
    using LibraryShop.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexPageStatisticsViewModel();
            viewModel = this.homeService.GetStatistics();
            viewModel.BooksImages = this.homeService.GetBooksImagesRandom();
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
