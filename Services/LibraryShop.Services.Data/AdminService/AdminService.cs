namespace LibraryShop.Services.Data.AdminService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Web.ViewModels.Administration.Models;

    public class AdminService : IAdminService
    {
        private readonly IDeletableEntityRepository<Genre> genersRepository;
        private readonly IDeletableEntityRepository<Dealer> dealersRepository;


        public AdminService(
            IDeletableEntityRepository<Genre> genersRepository,
            IDeletableEntityRepository<Dealer> dealersRepository)
        {
            this.genersRepository = genersRepository;
            this.dealersRepository = dealersRepository;
        }

        public async Task CreateNewGener(string generName)
        {
            if (this.genersRepository.All().Any(g => g.Name == generName))
            {
                throw new Exception("This gener allready exist in the Library!");
            }

            var gener = new Genre { Name = generName };
            await this.genersRepository.AddAsync(gener);
            await this.genersRepository.SaveChangesAsync();
        }

        public async Task DeleteDealerById(int dealerId)
        {
            var dealer = this.dealersRepository.All().FirstOrDefault(d => d.Id == dealerId);
            this.dealersRepository.Delete(dealer);
            await this.dealersRepository.SaveChangesAsync();
        }

        public async Task DeleteGener(string generName)
        {
            var gener = this.genersRepository.All().FirstOrDefault(g => g.Name == generName);

            if (gener != null)
            {
                this.genersRepository.Delete(gener);
                await this.genersRepository.SaveChangesAsync();
            }
        }
    }
}
