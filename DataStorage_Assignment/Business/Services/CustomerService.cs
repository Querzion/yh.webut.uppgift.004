using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;


    public Task<IResult> CreateUserAsync(UserRegistrationForm registrationForm)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IResult> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> UpdateUserAsync(int id, UserUpdateForm updateForm)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> CheckIfUserExists(string email)
    {
        throw new NotImplementedException();
    }
}