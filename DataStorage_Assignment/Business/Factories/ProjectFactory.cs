using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();

    // Create a new ProjectEntity based of the ProjectRegistrationForm, linked as form.
    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
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

    public static Project Create(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description ?? string.Empty,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        CustomerId = entity.CustomerId,
        StatusId = entity.StatusId,
        UserId = entity.UserId,
        ProductId = entity.ProductId,
        CustomerName = entity.Customer.CustomerName,
        StatusName = entity.Status.StatusName,
        UserName = $"{entity.User.FirstName} {entity.User.LastName}",
        ProductName = entity.Product.ProductName
    };

    public static ProjectUpdateForm Create(Project project) => new()
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

    public static ProjectEntity Create(ProjectEntity projectEntity, ProjectUpdateForm updateForm) => new()
    {
        Id = projectEntity.Id,
        Title = updateForm.Title,
        Description = updateForm.Description,
        StartDate = updateForm.StartDate,
        EndDate = updateForm.EndDate,
        CustomerId = updateForm.CustomerId,
        StatusId = updateForm.StatusId,
        UserId = updateForm.UserId,
        ProductId = updateForm.ProductId
    };
}