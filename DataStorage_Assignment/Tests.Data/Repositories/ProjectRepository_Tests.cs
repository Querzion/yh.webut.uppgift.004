using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Data.Helpers;

namespace Tests.Data.Repositories;

public class ProjectRepository_Tests
{
    private readonly DataContext _context;
    private readonly ProjectRepository _projectRepository;

    public ProjectRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseLazyLoadingProxies()
            .Options;
        
        _context = new DataContext(options);
        _projectRepository = new ProjectRepository(_context);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var entity = TestData.MakeTestProject();
        
        // Act
        var result = await _projectRepository.CreateAsync(entity);
        
        // Assert
        Assert.True(result);
    }
}