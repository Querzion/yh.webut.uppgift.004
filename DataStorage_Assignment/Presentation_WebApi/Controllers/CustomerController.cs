using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController(ICustomerService customerService) : Controller
{
    private readonly ICustomerService _customerService = customerService;

    /// <summary>
    /// I fed the whole customerservice content to ChatGPT, since there were some strange code I couldn't fully grasp.
    /// Because of this I also got a bit of a heads ups on a bunch of the commands, and I can create similar functionality
    /// for the rest of the controllers that I need to create.
    /// </summary>
    /// <param name="registrationForm"></param>
    /// <returns></returns>
    
    
    // POST api/customer
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerRegistrationForm registrationForm)
    {
        var result = await _customerService.CreateCustomerAsync(registrationForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return BadRequest(result); // Return 400 Bad Request with error message
    }

    // GET api/customer
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var result = await _customerService.GetAllCustomersAsync();
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/customer/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var result = await _customerService.GetCustomerByIdAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/customer/name/{customerName}
    [HttpGet("name/{customerName}")]
    public async Task<IActionResult> GetCustomerByName(string customerName)
    {
        var result = await _customerService.GetCustomerByNameAsync(customerName);
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // PUT api/customer/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateForm updateForm)
    {
        var result = await _customerService.UpdateCustomerAsync(id, updateForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // DELETE api/customer/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/customer/check/{customerName}
    [HttpGet("check/{customerName}")]
    public async Task<IActionResult> CheckIfCustomerExists(string customerName)
    {
        var result = await _customerService.CheckIfCustomerExists(customerName);
        if (result.Success)
            return Ok(result); // Return 200 OK with customer data
        return NotFound(result); // Return 404 Not Found with error message
    }
    
    
    // [HttpPost] // Based on Exercise 3 (With the help of ChatGPT).
    // public async Task<IActionResult> Create(CustomerRegistrationForm registrationForm)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest();
    //
    //     var customer = await _customerService.GetCustomerByNameAsync(registrationForm.CustomerName);
    //     if (customer != null)
    //         return Conflict("Customer with the same name already exists.");
    //     
    //     var result = await _customerService.CreateCustomerAsync(registrationForm);
    //     if (result.Success)
    //         return Ok();
    //
    //     return Problem(result.ErrorMessage);
    // }
    //
    // [HttpGet] // ChatGPT Trying to help out.
    // public async Task<IActionResult> GetAll()
    // {
    //     var result = await _customerService.GetAllCustomersAsync();
    //
    //     // Check if the result is successful and contains valid customers
    //     if (result.Success && result.Value.Any())
    //     {
    //         return Ok(result.Value);
    //     }
    //
    //     // If not found, return an empty list
    //     return NotFound(new List<Customer>());
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetById(int id)
    // {
    //     var customer = await _customerService.GetCustomerByIdAsync(id);
    //     return customer != null ? Ok(customer) : NotFound();
    // }
    //
    // [HttpPut]
    // public async Task<IActionResult> Update(CustomerUpdateForm form)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest();
    //
    //     var existing = await _customerService.GetCustomerByIdAsync(form.Id);
    //     if (existing == null)
    //         return NotFound();
    //
    //     var customer = await _customerService.UpdateCustomerAsync(form);
    //     return Ok(customer);
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     if (id <= 0)
    //         return BadRequest();
    //
    //     var customer = await _customerService.GetCustomerByIdAsync(id);
    //     if (customer == null)
    //         return NotFound();
    //
    //     var result = await _customerService.DeleteCustomerAsync(id);
    //     return result ? Ok(result) : Problem();
    // }
}