namespace LibraryShop.Web.Controllers
{
    using LibraryShop.Services.Data.BasketService;
    using LibraryShop.Web.ClaimsExtnesions;
    using Microsoft.AspNetCore.Mvc;

    public class BasketController : Controller
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public IActionResult AddToBasket(int id, string userId)
        {
            userId = this.User.GetId();
            this.basketService.AddToBasket(id, userId);
            return this.View();
        }
    }
}
