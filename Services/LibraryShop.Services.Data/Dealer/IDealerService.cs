namespace LibraryShop.Services.Data.Dealer
{
    using System.Collections.Generic;

    using LibraryShop.Web.ViewModels.Dealer;

    public interface IDealerService
    {
        public int GetDealerIByItUserId(string dealerUserId);

        public bool IsTheSameDealer(string userId);

        public IEnumerable<AllDealersModel> GetAll();
    }
}
