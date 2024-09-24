using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;
class Program
{
    private static ClientWebSocket ws = new ClientWebSocket();
    private static string username;

    static async Task Main(string[] args)
    {
        Console.Write("Ingrese su nombre de usuario: ");
        username = Console.ReadLine();

        await ConnectToServer();
        await GetMessageHistory();

        var receiveTask = ReceiveMessages();

        while (true)
        {
            string message = Console.ReadLine();
            if (message.ToLower() == "exit")
                break;

            await SendMessage(message);
        }

        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing connection", CancellationToken.None);
    }

    static async Task ConnectToServer()
    {
        await ws.ConnectAsync(new Uri("ws://localhost:4040"), CancellationToken.None);
        Console.WriteLine("Conectado al servidor.");
    }

    static async Task GetMessageHistory()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetStringAsync("http://localhost:4040/history");
            var messages = JsonConvert.DeserializeObject<List<Message>>(response);

            Console.WriteLine("Historial de mensajes:");
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.SentAt } - {message.Sender}: {message.MessageText}");
            }
            Console.WriteLine("Fin del historial.");
        }
    }

    static async Task SendMessage(string message)
    {
        var bytes = Encoding.UTF8.GetBytes($"{username}: {message}");
        await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    static async Task ReceiveMessages()
    {
        var buffer = new byte[1024 * 4];

        try
        {
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine(message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            if (ws.State != WebSocketState.Closed)
            {
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
        }
    }
}