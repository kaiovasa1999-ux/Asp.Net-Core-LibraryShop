namespace LibraryShop.Web.Controllers
{
    using LibraryShop.Data.Models;
    using LibraryShop.Services.Data.BasketService;
    using LibraryShop.Web.ClaimsExtnesions;
    using LibraryShop.Web.ViewModels.Basket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BasketController : Controller
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public IActionResult MyBasket()
        {
            return this.View();
        }
        [Authorize]
        public IActionResult AddToBasket(BaskteViewModel basket)
        {
            var basketData = new Basket()
            {
                UserId = this.User.GetId(),
                BooksInsBasket = basket.BooksInBasket,
                TotalPrice = basket.TotalPrice,
            };
            basket.UserId = this.User.GetId();
            this.basketService.AddToBasket(basket);

            return this.View(basket);
        }
    }
}
