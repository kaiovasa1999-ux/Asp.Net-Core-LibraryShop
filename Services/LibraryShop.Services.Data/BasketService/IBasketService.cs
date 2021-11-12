    namespace LibraryShop.Services.Data.BasketService
{
    using LibraryShop.Data.Models;
    using LibraryShop.Web.ViewModels.Basket;

    public interface IBasketService
    {
        Basket AddToBasket(BaskteViewModel basket);
    }
}
