using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    
    
    public async Task<IResult> CreateCustomerAsync(CustomerRegistrationForm registrationForm)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> GetAllCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> GetCustomerAsync(Expression<Func<Customer, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> UpdateCustomerAsync(CustomerUpdateForm updateForm)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> DeleteCustomerAsync(Expression<Func<Customer, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> CheckIfCustomerExists(Expression<Func<Customer, bool>> expression)
    {
        throw new NotImplementedException();
    }
}