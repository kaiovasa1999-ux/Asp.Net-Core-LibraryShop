namespace LibraryShop.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using LibraryShop.Common;
    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Services.Data.Dealer;
    using LibraryShop.Web.ViewModels.Dealer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/dealers")]
    public class DealersApiController : ControllerBase
    {
        private readonly IDealerService dealerService;
        private readonly IDeletableEntityRepository<Dealer> dealersRepository;

        public DealersApiController(IDealerService dealerService, IDeletableEntityRepository<Dealer> dealersRepository)
        {
            this.dealerService = dealerService;
            this.dealersRepository = dealersRepository;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IEnumerable<AllDealersModel> GetAll()
        {
            return this.dealerService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Dealer> GetById(int id)
        {
            var dealer = this.dealersRepository.All().FirstOrDefault(d => d.Id == id);

            if (dealer == null)
            {
                return this.NotFound();
            }

            return dealer;
        }
    }
}
