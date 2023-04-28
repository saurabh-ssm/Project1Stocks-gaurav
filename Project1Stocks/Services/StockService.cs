using Microsoft.EntityFrameworkCore;
using Project1Stocks.Data;

namespace Project1Stocks.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _dbContext;

        public StockService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dictionary<string, decimal>> GetStockPricesAsync()
        {
            var stocks = await _dbContext.Companies.ToDictionaryAsync(c => c.Name, c => c.StockPrice);
            return stocks;
        }
    }
}
