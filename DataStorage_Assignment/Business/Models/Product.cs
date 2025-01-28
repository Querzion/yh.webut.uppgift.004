namespace Business.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
}