namespace LibraryShop.Services.Data.HomeService
{
    using System.Collections.Generic;

    using LibraryShop.Data.Models;
    using LibraryShop.Web.ViewModels.Book;
    using LibraryShop.Web.ViewModels.Home;

    public interface IHomeService
    {
        IndexPageStatisticsViewModel GetStatistics();

        List<BookInfoIndexPage> GetAllBooksInfo();

        List<string> GetBooksImagesRandom();
    }
}
