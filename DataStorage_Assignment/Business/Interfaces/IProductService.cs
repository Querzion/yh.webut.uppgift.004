using Business.Dtos;

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
    Task<IResult> GetProductByIdAsync(int id);
    Task<IResult> GetProductByNameAsync(string productName);
    Task<IResult> UpdateProductAsync(int id, ProductUpdateForm updateForm);
    Task<IResult> DeleteProductAsync(int id);
    Task<IResult> CheckIfProductExists(string productName);
}