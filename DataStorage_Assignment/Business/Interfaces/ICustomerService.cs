using Business.Dtos;

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
    Task<IResult> GetCustomerByIdAsync(int id);
    Task<IResult> GetCustomerByNameAsync(string customerName);
    Task<IResult> UpdateCustomerAsync(int id, CustomerUpdateForm updateForm);
    Task<IResult> DeleteCustomerAsync(int id);
    Task<IResult> CheckIfCustomerExists(string customerName);
}