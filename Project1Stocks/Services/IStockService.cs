using System.Collections.Generic;
using System.Threading.Tasks;
namespace Project1Stocks.Services
{
    public interface IStockService
    {
        Task<Dictionary<string, decimal>> GetStockPricesAsync();
    }
}
