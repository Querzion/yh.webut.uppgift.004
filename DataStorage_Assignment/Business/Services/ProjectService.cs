using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<IResult> CreateProjectAsync(ProjectRegistrationForm registrationForm)
    {
        if (registrationForm == null)
            return Result.BadRequest("Invalid project registration.");
        
        try
        {
            if (await _projectRepository.AlreadyExistsAsync(x => x.Title == registrationForm.Title))
                return Result.BadRequest("Project already exists.");
            
            var projectEntity = ProjectFactory.Create(registrationForm);
            var result = await _projectRepository.CreateAsync(projectEntity);
            return result ? Result.Ok() : Result.Error("Project already exists.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        var project = projects?.Select(ProjectFactory.Create);
        return Result<IEnumerable<Project>>.Ok(project);
    }

    public async Task<IResult> GetProjectByIdAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectEntity == null)
            return Result.NotFound("Project not found.");
        
        var project = ProjectFactory.Create(projectEntity);

        // This is a ChatGPT thing. I don't think it's going to work at all.
        // Lazy loading will automatically fetch the related 'Customer' entity here
        var customerName = projectEntity.Customer?.CustomerName;
        
        return Result<Project>.Ok(project);
    }

    public async Task<IResult> GetProjectByNameAsync(string projectName)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Title == projectName);
        if (projectEntity == null)
            return Result.NotFound("Project not found.");
        
        var project = ProjectFactory.Create(projectEntity);
        
        // This is a ChatGPT thing. I don't think it's going to work at all.
        // Lazy loading will automatically fetch the related 'Customer' entity here
        var customerName = projectEntity.Customer?.CustomerName;
        
        return Result<Project>.Ok(project);
    }

    public async Task<IResult> UpdateProjectAsync(int id, ProjectUpdateForm updateForm)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectEntity == null)
            return Result.NotFound("Project not found.");
        
        try
        {
            projectEntity = ProjectFactory.Create(projectEntity, updateForm);
            var result = await _projectRepository.UpdateAsync(projectEntity);
            return result ? Result.Ok() : Result.Error("Project was not updated.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> DeleteProjectAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectEntity == null)
            return Result.NotFound("Project not found.");
        
        try
        {
            var result = await _projectRepository.DeleteAsync(projectEntity);
            return result ? Result.Ok() : Result.Error("Project was not deleted.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> CheckIfProjectExists(string projectName)
    {
        var entity = await _projectRepository.GetAsync(x => x.Title == projectName);
        if (entity == null)
            return Result.NotFound("Project not found.");
        
        try
        {
            var project = ProjectFactory.Create(entity);
            return Result<Project>.Ok(project);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
}