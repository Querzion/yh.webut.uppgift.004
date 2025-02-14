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

        await _customerRepository.BeginTransactionAsync();

        try
        {
            var customerEntity = CustomerFactory.CreateEntityFrom(registrationForm);

            if (await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == registrationForm.CustomerName))
            {
                await _customerRepository.RollbackTransactionAsync();
                return Result.AlreadyExists("A Customer with this name already exists.");
            }            
            
            var result = await _customerRepository.CreateAsync(customerEntity);

            if (result)
            {
                await _customerRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _customerRepository.RollbackTransactionAsync();
                return Result.Error("Customer could not be created.");
            }
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
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
        await _customerRepository.BeginTransactionAsync();
        
        try
        {
            var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
            if (customerEntity == null)
            {
                await _customerRepository.RollbackTransactionAsync();
                return Result.NotFound("Customer not found.");
            }
            
            customerEntity = CustomerFactory.Update(customerEntity, updateForm);
            
            var result = await _customerRepository.UpdateAsync(customerEntity);

            if (result)
            {
                await _customerRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _customerRepository.RollbackTransactionAsync();
                return Result.Error("Customer could not be updated.");
            }

        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
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