using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class UserEntity : IEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string LastName { get; set; } = null!;
    
    [Required]
    [Column(TypeName = "NVARCHAR(150)")]
    public string Email { get; set; } = null!;
    
    public virtual ICollection<ProjectEntity> Projects { get; set; } = null!;
}