using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Data.Helpers;

namespace Tests.Data.Repositories;

public class CustomerRepository_Tests
{
    private readonly DataContext _context;
    private readonly CustomerRepository _customerRepository;

    public CustomerRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseLazyLoadingProxies()
            .Options;
        
        _context = new DataContext(options);
        _customerRepository = new CustomerRepository(_context);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var entity = TestData.MakeTestCustomer();
        
        // Act
        var result = await _customerRepository.CreateAsync(entity);

        // Assert
        Assert.True(result);
    }
}