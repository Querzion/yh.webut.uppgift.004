using Business.Dtos;
using Business.Interfaces;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductService productService) : Controller
{
    // POST api/products
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRegistrationForm registrationForm)
    {
        var result = await productService.CreateProductAsync(registrationForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return BadRequest(result); // Return 400 Bad Request with error message
    }

    // GET api/products
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await productService.GetAllProductsAsync();
        if (result.Success)
            return Ok(result); // Return 200 OK with Product data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await productService.GetProductByIdAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with Product data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/products/name/{productName}
    [HttpGet("name/{productName}")]
    public async Task<IActionResult> GetProductByName(string productName)
    {
        var result = await productService.GetProductByNameAsync(productName);
        if (result.Success)
            return Ok(result); // Return 200 OK with Product data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // PUT api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateForm updateForm)
    {
        var result = await productService.UpdateProductAsync(id, updateForm);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // DELETE api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await productService.DeleteProductAsync(id);
        if (result.Success)
            return Ok(result); // Return 200 OK with result data
        return NotFound(result); // Return 404 Not Found with error message
    }

    // GET api/products/check/{customerName}
    [HttpGet("check/{email}")]
    public async Task<IActionResult> CheckIfCustomerExists(string email)
    {
        var result = await productService.CheckIfProductExists(email);
        if (result.Success)
            return Ok(result); // Return 200 OK with Product data
        return NotFound(result); // Return 404 Not Found with error message
    }
}