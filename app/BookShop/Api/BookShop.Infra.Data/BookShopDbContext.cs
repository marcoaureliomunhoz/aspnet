using BookShop.Infra.Data.EntitiesTypesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infra.Data
{
    public class BookShopDbContext : DbContext
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}