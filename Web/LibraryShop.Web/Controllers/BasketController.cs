namespace LibraryShop.Web.Controllers
{
    using LibraryShop.Services.Data.BasketService;
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

        [Authorize]
        public IActionResult MyBasket(BaskteViewModel basketView)
        {
            return this.View(basketView);
        }
    }
}
