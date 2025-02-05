using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICustomerService
{
    // Without IResult
    // Task<Customer> CreateCustomerAsync(CustomerRegistrationForm form);
    // Task<IEnumerable<Customer>> GetAllCustomersAsync();
    // Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
    // Task<Customer> UpdateCustomerAsync(CustomerUpdateForm form);
    // Task<bool> DeleteCustomerAsync(int id);
    // Task<bool> CheckIfCustomerExistsAsync(Expression<Func<CustomerEntity, bool>> expression);
    
    // With IResult
    Task<IResult> CreateUserAsync(UserRegistrationForm registrationForm);
    Task<IResult> GetAllUsersAsync();
    Task<IResult> GetUserByIdAsync(int id);
    Task<IResult> GetUserByEmailAsync(string email);
    Task<IResult> UpdateUserAsync(int id, UserUpdateForm updateForm);
    Task<IResult> DeleteUserAsync(int id);
    Task<IResult> CheckIfUserExists(string email);
}