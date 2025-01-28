using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProductUpdateForm
{
    [Required]
    public int Id { get; set; }
    
    [Required (ErrorMessage = "Product name is required")]
    [MinLength(2, ErrorMessage = "Product name must be at least 2 characters")]
    public string ProductName { get; set; } = null!;
    
    [Required (ErrorMessage = "Product price is required")]
    public decimal Price { get; set; }
}