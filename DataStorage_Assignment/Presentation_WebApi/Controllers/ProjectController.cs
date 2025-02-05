using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/v1/projects")]
public class ProjectController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    
    
}