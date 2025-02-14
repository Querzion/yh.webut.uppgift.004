using System.Diagnostics;
using Business.Factories;
using static System.Console;
using Business.Interfaces;
using Business.Models;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class CustomerDialogs(ICustomerService customerService) : ICustomerDialogs
{
    private readonly ICustomerService _customerService = customerService;
    
    public async Task MenuOptions()
    {
        while (true)
        {
            Clear();
            await ViewAllCustomersOption();
            Clear();
            Dialogs.MenuHeading("Customers Options");
            WriteLine("Choose an option:");
            WriteLine("1. Create new customer");
            WriteLine("2. View All customers");
            WriteLine("3. View customer");
            WriteLine("4. Update customer");
            WriteLine("5. Delete customer");
            WriteLine("0. Back to Main Menu");
            Write("Select an option: ");
            var input = ReadLine();

            switch (input)
            {
                case "1":
                    await CreateCustomerOption();
                    break;
                case "2":
                    await ViewAllCustomersOption();
                    break;
                case "3":
                    await ViewCustomerOption();
                    break;
                case "4":
                    await UpdateCustomerOption();
                    break;
                case "5":
                    await DeleteCustomerOption();
                    break;
                case "0":
                    return;
                default:
                    WriteLine("Invalid selection. Please try again.");
                    Task.Delay(1500).Wait(); // Pause for user to read message
                    break;
            }
        }
    }

    public async Task CreateCustomerOption()
    {
        Dialogs.MenuHeading("Create Customer");
        
        var customer = CustomerFactory.CreateRegistrationForm();
        
        Write("Enter Customer Name: ");
        customer.CustomerName = ReadLine()!;
        
        var result = await _customerService.CreateCustomerAsync(customer);
        
        if (result.Success)
            WriteLine($"Customer Created Successfully!");
        else
            WriteLine($"Customer Creation Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
        
        ReadKey();
    }

    public async Task ViewAllCustomersOption()
    {
        Dialogs.MenuHeading("View All Customers");
        
        var result = await _customerService.GetAllCustomersAsync();

        // ChatGPT generated
        // if (result is Result<IEnumerable<Customer>> customerResult && customerResult.Success && customerResult.Data is not null)
        // and merged it into this
        if (result is Result<IEnumerable<Customer>> { Success: true, Data: not null } customerResult)
        {
            foreach (var customer in customerResult.Data)
            {
                WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
            }
        }
        else
            WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
        
        
        ReadKey();
    }

    public async Task ViewCustomerOption()
    {
        Dialogs.MenuHeading("View Customer");
        
        WriteLine("Choose an option:");
        WriteLine("1. Search by ID");
        WriteLine("2. Search by Name");
        WriteLine("0. Back");
        Write("Select an option: ");
        var input = ReadLine();

        switch (input)
        {
            case "1":
                Dialogs.MenuHeading("Search by Id");
                
                Write("Enter Customer ID: ");
                var idInput = ReadLine();
                
                // ChatGPT Generated. (It forces the input to be of variable integer.)
                if (int.TryParse(idInput, out var customerId))
                {
                    var result = await _customerService.GetCustomerByIdAsync(customerId);

                    if (result is Result<Customer> { Success: true, Data: not null } customerResult)
                        WriteLine($"Customer with ID: {customerResult.Data.Id} found. Name: {customerResult.Data.CustomerName}");
                    else
                        WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
                }
                else
                {
                    WriteLine("Invalid ID format.");
                }
                
                break;
            
            
            case "2":
                Dialogs.MenuHeading("Search by Name");
                
                Write("Enter Customer Name: ");
                var nameInput = ReadLine()!;
                var searchResult = await _customerService.GetCustomerByNameAsync(nameInput);

                if (searchResult is Result<Customer> { Success: true, Data: not null } customer)
                {
                    WriteLine($"Customer Found: ID: {customer.Data.Id}, Name: {customer.Data.CustomerName}");
                }
                else
                {
                    WriteLine($"Failed! \nReason: {searchResult.ErrorMessage ?? "Unknown error."}");
                }
                
                break;
            
            
            case "0":
                return;
            
            default:
                WriteLine("Invalid selection. Please try again.");
                Task.Delay(1500).Wait();
                break;
        }
        
        ReadKey();
    }

    public async Task UpdateCustomerOption()
    {
        Dialogs.MenuHeading("Update Customer");
        
        Write("Customer Name: ");
        var nameInput = ReadLine()!;
        
        if(!string.IsNullOrEmpty(nameInput))
        {
            var result = await _customerService.GetCustomerByNameAsync(nameInput);
            if (result is Result<Customer> { Success: true, Data: not null } customer)
            {
                WriteLine($"Id: {customer.Data.Id} \nName: {customer.Data.CustomerName}");
                WriteLine("");

                var customerUpdateForm = CustomerFactory.CreateUpdateForm();
                customerUpdateForm.Id = customer.Data.Id;
                
                Write("Customer name: ");
                var customerName = ReadLine()!;
                if (!string.IsNullOrEmpty(customerName) && customerName != customer.Data.CustomerName)
                    customerUpdateForm.CustomerName = customerName;
                
                var updateResult = await _customerService.UpdateCustomerAsync(customer.Data.Id, customerUpdateForm);
                
                if (updateResult.Success)
                {
                    WriteLine($"id: {customer.Data.Id} updated successfully.");
                }
                else
                {
                    WriteLine($"Failed to update! \nReason: {updateResult.ErrorMessage ?? "Unknown error."}");
                }
            }
            else
            {
                WriteLine($"Field cannot be empty. Please try again.");
            }
            
            ReadKey();
        }
    }

    public async Task DeleteCustomerOption()
    {
        Dialogs.MenuHeading("Delete Customer");
        
        Write("Enter Customer ID: ");
        var idInput = ReadLine()!;

        if (!string.IsNullOrEmpty(idInput))
        {
            var customer = await _customerService.GetCustomerByIdAsync(int.Parse(idInput));
            if (customer is Result<Customer> { Success: true, Data: not null } customerResult)
            {
                var result = await _customerService.DeleteCustomerAsync(customerResult.Data.Id);
                
                if (result.Success)
                    WriteLine($"Customer Deleted Successfully!");
                else
                    WriteLine($"Customer Deletion Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
            }
            else
                WriteLine($"Failed! \nReason: {customer.ErrorMessage ?? "Unknown error."}");
        }
        
        ReadKey();
    }
}