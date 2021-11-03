namespace LibraryShop.Web.Controllers
{
    using System.Threading.Tasks;

    using LibraryShop.Common;
    using LibraryShop.Services.Data.AdminService;
    using LibraryShop.Web.ViewModels.Administration.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        [HttpGet]

        public IActionResult CreateNewGener()
        {
            var viewModel = new CreateGenerViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateNewGener(CreateGenerViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            ////var isDealer = this.dealerService.GetDealerIByItUserId(this.User.GetId());

            ////if (isDealer == 0)
            ////{
            ////    return this.RedirectToAction("ErrorPage", "View");
            ////}

            ////var userId = this.User.GetId();
            ////if (!this.ModelState.IsValid)
            ////{
            ////    inputBook.Geners = this.bookService.GetAllGeners();
            ////    return this.View(inputBook);
            ////}

            await this.adminService.CreateNewGener(viewModel.Name);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
