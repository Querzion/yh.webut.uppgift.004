using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Business.Dtos;

public class CustomerRegistrationForm
{
    [Required (ErrorMessage = "Customer name is required")]
    [MinLength(2, ErrorMessage = "Customer name must be at least 2 characters")]
    public string CustomerName { get; set; } = null!;
}