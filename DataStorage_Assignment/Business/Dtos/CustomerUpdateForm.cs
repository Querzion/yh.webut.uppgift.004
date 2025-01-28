using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class CustomerUpdateForm
{
    [Required]
    public int Id { get; set; }
    
    [Required (ErrorMessage = "Customer name is required")]
    [MinLength(2, ErrorMessage = "Customer name must be at least 2 characters")]
    public string CustomerName { get; set; } = null!;
}