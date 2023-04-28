using ConsumeStock1API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO.Pipelines;

namespace ConsumeStock1API.Controllers
{
    public class StocksController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5000/api");
        HttpClient client;
        public StocksController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/api/");
            var response = await client.GetAsync("Stocks");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<Dictionary<string, decimal>>();
                var companies = data.Select(d => new Company { Name = d.Key, StockPrice = d.Value });
                return View(companies);
            }
            else
            {
                // handle error
                return View();
            }
        }
    }
}
