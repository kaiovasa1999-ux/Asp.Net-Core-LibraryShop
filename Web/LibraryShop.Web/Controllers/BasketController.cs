namespace LibraryShop.Web.Controllers
{
    using LibraryShop.Data.Models;
    using LibraryShop.Services.Data.BasketService;
    using LibraryShop.Web.ClaimsExtnesions;
    using LibraryShop.Web.ViewModels.Basket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class BasketController : Controller
    {
        private readonly IBasketService basketService;
        private readonly ILogger<BaseController> logger;

        public BasketController(IBasketService basketService,ILogger<BaseController> logger)
        {
            this.basketService = basketService;
            this.logger = logger;
        }

        public IActionResult MyBasket()
        {
            // this.logger.LogInformation(12345, "Ask for basket");
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

            // this.logger.LogInformation(12345, "Ask for basket");
            return this.View(basket);
        }
    }
}
