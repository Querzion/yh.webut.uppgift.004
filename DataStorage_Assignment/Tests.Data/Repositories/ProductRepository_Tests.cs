using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Data.Helpers;

namespace Tests.Data.Repositories;

public class ProductRepository_Tests
{
    private readonly DataContext _context;
    private readonly ProductRepository _productRepository;

    public ProductRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseLazyLoadingProxies()
            .Options;
        
        _context = new DataContext(options);
        _productRepository = new ProductRepository(_context);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var entity = TestData.MakeTestProduct();
        
        // Act
        var result = await _productRepository.CreateAsync(entity);

        // Assert
        Assert.True(result);
    }
}