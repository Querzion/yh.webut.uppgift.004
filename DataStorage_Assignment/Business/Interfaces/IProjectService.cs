using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProjectService
{
    // Without IResult
    // Task<Project> CreateProjectAsync(ProjectRegistrationForm form);
    // Task<IEnumerable<Project>> GetAllProjectsAsync();
    // Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    // Task<Project> UpdateProjectAsync(ProjectUpdateForm form);
    // Task<bool> DeleteProjectAsync(int id);
    // Task<bool> CheckIfProjectExistsAsync(Expression<Func<ProjectEntity, bool>> expression);
    
    // With IResult
    Task<IResult> CreateProjectAsync(ProjectRegistrationForm registrationForm);
    Task<IResult> GetAllProjectsAsync();
    Task<IResult> GetProjectAsync(Expression<Func<Project, bool>> expression);
    Task<IResult> UpdateProjectAsync(ProjectUpdateForm updateForm);
    Task<IResult> DeleteProjectAsync(Expression<Func<Project, bool>> expression);
    Task<IResult> CheckIfProjectExists(Expression<Func<Project, bool>> expression);
}