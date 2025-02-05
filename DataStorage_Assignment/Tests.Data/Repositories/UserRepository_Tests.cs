using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tests.Data.Helpers;

namespace Tests.Data.Repositories;

public class UserRepository_Tests
{
    private readonly DataContext _context;
    private readonly UserRepository _userRepository;

    public UserRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseLazyLoadingProxies()
            .Options;
        
        _context = new DataContext(options);
        _userRepository = new UserRepository(_context);
    }

    // Create (Based on the test from the integration tests video)
    [Fact]
    public async Task CreateAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var entity = TestData.MakeTestUser();
        
        // Act
        var result = await _userRepository.CreateAsync(entity);
        
        // Assert
        Assert.True(result);
    }
    
    // Read ( Help from ChatGPT links in the Readme.md file. )
    [Fact]
    public async Task GetAllAsync_ShouldInhabitMultipleEntitiesFromDatabase()
    {
        // Asssign
        var user1 = TestData.MakeTestUser1();
        var user2 = TestData.MakeTestUser2();
        var user3 = TestData.MakeTestUser3();
        var user4 = TestData.MakeTestUser4();
        
        _userRepository.CreateAsync(user1);
        _userRepository.CreateAsync(user2);
        _userRepository.CreateAsync(user3);
        _userRepository.CreateAsync(user4);
        
        // Act
        var result = await _userRepository.GetAllAsync();
        
        // Assert
        Assert.Contains(result, u => u.Email == user1.Email);
        Assert.Contains(result, u => u.Email == user2.Email);
        Assert.Contains(result, u => u.Email == user3.Email);
        Assert.Contains(result, u => u.Email == user4.Email);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var user1 = TestData.MakeTestUser1();
        var user2 = TestData.MakeTestUser2();

        await _userRepository.CreateAsync(user1);
        await _userRepository.CreateAsync(user2);

        // Act
        var result = await _userRepository.GetAsync(u => u.Email == "anna.andersson@querzion.com");

        // Assert
        Assert.NotNull(result);                          // Ensure a user was found
        Assert.Equal("Anna", result.FirstName);         // Verify the correct user
        Assert.Equal("Andersson", result.LastName);
        Assert.Equal("anna.andersson@querzion.com", result.Email);
        
    }
    
    [Fact]
    public async Task GetByNameAsync_ShouldReturnAnEntityFromDatabase()
    {
        // Arrange
        var user1 = TestData.MakeTestUser1();
        var user2 = TestData.MakeTestUser2();

        await _userRepository.CreateAsync(user1);
        await _userRepository.CreateAsync(user2);

        // Act
        var result = await _userRepository.GetAsync(u => u.FirstName == "Anna");

        // Assert
        Assert.NotNull(result);                          // Ensure a user was found
        Assert.Equal("Anna", result.FirstName);         // Verify the correct user
        Assert.Equal("Andersson", result.LastName);
        Assert.Equal("anna.andersson@querzion.com", result.Email);
    }
    
    // Update
    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedEntityFromDatabase()
    {
        // Arrange
        var user = TestData.MakeTestUser();
        await _userRepository.CreateAsync(user);
        
        // Act
        user.FirstName = "Stam";
        var updatedResult = await _userRepository.UpdateAsync(user);
        var updatedUser = await _userRepository.GetAsync(u => u.Email == user.Email);
        
        // Assert
        Assert.True(updatedResult);
        Assert.NotNull(updatedUser);
        Assert.Equal("Stam", updatedUser.FirstName);
    }
    
    // Delete
    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntityFromDatabase()
    {
        // Arrange
        var entity = TestData.MakeTestUser();
            
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _userRepository.DeleteAsync(entity);
        
        // Assert
        Assert.True(result);
        Assert.Null(await _context.Users.FindAsync(entity.Id));
    }
    
    // Find
    [Fact]
    public async Task AlreadyExistsAsync_ShouldReturnTrue_WhenEntityExists()
    {
        // Arrange
        var testEntity = TestData.MakeTestUser();
        await _userRepository.CreateAsync(testEntity);
        await _context.SaveChangesAsync();

        // Act
        var exists = await _userRepository.AlreadyExistsAsync(e => e.Email == "slisk.lindqvist@querzion.com");

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task AlreadyExistsAsync_ShouldReturnFalse_WhenEntityDoesNotExist()
    {
        // Act
        var exists = await _userRepository.AlreadyExistsAsync(e => e.FirstName == "NonExisting");

        // Assert
        Assert.False(exists);
    }
}