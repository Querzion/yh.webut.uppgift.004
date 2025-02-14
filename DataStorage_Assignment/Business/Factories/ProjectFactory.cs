using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm CreateRegistrationForm() => new();
    public static ProjectUpdateForm CreateUpdateForm() => new();

    // Create a new ProjectEntity based of the ProjectRegistrationForm, linked as form.
    public static ProjectEntity CreateEntityFrom(ProjectRegistrationForm form) => new()
    {
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        CustomerId = form.CustomerId,
        StatusId = form.StatusId,
        UserId = form.UserId,
        ProductId = form.ProductId
    };
    public static Project CreateOutputModel(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        CustomerId = entity.CustomerId,
        StatusId = entity.StatusId,
        UserId = entity.UserId,
        ProductId = entity.ProductId,
        // CustomerName = entity.Customer.CustomerName,
        // StatusName = entity.Status.StatusName,
        // UserName = $"{entity.User.FirstName} {entity.User.LastName}",
        // ProductName = entity.Product.ProductName
        
        // CustomerName = entity.Customer?.CustomerName ?? "Unknown Customer",
        // StatusName = entity.Status?.StatusName ?? "Unknown Status",
        // UserName = entity.User != null ? $"{entity.User.FirstName} {entity.User.LastName}" : "Unknown User",
        // ProductName = entity.Product?.ProductName ?? "Unknown Product"
        
        // Customer = entity.Customer.CustomerName,
        // Status = entity.Status.StatusName,
        // User = $"{entity.User.FirstName} {entity.User.LastName}",
        // Product = entity.Product.ProductName
        
        // Customer = entity.Customer?.CustomerName ?? "Unknown Customer",
        // Status = entity.Status.StatusName ?? "Unknown Status",  // Ensure Status is not null
        // User = entity.User != null ? $"{entity.User.FirstName} {entity.User.LastName}" : "Unknown User",
        // Product = entity.Product?.ProductName ?? "Unknown Product",
    };
    public static Project CreateOutputModelFrom(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        CustomerId = entity.CustomerId,
        StatusId = entity.StatusId,
        UserId = entity.UserId,
        ProductId = entity.ProductId,
        // CustomerName = entity.Customer.CustomerName,
        // StatusName = entity.Status.StatusName,
        // UserName = $"{entity.User.FirstName} {entity.User.LastName}",
        // ProductName = entity.Product.ProductName
    };

    public static ProjectUpdateForm CreateUpdateForm(Project project) => new()
    {
        Id = project.Id,
        Title = project.Title,
        Description = project.Description,
        StartDate = project.StartDate,
        EndDate = project.EndDate,
        CustomerId = project.CustomerId,
        StatusId = project.StatusId,
        UserId = project.UserId,
        ProductId = project.ProductId
    };
    
    public static ProjectEntity Update(ProjectEntity projectEntity, ProjectUpdateForm updateForm)
    {
        projectEntity.Id = projectEntity.Id;
        projectEntity.Title = updateForm.Title;
        projectEntity.Description = updateForm.Description;
        projectEntity.StartDate = updateForm.StartDate;
        projectEntity.EndDate = updateForm.EndDate;
        projectEntity.CustomerId = updateForm.CustomerId;
        projectEntity.StatusId = updateForm.StatusId;
        projectEntity.UserId = updateForm.UserId;
        projectEntity.ProductId = updateForm.ProductId;

        return projectEntity;
    }

    // public static ProjectEntity Create(ProjectEntity projectEntity, ProjectUpdateForm updateForm) => new()
    // {
    //     Id = projectEntity.Id,
    //     Title = updateForm.Title,
    //     Description = updateForm.Description,
    //     StartDate = updateForm.StartDate,
    //     EndDate = updateForm.EndDate,
    //     CustomerId = updateForm.CustomerId,
    //     StatusId = updateForm.StatusId,
    //     UserId = updateForm.UserId,
    //     ProductId = updateForm.ProductId
    // };
}
