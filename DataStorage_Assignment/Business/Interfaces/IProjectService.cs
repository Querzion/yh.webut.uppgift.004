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
    Task<IResult> CreateUserAsync(UserRegistrationForm registrationForm);
    Task<IResult> GetAllUsersAsync();
    Task<IResult> GetUserByIdAsync(int id);
    Task<IResult> GetUserByEmailAsync(string email);
    Task<IResult> UpdateUserAsync(int id, UserUpdateForm updateForm);
    Task<IResult> DeleteUserAsync(int id);
    Task<IResult> CheckIfUserExists(string email);
}