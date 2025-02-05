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
    Task<IResult> CreateCustomerAsync(CustomerRegistrationForm registrationForm);
    Task<IResult> GetAllCustomersAsync();
    Task<IResult> GetCustomerAsync(Expression<Func<Customer, bool>> expression);
    Task<IResult> UpdateCustomerAsync(CustomerUpdateForm updateForm);
    Task<IResult> DeleteCustomerAsync(Expression<Func<Customer, bool>> expression);
    Task<IResult> CheckIfCustomerExists(Expression<Func<Customer, bool>> expression);
}