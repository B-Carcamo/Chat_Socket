
public partial class Program
{
 public static async Task Main(string[] args)
    {
        using (var context = new ChatContext())
        {
            context.Database.EnsureCreated();
        }

        Server server = new Server();
        await server.Start();
    }
}