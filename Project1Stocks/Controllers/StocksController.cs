using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1Stocks.Data;
using Project1Stocks.WebSockets;
using System.Net.WebSockets;

namespace Project1Stocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<StocksController> _logger;
        private readonly WebSocketHandler _webSocketHandler;

        public StocksController(ApplicationDbContext dbContext, ILogger<StocksController> logger, WebSocketHandler webSocketHandler)
        {
            _dbContext = dbContext;
            _logger = logger;
            _webSocketHandler = webSocketHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _dbContext.Companies.ToDictionaryAsync(c => c.Name, c => c.StockPrice);
            return Ok(stocks);
        }

        //[HttpGet("ws")]
        //public async Task GetStockUpdates()
        //{
        //    if (HttpContext.WebSockets.IsWebSocketRequest)
        //    {
        //        WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        //        _webSocketHandler.AddWebSocket(webSocket);
        //        await _webSocketHandler.HandleWebSocketAsync(webSocket);
        //    }
        //    else
        //    {
        //        HttpContext.Response.StatusCode = 400;
        //    }
        //}
    }
}
