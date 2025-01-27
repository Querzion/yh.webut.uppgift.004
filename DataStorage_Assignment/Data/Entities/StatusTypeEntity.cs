using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class StatusTypeEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string StatusName { get; set; } = null!;
    
    public virtual ICollection<ProjectEntity> Projects { get; set; } = null!;
}