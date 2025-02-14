using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProductFactory
{
    public static ProductRegistrationForm CreateRegistrationForm() => new();
    public static ProductUpdateForm CreateUpdateForm() => new();

    public static ProductEntity CreateEntityFrom(ProductRegistrationForm registrationForm) => new()
    {
        ProductName = registrationForm.ProductName,
        Price = registrationForm.Price
    };

    public static Product CreateOutputModel(ProductEntity entity) => new()
    {
        Id = entity.Id,
        ProductName = entity.ProductName,
        Price = entity.Price
    };
    public static Product CreateOutputModelFrom(ProductEntity entity) => new()
    {
        Id = entity.Id,
        ProductName = entity.ProductName,
        Price = entity.Price
    };
    
    public static ProductUpdateForm CreateUpdateForm(Product product) => new()
    {
        Id = product.Id,
        ProductName = product.ProductName,
        Price = product.Price
    };

    public static ProductEntity Update(ProductEntity productEntity, ProductUpdateForm updateForm)
    {
        productEntity.Id = productEntity.Id;
        productEntity.ProductName = updateForm.ProductName;
        productEntity.Price = updateForm.Price;

        return productEntity;
    }
}