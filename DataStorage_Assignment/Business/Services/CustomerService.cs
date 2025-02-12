using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

// Testing something, in order to make sense of it faster.

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    
    public async Task<IResult> CreateCustomerAsync(CustomerRegistrationForm registrationForm)
    {
        if (registrationForm == null)
            return Result.BadRequest("Invalid customer registration.");

        try
        {
            if (await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == registrationForm.CustomerName))
                return Result.AlreadyExists("A Customer with this name already exists.");
            
            var customerEntity = CustomerFactory.CreateEntityFrom(registrationForm);
            
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
        if (customerEntities == null)
            return Result.NotFound("No customers found.");
        
        var customers = customerEntities?.Select(CustomerFactory.CreateOutputModel);
        return Result<IEnumerable<Customer>>.Ok(customers);
    }

    public async Task<IResult> GetCustomerByIdAsync(int id)
    {
        var entity = await _customerRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return Result.NotFound("Customer not found.");
        
        var customer = CustomerFactory.CreateOutputModelFrom(entity);
        return Result<Customer>.Ok(customer);
    }

    public async Task<IResult> GetCustomerByNameAsync(string customerName)
    {
        var entity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        if (entity == null)
            return Result.NotFound("Customer not found.");
        
        var customer = CustomerFactory.CreateOutputModelFrom(entity);
        return Result<Customer>.Ok(customer);
    }

    public async Task<IResult> UpdateCustomerAsync(int id, CustomerUpdateForm updateForm)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        if (customerEntity == null)
            return Result.NotFound("Customer not found.");
        
        try
        {
            customerEntity = CustomerFactory.Update(customerEntity, updateForm);
            
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
        var entity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        if (entity == null)
            return Result.NotFound("Customer not found.");
        
        try
        {
            var customer = CustomerFactory.CreateOutputModelFrom(entity);
            return Result<Customer>.Ok(customer);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
}