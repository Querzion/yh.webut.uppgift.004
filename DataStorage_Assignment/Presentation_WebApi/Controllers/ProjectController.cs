using Business.Dtos;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    
    // POST api/projects
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectRegistrationForm registrationForm)
    {
        var result = await _projectService.CreateProjectAsync(registrationForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return BadRequest(result); // Return 400 Bad Request with error message
    }

    // GET api/projects
    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var result = await _projectService.GetAllProjectsAsync();
        if (result.Success)
            return Ok(result); // Return 200 OK with Project data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/projects/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var result = await _projectService.GetProjectByIdAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with Project data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/projects/name/{projectName}
    [HttpGet("name/{projectName}")]
    public async Task<IActionResult> GetProjectByName(string projectName)
    {
        var result = await _projectService.GetProjectByNameAsync(projectName);
        if (result.Success)
            return Ok(result); // Return 200 OK with Project data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // PUT api/projects/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectUpdateForm updateForm)
    {
        var result = await _projectService.UpdateProjectAsync(id, updateForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // DELETE api/projects/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var result = await _projectService.DeleteProjectAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/projects/check/{projectName}
    [HttpGet("check/{projectName}")]
    public async Task<IActionResult> CheckIfProjectExists(string projectName)
    {
        var result = await _projectService.CheckIfProjectExists(projectName);
        if (result.Success)
            return Ok(result); // Return 200 OK with Project data
        return NotFound(result); // Return 404 Not Found with error message
    }
}