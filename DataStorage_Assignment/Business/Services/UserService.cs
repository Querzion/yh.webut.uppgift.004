using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    
    
    public async Task<IResult> CreateUserAsync(UserRegistrationForm registrationForm)
    {
        if (registrationForm == null)
            return Result.BadRequest("Invalid user registration.");

        try
        {
            if (await _userRepository.AlreadyExistsAsync(x => x.Email == registrationForm.Email))
                return Result.AlreadyExists("A user with this email already exists.");
            
            var userEntity = UserFactory.Create(registrationForm);
            
            var result = await _userRepository.CreateAsync(userEntity);
            return result ? Result.Ok() : Result.Error("Unable to create user.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> GetAllUsersAsync()
    {
        var userEntities = await _userRepository.GetAllAsync();
        var users = userEntities?.Select(UserFactory.Create);
        return Result<IEnumerable<User>>.Ok(users);
    }

    public async Task<IResult> GetUserByIdAsync(int id)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return Result.NotFound("User not found.");
        
        var user = UserFactory.Create(entity);
        return Result<User>.Ok(user);
    }

    public async Task<IResult> GetUserByEmailAsync(string email)
    {
        var entity = await _userRepository.GetAsync(x => x.Email == email);
        if (entity == null)
            return Result.NotFound("User not found.");
        
        var user = UserFactory.Create(entity);
        return Result<User>.Ok(user);
    }

    public async Task<IResult> UpdateUserAsync(int id, UserUpdateForm updateForm)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Id == id);
        if (userEntity == null)
            return Result.NotFound("User not found.");
        
        try
        {
            userEntity = UserFactory.Update(userEntity, updateForm);
            
            var result = await _userRepository.UpdateAsync(userEntity);
            return result ? Result.Ok() : Result.Error("Unable to update user.");

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> DeleteUserAsync(int id)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Id == id);
        if (userEntity == null)
            return Result.NotFound("User not found.");
        
        try
        {
            var result = await _userRepository.DeleteAsync(userEntity);
            return result ? Result.Ok() : Result.Error("Unable to delete user.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }


    public async Task<IResult> CheckIfUserExists(string email)
    {
        var entity = await _userRepository.GetAsync(x => x.Email == email);
        if (entity == null)
            return Result.NotFound("User not found.");
        
        try
        {
            var user = UserFactory.Create(entity);
            return Result<User>.Ok(user);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
}