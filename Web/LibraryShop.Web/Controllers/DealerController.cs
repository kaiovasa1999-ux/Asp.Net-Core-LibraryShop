namespace LibraryShop.Web.Controllers
{
    using System;
    using System.Linq;

    using LibraryShop.Data;
    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Web.ClaimsExtnesions;
    using LibraryShop.Web.ViewModels.Dealer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealerController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IDeletableEntityRepository<Dealer> dealrsRepository;

        public DealerController(ApplicationDbContext data, IDeletableEntityRepository<Dealer> dealrsRepository)
        {
            this.dealrsRepository = dealrsRepository;
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

            if (this.dealrsRepository.All().Any(d => d.UserId == userId))
            {
                this.TempData["DealeraMessage"] = "You are already a Dealer!!";
                return this.RedirectToAction("index", "Home");
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

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
