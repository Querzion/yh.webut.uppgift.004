using Business.Dtos;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/status-types")]
public class StatusTypeController(IStatusTypeService statusTypeService) : Controller
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    
    // POST api/status-types
    [HttpPost]
    public async Task<IActionResult> CreateStatus([FromBody] StatusTypeRegistrationForm registrationForm)
    {
        var result = await _statusTypeService.CreateStatusTypeAsync(registrationForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return BadRequest(result); // Return 400 Bad Request with error message
    }

    // GET api/status-types
    [HttpGet]
    public async Task<IActionResult> GetAllStatusTypes()
    {
        var result = await _statusTypeService.GetAllStatusTypesAsync();
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/status-types/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStatusById(int id)
    {
        var result = await _statusTypeService.GetStatusTypeByIdAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/status-types/name/{customerName}
    [HttpGet("name/{statusType}")]
    public async Task<IActionResult> GetStatusTypeByName(string statusName)
    {
        var result = await _statusTypeService.GetStatusByStatusNameAsync(statusName);
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // PUT api/status-types/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatusType(int id, [FromBody] StatusTypeUpdateForm updateForm)
    {
        var result = await _statusTypeService.UpdateStatusTypeAsync(id, updateForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // DELETE api/status-types/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStatusType(int id)
    {
        var result = await _statusTypeService.DeleteStatusTypeAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/status-types/check/{statusType}
    [HttpGet("check/{statusType}")]
    public async Task<IActionResult> CheckIfStatusTypeExists(string statusType)
    {
        var result = await _statusTypeService.CheckIfStatusExists(statusType);
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }
}