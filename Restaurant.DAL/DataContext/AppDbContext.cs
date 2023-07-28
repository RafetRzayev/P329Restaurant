using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Entites;

namespace Restaurant.DAL.DataContext
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Ingridient> Ingridients { get; set;}
        public DbSet<FoodIngridient> FoodIngridients { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
