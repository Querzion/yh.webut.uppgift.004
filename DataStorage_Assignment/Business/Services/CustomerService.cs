using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository)
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
}