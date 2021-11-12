namespace LibraryShop.Services.Data.HomeService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Web.ViewModels.Book;
    using LibraryShop.Web.ViewModels.Home;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Dealer> dealerssRepository;
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Genre> genersRepository;

        public HomeService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Dealer> dealerssRepository,
            IDeletableEntityRepository<Book> booksRepository,
            IDeletableEntityRepository<Genre> genersRepository)
        {
            this.usersRepository = usersRepository;
            this.dealerssRepository = dealerssRepository;
            this.booksRepository = booksRepository;
            this.genersRepository = genersRepository;
        }

        public List<BookInfoIndexPage> GetAllBooksInfo()
        {
            return this.booksRepository.All().Select(b => new BookInfoIndexPage
            {
                Id = b.Id,
                Author = b.Author,
                Title = b.Title,
                Descirption = b.Description,
                ImageUrl = b.ImageUrl,
                Pages = b.Pages,
                Price = b.Price,
            })
                .ToList();
        }

        public List<string> GetBooksImagesRandom()
        {
            return this.booksRepository.All().OrderBy(b => Guid.NewGuid())
                .Select(b => b.ImageUrl).ToList();
        }

        public IndexPageStatisticsViewModel GetStatistics()
        {
            var viewModel = new IndexPageStatisticsViewModel();
            viewModel.TotalUsers = this.usersRepository.AllAsNoTracking().Count();
            viewModel.TotalDealers = this.dealerssRepository.AllAsNoTracking().Count();
            viewModel.TotalBooks = this.booksRepository.AllAsNoTracking().Count();
            viewModel.TotalBookGeners = this.genersRepository.AllAsNoTracking().Count();

            return viewModel;
        }
    }
}
