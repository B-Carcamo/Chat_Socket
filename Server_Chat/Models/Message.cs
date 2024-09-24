using Microsoft.EntityFrameworkCore;

public class Message {

    public int Id { get; set; }

    public required string Sender { get; set; }

    public required string Receiver { get; set; }

    public required string MessageText { get; set; }

    public DateTime SentAt { get; set; }

}


public class ChatContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=chatSocket;User Id=sa;Password=yourpassword;TrustServerCertificate=True;");
    }
}