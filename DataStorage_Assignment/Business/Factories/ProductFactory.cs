using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProductFactory
{
    // public static ProductRegistrationForm CreateRegistrationForm() => new();

    public static ProductEntity Create(ProductRegistrationForm registrationForm) => new()
    {
        ProductName = registrationForm.ProductName,
        Price = registrationForm.Price
    };

    public static Product Create(ProductEntity entity) => new()
    {
        Id = entity.Id,
        ProductName = entity.ProductName,
        Price = entity.Price
    };
    
    public static ProductUpdateForm Create(Product product) => new()
    {
        Id = product.Id,
        ProductName = product.ProductName,
        Price = product.Price
    };

    public static ProductEntity Create(ProductEntity productEntity, ProductUpdateForm updateForm) => new()
    {
        Id = productEntity.Id,
        ProductName = updateForm.ProductName,
        Price = updateForm.Price
    };
}