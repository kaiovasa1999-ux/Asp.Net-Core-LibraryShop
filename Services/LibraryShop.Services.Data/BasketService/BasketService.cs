namespace LibraryShop.Services.Data.BasketService
{
    using System.Linq;
    using System.Threading.Tasks;

    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Web.ViewModels.Basket;

    public class BasketService : IBasketService
    {
        private readonly IDeletableEntityRepository<Basket> basketRepo;
        private readonly IDeletableEntityRepository<Book> bookRepo;

        public BasketService(IDeletableEntityRepository<Basket> basketRepo, IDeletableEntityRepository<Book> bookRepo)
        {
            this.basketRepo = basketRepo;
            this.bookRepo = bookRepo;
        }

        ////public BaskteViewModel AddToBasket(int id, string userId)
        ////{
        ////    var basket = this.basketRepo.All().Where(b => b.UserId == userId).FirstOrDefault();
        ////    var item = this.bookRepo.All().Where(b => b.Id == id).FirstOrDefault();
        ////    basket.BooksInsBasket.Add(item);

        ////    return basket;
        ////}

        public Basket AddToBasket(BaskteViewModel basket)
        {
            var basketToAddTo = this.basketRepo.All().Where(b => b.UserId == basket.UserId).FirstOrDefault();
            var item = this.bookRepo.All().Where(b => b.Id == basket.BookId).FirstOrDefault();
            basketToAddTo.BooksInsBasket.Add(item);
            return basketToAddTo;
        }
     }
}
