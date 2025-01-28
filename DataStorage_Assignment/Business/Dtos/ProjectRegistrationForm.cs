using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    [Required (ErrorMessage = "Project title is required")]
    [MinLength(2, ErrorMessage = "Title must be at least 2 characters")]
    public string Title { get; set; } = null!;
    
    public string? Description { get; set; }
    
    // Isn't this set in the entity to the database? 
    [Required (ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int StatusId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ProductId { get; set; }
}