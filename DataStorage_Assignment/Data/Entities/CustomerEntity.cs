using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string CustomerName { get; set; } = null!;
}