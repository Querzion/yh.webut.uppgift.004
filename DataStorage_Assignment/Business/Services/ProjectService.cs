using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    
    public async Task<IResult> CreateProjectAsync(ProjectRegistrationForm registrationForm)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> GetAllProjectsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> GetProjectAsync(Expression<Func<Project, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> UpdateProjectAsync(ProjectUpdateForm updateForm)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> DeleteProjectAsync(Expression<Func<Project, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> CheckIfProjectExists(Expression<Func<Project, bool>> expression)
    {
        throw new NotImplementedException();
    }
}