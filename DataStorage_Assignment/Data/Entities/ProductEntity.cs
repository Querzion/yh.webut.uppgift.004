using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(150)")]
    public string ServiceName { get; set; } = null!;
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
}