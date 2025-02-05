using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    
    public async Task<IResult> CreateCustomerAsync(CustomerRegistrationForm registrationForm)
    {
        if (registrationForm == null)
            return Result.BadRequest("Invalid registration form.");

        try
        {
            if (await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == registrationForm.CustomerName))
                return Result.AlreadyExists("A Customer with this email already exists.");
            
            var customerEntity = CustomerFactory.Create(registrationForm);
            
            var result = await _customerRepository.CreateAsync(customerEntity);
            return result ? Result.Ok() : Result.Error("Unable to create Customer.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> GetAllCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAllAsync();
        var customers = customerEntities?.Select(CustomerFactory.Create);
        return Result<IEnumerable<Customer>>.Ok(customers);
    }

    public async Task<IResult> GetCustomerByIdAsync(int id)
    {
        var entity = await _customerRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return Result.NotFound("Customer not found.");
        
        var customer = CustomerFactory.Create(entity);
        return Result<Customer>.Ok(customer);
    }

    public async Task<IResult> GetCustomerByNameAsync(string customerName)
    {
        var entity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        if (entity == null)
            return Result.NotFound("Customer not found.");
        
        var customer = CustomerFactory.Create(entity);
        return Result<Customer>.Ok(customer);
    }

    public async Task<IResult> UpdateCustomerAsync(int id, CustomerUpdateForm updateForm)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        if (customerEntity == null)
            return Result.NotFound("Customer not found.");
        
        try
        {
            customerEntity = CustomerFactory.Create(customerEntity, updateForm);
            var result = await _customerRepository.UpdateAsync(customerEntity);
            return result ? Result.Ok() : Result.Error("Unable to update customer.");

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> DeleteCustomerAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        if (customerEntity == null)
            return Result.NotFound("Customer not found.");
        
        try
        {
            var result = await _customerRepository.DeleteAsync(customerEntity);
            return result ? Result.Ok() : Result.Error("Unable to delete Customer.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
    
    public async Task<IResult> CheckIfCustomerExists(string customerName)
    {
        try
        {
            var entity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
            if (entity == null)
                return Result.NotFound("Customer not found.");
        
            var customer = CustomerFactory.Create(entity);
            return Result<Customer>.Ok(customer);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
}