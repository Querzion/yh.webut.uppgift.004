namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    
    public string CustomerName { get; set; } = null!;
    public string StatusName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    
    // public Customer Customer { get; set; } = null!;
    // public StatusType Status { get; set; } = null!;
    // public User User { get; set; } = null!;
    // public Product Product { get; set; } = null!;
}