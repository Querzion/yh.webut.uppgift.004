using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Data.Helpers;

namespace Tests.Data.Repositories;

public class StatusTypeRepository_Tests
{
    private readonly DataContext _context;
    private readonly StatusTypeRepository _statusTypeRepository;

    public StatusTypeRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseLazyLoadingProxies()
            .Options;
        
        _context = new DataContext(options);
        _statusTypeRepository = new StatusTypeRepository(_context);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var entity = TestData.MakeTestStatusType();
        
        // Act
        var result = await _statusTypeRepository.CreateAsync(entity);
        
        // Assert
        Assert.True(result);
    }
}