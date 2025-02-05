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
    
    // Read ( Help from ChatGPT links in the Readme.md file. )
    [Fact]
    public async Task GetAllAsync_ShouldInhabitMultipleEntitiesFromDatabase()
    {
        // Asssign
        var status1 = TestData.MakeTestStatusType1();
        var status2 = TestData.MakeTestStatusType2();
        var status3 = TestData.MakeTestStatusType3();
        var status4 = TestData.MakeTestStatusType4();
        
        _statusTypeRepository.CreateAsync(status1);
        _statusTypeRepository.CreateAsync(status2);
        _statusTypeRepository.CreateAsync(status3);
        _statusTypeRepository.CreateAsync(status4);
        
        // Act
        var result = await _statusTypeRepository.GetAllAsync();
        
        // Assert
        Assert.Contains(result, u => u.StatusName == status1.StatusName);
        Assert.Contains(result, u => u.StatusName == status2.StatusName);
        Assert.Contains(result, u => u.StatusName == status3.StatusName);
        Assert.Contains(result, u => u.StatusName == status4.StatusName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var status1 = TestData.MakeTestStatusType1();
        var status2 = TestData.MakeTestStatusType2();
        var status3 = TestData.MakeTestStatusType3();
        var status4 = TestData.MakeTestStatusType4();

        await _statusTypeRepository.CreateAsync(status1);
        await _statusTypeRepository.CreateAsync(status2);
        await _statusTypeRepository.CreateAsync(status3);
        await _statusTypeRepository.CreateAsync(status4);

        // Act
        var result = await _statusTypeRepository.GetAsync(u => u.Id == 3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Id);
    }
    
    [Fact]
    public async Task GetByNameAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var status1 = TestData.MakeTestStatusType1();
        var status2 = TestData.MakeTestStatusType2();
        var status3 = TestData.MakeTestStatusType3();
        var status4 = TestData.MakeTestStatusType4();

        await _statusTypeRepository.CreateAsync(status1);
        await _statusTypeRepository.CreateAsync(status2);
        await _statusTypeRepository.CreateAsync(status3);
        await _statusTypeRepository.CreateAsync(status4);

        // Act
        var result = await _statusTypeRepository.GetAsync(u => u.StatusName == "Pending");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pending", result.StatusName);
    }
    
    // Update
    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedEntityFromDatabase()
    {
        // Arrange
        var status = TestData.MakeTestStatusType();
        await _statusTypeRepository.CreateAsync(status);
        
        // Act
        status.StatusName = "Occupied";
        var updatedResult = await _statusTypeRepository.UpdateAsync(status);
        var updatedUser = await _statusTypeRepository.GetAsync(u => u.StatusName == status.StatusName);
        
        // Assert
        Assert.True(updatedResult);
        Assert.NotNull(updatedUser);
        Assert.Equal("Occupied", updatedUser.StatusName);
    }
    
    // Delete
    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntityFromDatabase()
    {
        // Arrange
        var status = TestData.MakeTestStatusType();
            
        await _context.StatusTypes.AddAsync(status);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _statusTypeRepository.DeleteAsync(status);
        
        // Assert
        Assert.True(result);
        Assert.Null(await _context.StatusTypes.FindAsync(status.Id));
    }
    
    // Find
    [Fact]
    public async Task AlreadyExistsAsync_ShouldReturnTrue_WhenEntityExists()
    {
        // Arrange
        var testEntity = TestData.MakeTestStatusType();
        await _statusTypeRepository.CreateAsync(testEntity);
        await _context.SaveChangesAsync();

        // Act
        var exists = await _statusTypeRepository.AlreadyExistsAsync(e => e.StatusName == "Active");

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task AlreadyExistsAsync_ShouldReturnFalse_WhenEntityDoesNotExist()
    {
        // Act
        var exists = await _statusTypeRepository.AlreadyExistsAsync(e => e.StatusName == "NonExisting");

        // Assert
        Assert.False(exists);
    }
}