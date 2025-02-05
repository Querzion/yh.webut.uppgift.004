using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class CustomerEntity : IEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string CustomerName { get; set; } = null!;
    
    // public virtual ICollection<ProjectEntity> Projects { get; set; } = null!;
}