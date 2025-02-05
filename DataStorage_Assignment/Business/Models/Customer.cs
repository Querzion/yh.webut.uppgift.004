namespace Business.Models;

public class Customer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
}

// public record Customer(int Id, string CustomerName);