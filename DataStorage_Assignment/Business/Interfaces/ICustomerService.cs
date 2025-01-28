using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(CustomerRegistrationForm form);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<Customer> UpdateCustomerAsync(CustomerUpdateForm form);
    Task<bool> DeleteCustomerAsync(int id);
    Task<bool> CheckIfCustomerExistsAsync(Expression<Func<CustomerEntity, bool>> expression);
}