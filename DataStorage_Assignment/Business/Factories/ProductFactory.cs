using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProductFactory
{
    public static ProductRegistrationForm Create() => new();

    public static ProductEntity Create(ProductRegistrationForm form) => new()
    {
        ProductName = form.ProductName,
        Price = form.Price
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

    public static ProductEntity Create(ProductUpdateForm form) => new()
    {
        Id = form.Id,
        ProductName = form.ProductName,
        Price = form.Price
    };
}