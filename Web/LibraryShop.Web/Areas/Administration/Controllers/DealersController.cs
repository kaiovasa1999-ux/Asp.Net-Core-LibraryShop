namespace LibraryShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LibraryShop.Data;
    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("DealerArea")]
    public class DealersController : DealerControllerArea
    {
        private readonly IDeletableEntityRepository<Dealer> dataRepository;

        public DealersController(IDeletableEntityRepository<Dealer> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Dealers
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dataRepository.All().ToListAsync());
        }

        // GET: Administration/Dealers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return this.NotFound();
            }

            return this.View(dealer);
        }

        // GET: Administration/Dealers/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Dealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PhoneNumber,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Dealer dealer)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(dealer);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(dealer);
        }

        // GET: Administration/Dealers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            if (dealer == null)
            {
                return this.NotFound();
            }

            return this.View(dealer);
        }

        // POST: Administration/Dealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PhoneNumber,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Dealer dealer)
        {
            if (id != dealer.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(dealer);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DealerExists(dealer.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(dealer);
        }

        // GET: Administration/Dealers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = await this.dataRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dealer == null)
            {
                return this.NotFound();
            }

            return this.View(dealer);
        }

        // POST: Administration/Dealers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dealer = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(dealer);
            this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DealerExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
