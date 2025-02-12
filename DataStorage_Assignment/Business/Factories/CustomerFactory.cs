using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm CreateRegistrationForm() => new();

    public static CustomerEntity CreateEntityFrom(CustomerRegistrationForm registrationForm) => new()
    {
        CustomerName = registrationForm.CustomerName
    };

    public static Customer CreateOutputModel(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName
    };
    public static Customer CreateOutputModelFrom(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName
    };

    public static CustomerUpdateForm CreateUpdateForm(Customer customer) => new()
    {
        Id = customer.Id,
        CustomerName = customer.CustomerName
    };

    public static CustomerEntity Update(CustomerEntity customerEntity, CustomerUpdateForm updateForm)
    {
        customerEntity.Id = customerEntity.Id;
        customerEntity.CustomerName = updateForm.CustomerName;
        
        return customerEntity;
    }
}