using System.Diagnostics;
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
    
    public async Task<IResult> CreateProductAsync(ProductRegistrationForm registrationForm)
    {
        if (registrationForm == null)
            return Result.BadRequest("Invalid product registration.");
        
        await _productRepository.BeginTransactionAsync();

        try
        {
            var product = ProductFactory.CreateEntityFrom(registrationForm);

            if (await _productRepository.AlreadyExistsAsync(x => x.ProductName == registrationForm.ProductName))
            {
                await _productRepository.RollbackTransactionAsync();
                return Result.AlreadyExists("Product/Service with this name already exists");
            }
            
            var result = await _productRepository.CreateAsync(product);

            if (result)
            {
                await _productRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _productRepository.RollbackTransactionAsync();
                return Result.Error("Failed to create product");
            }
        }
        catch (Exception ex)
        {
            await _productRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> GetAllProductsAsync()
    {
        var productEntities = await _productRepository.GetAllAsync();
        var product = productEntities?.Select(ProductFactory.CreateOutputModel);
        return Result<IEnumerable<Product>>.Ok(product);
    }

    public async Task<IResult> GetProductByIdAsync(int id)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == id);
        if (productEntity == null)
            return Result.NotFound("Product not found");
        
        var product = ProductFactory.CreateOutputModelFrom(productEntity);
        return Result<Product>.Ok(product);
    }

    public async Task<IResult> GetProductByNameAsync(string productName)
    {
        var productEntity = await _productRepository.GetAsync(x => x.ProductName == productName);
        if (productEntity == null)
            return Result.NotFound("Product not found");
        
        var product = ProductFactory.CreateOutputModelFrom(productEntity);
        return Result<Product>.Ok(product);
    }

    public async Task<IResult> UpdateProductAsync(int id, ProductUpdateForm updateForm)
    {
        await _productRepository.BeginTransactionAsync();

        try
        {
            var productEntity = await _productRepository.GetAsync(x => x.Id == id);
            if (productEntity == null)
            {
                await _productRepository.RollbackTransactionAsync();
                return Result.NotFound("Product not found");
            }

            productEntity = ProductFactory.Update(productEntity, updateForm);
        
            var result = await _productRepository.UpdateAsync(productEntity);

            if (result)
            {
                await _productRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _productRepository.RollbackTransactionAsync();
                return Result.Error("Failed to update product");
            }
        }
        catch (Exception ex)
        {
            await _productRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> DeleteProductAsync(int id)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == id);
        if (productEntity == null)
            return Result.NotFound("Product not found.");

        try
        {
            var result = await _productRepository.DeleteAsync(productEntity);
            return result ? Result.Ok() : Result.Error("Product not found.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> CheckIfProductExists(string productName)
    {
        var entity = await _productRepository.GetAsync(x => x.ProductName == productName);
        if (entity == null)
            return Result.NotFound("Product not found");
        
        try
        {
            var product  = ProductFactory.CreateOutputModelFrom(entity);
            return Result<Product>.Ok(product);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
    
    // public async Task<Product> CreateProductAsync(ProductRegistrationForm form)
    // {
    //     // Check if the product exists
    //     var product = await _productRepository.GetAsync(x => x.ProductName == form.ProductName);
    //     // If not Create product
    //     product ??= await _productRepository.CreateAsync(ProductFactory.Create(form));
    //     
    //     return ProductFactory.Create(product);
    // }
    //
    // public async Task<IEnumerable<Product>> GetAllProductsAsync()
    // {
    //     var products = await _productRepository.GetAllAsync();
    //     return products.Select(ProductFactory.Create);
    // }
    //
    // public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    // {
    //     var entity = await _productRepository.GetAsync(expression);
    //     var product = ProductFactory.Create(entity);
    //     return product ?? null!;
    // }
    //
    // public async Task<Product> UpdateProductAsync(ProductUpdateForm form)
    // {
    //     var entity = await _productRepository.UpdateAsync(ProductFactory.Create(form));
    //     var product = ProductFactory.Create(entity);
    //     return product ?? null!;
    // }
    //
    // public async  Task<bool> DeleteProductAsync(int id)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<bool> CheckIfProductExistsAsync(Expression<Func<ProductEntity, bool>> expression)
    // {
    //     return await _productRepository.AlreadyExistsAsync(expression);
    // }
}