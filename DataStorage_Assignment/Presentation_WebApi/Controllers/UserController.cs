using Business.Dtos;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;
    
    // POST api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRegistrationForm registrationForm)
    {
        var result = await _userService.CreateUserAsync(registrationForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return BadRequest(result); // Return 400 Bad Request with error message
    }

    // GET api/users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();
        if (result.Success)
            return Ok(result); // Return 200 OK with User data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with User data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/users/name/{userName}
    [HttpGet("name/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var result = await _userService.GetUserByEmailAsync(email);
        if (result.Success)
            return Ok(result); // Return 200 OK with User data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // PUT api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateForm updateForm)
    {
        var result = await _userService.UpdateUserAsync(id, updateForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // DELETE api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/users/check/{userName}
    [HttpGet("check/{email}")]
    public async Task<IActionResult> CheckIfUserExists(string email)
    {
        var result = await _userService.CheckIfUserExists(email);
        if (result.Success)
            return Ok(result); // Return 200 OK with User data
        return NotFound(result); // Return 404 Not Found with error message
    }
}