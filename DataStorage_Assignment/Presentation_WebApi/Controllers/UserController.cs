using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;
    
}