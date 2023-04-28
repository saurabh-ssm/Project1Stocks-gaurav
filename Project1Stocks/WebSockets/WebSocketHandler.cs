using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Project1Stocks.Data;

namespace Project1Stocks.WebSockets
{
    public class WebSocketHandler
    {
        private readonly ConcurrentDictionary<string, WebSocket> _webSockets = new ConcurrentDictionary<string, WebSocket>();
        private readonly ILogger<WebSocketHandler> _logger;
        private readonly ApplicationDbContext _dbContext;

        public WebSocketHandler(ILogger<WebSocketHandler> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void AddWebSocket(WebSocket webSocket)
        {
            _webSockets.TryAdd(webSocket.GetHashCode().ToString(), webSocket);
        }

        public async Task HandleWebSocketAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var webSocketId = webSocket.GetHashCode().ToString();

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    _webSockets.TryRemove(webSocketId, out _);
                }
            }
        }

        public async Task SendUpdateAsync()
        {
            var stocks = await _dbContext.Companies.ToDictionaryAsync(c => c.Name, c => c.StockPrice);

            var message = new WebSocketMessage
            {
                MessageType = WebSocketMessageType.Text,
                Data = JsonConvert.SerializeObject(stocks)
            };

            var serializedMessage = JsonConvert.SerializeObject(message);
            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(serializedMessage));

            foreach (var webSocket in _webSockets.Values.ToList())
            {
                try
                {
                    await webSocket.SendAsync(buffer, message.MessageType, true, CancellationToken.None);
                }
                catch (WebSocketException ex)
                {
                    _logger.LogError(ex, "Failed to send websocket message.");
                    _webSockets.TryRemove(webSocket.GetHashCode().ToString(), out _);
                }
            }
        }
    }

    public class WebSocketMessage
    {
        public WebSocketMessageType MessageType { get; set; }
        public string Data { get; set; }
    }


}
