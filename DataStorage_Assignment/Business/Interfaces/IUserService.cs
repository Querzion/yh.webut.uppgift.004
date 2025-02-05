using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IUserService
{
    // Without IResult
    // Task<User> CreateUserAsync(UserRegistrationForm registrationForm);
    // Task<IEnumerable<User>> GetAllUsersAsync();
    // Task<User> GetUserAsync(Expression<Func<UserEntity, bool>> expression);
    // Task<User> UpdateUserAsync(UserUpdateForm form);
    // Task<bool> DeleteUserAsync(int id);
    // Task<bool> CheckIfUserExistsAsync(Expression<Func<UserEntity, bool>> expression);
    
    // With IResult
    Task<IResult> CreateUserAsync(UserRegistrationForm registrationForm);
    Task<IResult> GetAllUsersAsync();
    Task<IResult> GetUserByIdAsync(int id);
    Task<IResult> GetUserByEmailAsync(string email);
    Task<IResult> UpdateUserAsync(int id, UserUpdateForm updateForm);
    Task<IResult> DeleteUserAsync(int id);
    Task<IResult> CheckIfUserExists(string email);
}