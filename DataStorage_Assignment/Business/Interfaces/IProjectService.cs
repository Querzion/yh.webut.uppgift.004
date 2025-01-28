using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<Project> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<Project> UpdateProjectAsync(ProjectUpdateForm form);
    Task<bool> DeleteProjectAsync(int id);
    Task<bool> CheckIfProjectExistsAsync(Expression<Func<ProjectEntity, bool>> expression);
}