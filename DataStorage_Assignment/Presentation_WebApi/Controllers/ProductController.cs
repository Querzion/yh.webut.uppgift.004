using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController(IProductRepository productRepository) : Controller
{
    private readonly IProductRepository _productRepository = productRepository;
}