using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(150)")]
    public string Title { get; set; } = null!;
    
    [Column(TypeName = "TEXT")]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }
    
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    
    public int StatusId { get; set; }
    public StatusTypeEntity Status { get; set; } = null!;
    
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
}