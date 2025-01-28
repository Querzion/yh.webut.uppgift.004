using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class CustomerFactory
{
    public static CustomerRegistrationForm CreateForm() => new();

    public static CustomerEntity CreateEntity(CustomerRegistrationForm form) => new()
    {
        CustomerName = form.CustomerName
    };

    public static Customer CreateModel(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName
    };

    public static CustomerUpdateForm CreateUpdateForm(Customer customer) => new()
    {
        Id = customer.Id,
        CustomerName = customer.CustomerName
    };

    public static CustomerEntity CreateEntity(CustomerUpdateForm form) => new()
    {
        Id = form.Id,
        CustomerName = form.CustomerName
    };
}