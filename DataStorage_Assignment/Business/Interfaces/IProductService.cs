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
    Task<IResult> CreateUserAsync(UserRegistrationForm registrationForm);
    Task<IResult> GetAllUsersAsync();
    Task<IResult> GetUserByIdAsync(int id);
    Task<IResult> GetUserByEmailAsync(string email);
    Task<IResult> UpdateUserAsync(int id, UserUpdateForm updateForm);
    Task<IResult> DeleteUserAsync(int id);
    Task<IResult> CheckIfUserExists(string email);
}