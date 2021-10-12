namespace LibraryShop.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LibraryShop.Data.Models;

    public class BookGenerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Genres.Any())
            {
                return;
            }

            await dbContext.Genres.AddAsync(new Genre { Name = "Action and Adventure" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Classics" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Comic Book or Graphic Novel" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Detective and Mystery" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Fantasy" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Historical Fiction" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Horror" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Literary Fiction" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Romance" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Science Fiction (Sci-Fi)" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Short Stories" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Suspense and Thrillers" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Women's Fiction" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Cookbooks" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Biographies and Autobiographies" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Essays" });
            await dbContext.Genres.AddAsync(new Genre { Name = "History" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Memoir" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Poetry" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Self-Help" });
            await dbContext.Genres.AddAsync(new Genre { Name = "True Crime" });

            await dbContext.SaveChangesAsync();
        }
    }
}
