using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

public class Server
{
    private readonly List<WebSocket> _clients = new List<WebSocket>();
    private readonly ChatContext _context;

    public Server()
    {
        _context = new ChatContext();
    }

    public async Task Start()
    {
        var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:4040/");
        listener.Start();
        Console.WriteLine("Servidor iniciado en http://localhost:4040/");

        while (true)
        {
            HttpListenerContext context = await listener.GetContextAsync();

            if (context.Request.Url.LocalPath == "/history")
            {
                await HandleHistoryRequest(context);
            }
            else if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext wsContext = await context.AcceptWebSocketAsync(null);
                WebSocket client = wsContext.WebSocket;
                _clients.Add(client);

                _ = HandleClientAsync(client);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    private async Task HandleHistoryRequest(HttpListenerContext context)
    {
        var messages = await _context.Messages
            .OrderByDescending(m => m.SentAt)
            .Take(50)
            .ToListAsync();

        var json = JsonConvert.SerializeObject(messages);
        var buffer = Encoding.UTF8.GetBytes(json);

        context.Response.ContentType = "application/json";
        await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        context.Response.Close();
    }

    private async Task HandleClientAsync(WebSocket client)
    {
        var buffer = new byte[1024 * 4];
        var segment = new ArraySegment<byte>(buffer);

        try
        {
            while (client.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await client.ReceiveAsync(segment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    break;
                }

                string messageText = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var parts = messageText.Split(new[] { ':' }, 2);
                string sender = parts[0].Trim();
                string content = parts[1].Trim();

                var message = new Message
                {
                    Sender = sender,
                    Receiver = "all", // Para simplificar, asumimos que todos los mensajes son para todos
                    MessageText = content,
                    SentAt = DateTime.UtcNow
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                await BroadcastMessageAsync(client, messageText);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cliente Desconectado: {ex.Message}");
        }
        finally
        {
            _clients.Remove(client);
        }
    }

    private async Task BroadcastMessageAsync(WebSocket sender, string message)
    {
        var tasks = _clients
            .Where(c => c != sender && c.State == WebSocketState.Open)
            .Select(c => SendMessageAsync(c, message));

        await Task.WhenAll(tasks);
    }

    private async Task SendMessageAsync(WebSocket client, string message)
    {
        var bytes = Encoding.UTF8.GetBytes(message);
        await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

}