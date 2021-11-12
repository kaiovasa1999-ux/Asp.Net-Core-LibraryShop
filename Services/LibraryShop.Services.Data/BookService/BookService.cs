namespace LibraryShop.Services.Data.BookService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Services.Mapping;
    using LibraryShop.Web.ViewModels.Book;

    public class BookService : IBookService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Genre> genersRepository;
        private readonly IDeletableEntityRepository<Dealer> dealersRepository;

        public BookService(
            IDeletableEntityRepository<Book> bookRepository,
            IDeletableEntityRepository<Genre> genersRepository,
            IDeletableEntityRepository<Dealer> dealersRepository)
        {
            this.booksRepository = bookRepository;
            this.genersRepository = genersRepository;
            this.dealersRepository = dealersRepository;
        }

        public async Task AddAsync(AddBookFormModel inputBook)
        {
            var bookData = new Book()
            {
                Author = inputBook.Author,
                Title = inputBook.Title,
                GenreId = inputBook.GenerID,
                Price = inputBook.Price,
                YearCreated = inputBook.YearCreated,
                Pages = inputBook.Pages,
                ImageUrl = inputBook.ImageUrl,
                Description = inputBook.Description,
            };
            await this.booksRepository.AddAsync(bookData);
            await this.booksRepository.SaveChangesAsync();
        }

        public ServiceModelSorting All(
            string author,
            string gener,
            string title,
            string serchTherm,
            BooksSortedEnumerator sorting,
            int currentPage,
            int booksPerPage = 3)
        {
            var booksQuery = this.booksRepository.All().AsQueryable();

            if (!string.IsNullOrWhiteSpace(gener))
            {
                booksQuery = booksQuery.Where(b => b.Genre.Name.ToLower() == gener.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                booksQuery = booksQuery.Where(b => b.Title.ToLower() == title.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(serchTherm))
            {
                booksQuery = booksQuery.Where(b => b.Description.ToLower().Contains(serchTherm.ToLower())
                || b.Title.ToLower().Contains(serchTherm.ToLower())
                || b.Author.ToLower().Contains(serchTherm.ToLower()));
            }

            booksQuery = sorting switch
            {
                BooksSortedEnumerator.AuthorName => booksQuery.OrderByDescending(b => b.Author),
                BooksSortedEnumerator.Pages => booksQuery.OrderByDescending(b => b.Pages),
                BooksSortedEnumerator.Price => booksQuery.OrderByDescending(b => b.Price),
                BooksSortedEnumerator.YearCreated => booksQuery.OrderByDescending(b => b.YearCreated),
                _ => booksQuery.OrderByDescending(b => b.Id),
            };

            var totalBooks = booksQuery.Count();

            var books = booksQuery
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage)
                .Select(b => new BookListingViewModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    Title = b.Title,
                    CategoryName = b.Genre.Name,
                    YearCreated = b.YearCreated,
                    ImageUrl = b.ImageUrl,
                    Descrption = b.Description,
                }).ToList();

            return new ServiceModelSorting
            {
                BooksPerPage = booksPerPage,
                Books = books,
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
            };
        }

        public IEnumerable<BookListingViewModel> ByDealer(string userId)
        {
            return this.booksRepository.All().Where(b => b.Dealer.UserId == userId)
                .To<BookListingViewModel>().ToList();
        }

        public async Task DeleteAsync(int id)
        {
            var book = this.booksRepository.All().FirstOrDefault(x => x.Id == id);
            this.booksRepository.Delete(book);
            await this.booksRepository.SaveChangesAsync();
        }

        public List<string> GetAllAuthorsNames()
        {
            var authors = this.booksRepository.All().Select(b => b.Author).ToList();
            return authors;
        }

        // TODO: Have to insert all Scaffiolded controllers functionality in services!!!

        ////public async Task Details(int? id)
        ////{
        ////        if (id == null)
        ////        {
        ////        throw new Exception("Not foind");
        ////        }

        ////        var book = await this.booksRepository.All()
        ////            .Contains(b => b.Dealer)
        ////            .Include(b => b.Genre)
        ////            .FirstOrDefaultAsync(m => m.Id == id);
        ////        if (book == null)
        ////        {
        ////            return this.NotFound();
        ////        }

        ////        return book;
        ////}

        public IEnumerable<string> GetAllBooksTitles()
        {
            return this.booksRepository.All().Distinct()
                .Select(b => b.Title).ToList();
        }

        public IEnumerable<GenerViewModel> GetAllGeners()
        {
            return this.genersRepository.All().Select(g => new GenerViewModel
            {
                Id = g.Id,
                Name = g.Name,
            })
                .ToList();
        }

        public EditbookFormModel GetById(int id, string userId)
        {
            return this.booksRepository.All().Where(b => b.Id == id)
                .Select(b => new EditbookFormModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    Title = b.Title,
                    YearCreated = b.YearCreated,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Pages = b.Pages,
                    GenerId = b.GenreId,
                    BookDealerUserId = userId,
                })
                .FirstOrDefault();
        }

        public AboutBookViewModel GetDetails(AboutBookViewModel bookInput)
        {
            var book = this.booksRepository.All().Where(b => b.Id == bookInput.Id)
                .Select(b => new AboutBookViewModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    Title = b.Title,
                    CategoryName = b.Genre.Name,
                    YearCreated = b.YearCreated,
                    Price = b.Price,
                    Pages = b.Pages,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                })
                .FirstOrDefault();

            return book;
        }

        public AboutBookViewModel GetDetailsByImage(string imageUrl)
        {
            return this.booksRepository.All()
             .Select(b => new AboutBookViewModel
             {
                 Author = b.Author,
                 Title = b.Title,
                 Pages = b.Pages,
                 Price = b.Price,
                 ImageUrl = b.ImageUrl,
                 Description = b.Description,
                 CategoryName = b.Genre.Name,
             })
                .FirstOrDefault(b => b.ImageUrl == imageUrl);
        }

        public IEnumerable<string> GetGenerName()
        {
            return this.booksRepository.All().Select(b => b.Genre.Name).ToList();
        }

        public int GetTotalBooks()
        {
            return this.booksRepository.All().Count();
        }

        public async Task UpdateAsync(int id, EditbookFormModel book)
        {
            var bookData = this.booksRepository.All().FirstOrDefault(x => x.Id == id);
            bookData.Author = book.Author;
            bookData.Title = book.Title;
            bookData.Id = book.Id;
            bookData.GenreId = book.GenerId;
            bookData.Price = book.Price;
            bookData.Pages = book.Pages;
            bookData.YearCreated = book.YearCreated;
            bookData.ImageUrl = book.ImageUrl;
            bookData.Description = book.Description;
            await this.booksRepository.SaveChangesAsync();
        }
    }
}
