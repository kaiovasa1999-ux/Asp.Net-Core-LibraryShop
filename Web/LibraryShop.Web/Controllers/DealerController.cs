namespace LibraryShop.Web.Controllers
{
    using System.Linq;

    using LibraryShop.Data;
    using LibraryShop.Data.Models;
    using LibraryShop.Web.ClaimsExtnesions;
    using LibraryShop.Web.ViewModels.Dealer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealerController : Controller
    {
        private readonly ApplicationDbContext data;

        public DealerController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Become()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDealerFormModel inputDealer)
        {
            var viewModel = new BecomeDealerFormModel();
            if (!this.ModelState.IsValid)
            {
                return this.View(inputDealer);
            }

            var userId = this.User.GetId();

            if (this.IsDealer())
            {
                return this.BadRequest();
            }

            var dealerDarta = new Dealer()
            {
                Name = inputDealer.Name,
                PhoneNumber = inputDealer.PhoneNubmer,
                UserId = userId,
            };
            this.data.Dealers.Add(dealerDarta);
            this.data.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        private bool IsDealer()
        {
            return this.data.Dealers.Any(d => d.UserId == this.User.GetId());
        }
    }
}
