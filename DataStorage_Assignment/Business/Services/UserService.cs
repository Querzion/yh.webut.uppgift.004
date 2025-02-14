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

        await _userRepository.BeginTransactionAsync();

        try
        {
            var userEntity = UserFactory.CreateEntityFrom(registrationForm);

            if (await _userRepository.AlreadyExistsAsync(x => x.Email == registrationForm.Email))
            {
                await _userRepository.RollbackTransactionAsync();
                return Result.AlreadyExists("A user with this email already exists.");
            }
            
            var result = await _userRepository.CreateAsync(userEntity);

            if (result)
            {
                await _userRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _userRepository.RollbackTransactionAsync();
                return Result.BadRequest("Unable to create user.");
            }
        }
        catch (Exception ex)
        {
            await _userRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> GetAllUsersAsync()
    {
        var userEntities = await _userRepository.GetAllAsync();
        var users = userEntities?.Select(UserFactory.CreateOutputModel);
        return Result<IEnumerable<User>>.Ok(users);
    }

    public async Task<IResult> GetUserByIdAsync(int id)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return Result.NotFound("User not found.");
        
        var user = UserFactory.CreateOutputModelFrom(entity);
        return Result<User>.Ok(user);
    }

    public async Task<IResult> GetUserByEmailAsync(string email)
    {
        var entity = await _userRepository.GetAsync(x => x.Email == email);
        if (entity == null)
            return Result.NotFound("User not found.");
        
        var user = UserFactory.CreateOutputModelFrom(entity);
        return Result<User>.Ok(user);
    }

    public async Task<IResult> UpdateUserAsync(int id, UserUpdateForm updateForm)
    {
        // var userEntity = await _userRepository.GetAsync(x => x.Id == id);
        // if (userEntity == null)
        //     return Result.NotFound("User not found.");
        //
        // try
        // {
        //     userEntity = UserFactory.Update(userEntity, updateForm);
        //     
        //     var result = await _userRepository.UpdateAsync(userEntity);
        //     return result ? Result.Ok() : Result.Error("Unable to update user.");
        //
        // }
        // catch (Exception ex)
        // {
        //     Debug.WriteLine(ex.Message);
        //     return Result.Error(ex.Message);
        // }
        
        // the transaction management version that came up short. "(
        await _userRepository.BeginTransactionAsync();
        
        try
        {
            var userEntity = await _userRepository.GetAsync(x => x.Id == id);
            if (userEntity == null)
            {
                await _userRepository.RollbackTransactionAsync();
                return Result.NotFound("User not found.");
            }
            
            userEntity = UserFactory.Update(userEntity, updateForm);
            
            var result = await _userRepository.UpdateAsync(userEntity);
        
            if (result)
            {
                await _userRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _userRepository.RollbackTransactionAsync();
                return Result.BadRequest("Unable to update user.");
            }
        }
        catch (Exception ex)
        {
            await _userRepository.RollbackTransactionAsync();
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
            var user = UserFactory.CreateOutputModelFrom(entity);
            return Result<User>.Ok(user);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
}