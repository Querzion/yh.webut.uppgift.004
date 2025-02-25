using Business.Dtos;

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
    Task<IResult> GetProjectByIdAsync(int id);
    Task<IResult> GetProjectByNameAsync(string projectName);
    Task<IResult> UpdateProjectAsync(int id, ProjectUpdateForm updateForm);
    Task<IResult> DeleteProjectAsync(int id);
    Task<IResult> CheckIfProjectExists(string projectName);
}