using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProductService
{
    // Without IResult
    // Task<Product> CreateProductAsync(ProductRegistrationForm form);
    // Task<IEnumerable<Product>> GetAllProductsAsync();
    // Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression);
    // Task<Product> UpdateProductAsync(ProductUpdateForm form);
    // Task<bool> DeleteProductAsync(int id);
    // Task<bool> CheckIfProductExistsAsync(Expression<Func<ProductEntity, bool>> expression);
    
    // With IResult
    Task<IResult> CreateProductAsync(ProductRegistrationForm registrationForm);
    Task<IResult> GetAllProductsAsync();
    Task<IResult> GetProductAsync(Expression<Func<Product, bool>> expression);
    Task<IResult> UpdateProductAsync(ProductUpdateForm updateForm);
    Task<IResult> DeleteProductAsync(Expression<Func<Product, bool>> expression);
    Task<IResult> CheckIfProductExists(Expression<Func<Product, bool>> expression);
}