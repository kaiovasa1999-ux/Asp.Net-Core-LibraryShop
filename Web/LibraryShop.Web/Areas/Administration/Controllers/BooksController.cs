namespace LibraryShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LibraryShop.Data;
    using LibraryShop.Data.Common.Repositories;
    using LibraryShop.Data.Models;
    using LibraryShop.Services.Data.BookService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class BooksController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Book> db;
        private readonly IBookService bookService;

        public BooksController(IDeletableEntityRepository<Book> db, IBookService bookService)
        {
            this.bookService = bookService;
            this.db = db;
        }

        // GET: Administration/Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.All().Include(b => b.Dealer).Include(b => b.Genre);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var book = await this.db.All()
                .Include(b => b.Dealer)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return this.NotFound();
            }

            return this.View(book);
        }

        // GET: Administration/Books/Create
        public IActionResult Create()
        {
            this.ViewData["DealerId"] = new SelectList(this.db.All(), "Id", "Id");
            this.ViewData["GenreId"] = new SelectList(this.db.All(), "Id", "Id");
            return this.View();
        }

        // POST: Administration/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Author,Title,Pages,Price,YearCreated,GenreId,DealerId,ImageUrl,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Book book)
        {
            if (this.ModelState.IsValid)
            {
                await this.db.AddAsync(book);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["DealerId"] = new SelectList(this.db.All(), "Id", "Id", book.DealerId);
            this.ViewData["GenreId"] = new SelectList(this.db.All(), "Id", "Id", book.GenreId);
            return this.View(book);
        }

        // GET: Administration/Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var book = this.db.All().FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return this.NotFound();
            }

            this.ViewData["DealerId"] = new SelectList(this.db.All(), "Id", "Id", book.DealerId);
            this.ViewData["GenreId"] = new SelectList(this.db.All(), "Id", "Id", book.GenreId);
            return this.View(book);
        }

        // POST: Administration/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Author,Title,Pages,Price,YearCreated,GenreId,DealerId,ImageUrl,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Book book)
        {
            if (id != book.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(book);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.BookExists(book.Id))
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

            this.ViewData["DealerId"] = new SelectList(this.db.All(), "Id", "Id", book.DealerId);
            this.ViewData["GenreId"] = new SelectList(this.db.All(), "Id", "Id", book.GenreId);
            return this.View(book);
        }

        // GET: Administration/Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var book = await this.db.All()
                .Include(b => b.Dealer)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return this.NotFound();
            }

            return this.View(book);
        }

        // POST: Administration/Books/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = this.db.All().FirstOrDefault(x => x.Id == id);
            this.db.Delete(book);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool BookExists(int id)
        {
            return this.db.All().Any(e => e.Id == id);
        }
    }
}
