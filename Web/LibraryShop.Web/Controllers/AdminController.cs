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

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]

        public IActionResult CreateNewGener()
        {
            var viewModel = new GenerFunctionModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateNewGener(GenerFunctionModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.adminService.CreateNewGener(viewModel.Name);
            this.TempData["AddedGener"] = "The gener is Added Succesfully";

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public IActionResult DeleteGener()
        {
            var viewModel = new GenerFunctionModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteGener(GenerFunctionModel view)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(view);
            }

            await this.adminService.DeleteGener(view.Name);
            this.TempData["DeleteMessage"] = "The gener is Delete succesfully!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
