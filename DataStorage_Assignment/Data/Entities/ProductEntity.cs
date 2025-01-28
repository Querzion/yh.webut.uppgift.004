using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class ProductEntity : IEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(150)")]
    public string ProductName { get; set; } = null!;
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    
    public virtual ICollection<ProjectEntity> Projects { get; set; } = null!;
}