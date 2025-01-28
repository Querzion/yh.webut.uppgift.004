using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    
    public async Task<Product> CreateProductAsync(ProductRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> UpdateProductAsync(ProductUpdateForm form)
    {
        throw new NotImplementedException();
    }

    public async  Task<bool> DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckIfProductExistsAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        return await _productRepository.AnyAsync(expression);
    }
}