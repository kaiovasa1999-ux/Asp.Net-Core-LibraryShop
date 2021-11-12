namespace LibraryShop.Services.Data.Dealer
{
    using System.Collections.Generic;
    using System.Linq;

    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Web.ViewModels.Dealer;

    public class DealerService : IDealerService
    {
        private readonly IDeletableEntityRepository<Dealer> dealerRepository;

        public DealerService(IDeletableEntityRepository<Dealer> dealerRepository)
        {
            this.dealerRepository = dealerRepository;
        }

        public IEnumerable<AllDealersModel> GetAll()
        {
            return this.dealerRepository.All()
                .OrderByDescending(d => d.Id)
                .Where(d => d.UserId != null)
                .Select(d => new AllDealersModel
                {
                    Name = d.Name,
                    PhoneNumber = d.PhoneNumber,
                }).ToList();
        }

        public int GetDealerIByItUserId(string dealerUserId)
        {
            return this.dealerRepository.All()
                .Where(d => d.UserId == dealerUserId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }

        public bool IsTheSameDealer(string userId)
        {
            return this.dealerRepository.All().Any(d => d.UserId == userId);
        }
    }
}
