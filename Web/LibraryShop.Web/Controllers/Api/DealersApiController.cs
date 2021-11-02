namespace LibraryShop.Web.Controllers.Api
{
    using System.Collections.Generic;

    using LibraryShop.Common;
    using LibraryShop.Services.Data.Dealer;
    using LibraryShop.Web.ViewModels.Dealer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/dealers")]
    public class DealersApiController : ControllerBase
    {
        private readonly IDealerService dealerService;

        public DealersApiController(IDealerService dealerService)
        {
            this.dealerService = dealerService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IEnumerable<AllDealersModel> All()
        {
            return this.dealerService.GetAll();
        }
    }
}
