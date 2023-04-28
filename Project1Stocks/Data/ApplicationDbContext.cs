using Microsoft.EntityFrameworkCore;
using Project1Stocks.Models;

namespace Project1Stocks.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Wipro", StockPrice = 100 },
                new Company { Id = 2, Name = "Zensar", StockPrice = 200 },
                new Company { Id = 3, Name = "TCS", StockPrice = 300 },
                new Company { Id = 4, Name = "Tesla", StockPrice = 400 },
                new Company { Id = 5, Name = "Apple", StockPrice = 500 }
            );
        }
    }
}
