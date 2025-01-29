using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
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
        // Check if the product exists
        var product = await _productRepository.GetAsync(x => x.ProductName == form.ProductName);
        // If not Create product
        product ??= await _productRepository.CreateAsync(ProductFactory.Create(form));
        
        return ProductFactory.Create(product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(ProductFactory.Create);
    }

    public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        var entity = await _productRepository.GetAsync(expression);
        var product = ProductFactory.Create(entity);
        return product ?? null!;
    }

    public async Task<Product> UpdateProductAsync(ProductUpdateForm form)
    {
        var entity = await _productRepository.UpdateAsync(ProductFactory.Create(form));
        var product = ProductFactory.Create(entity);
        return product ?? null!;
    }

    public async  Task<bool> DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckIfProductExistsAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        return await _productRepository.AlreadyExistsAsync(expression);
    }
}