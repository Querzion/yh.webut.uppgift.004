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
        
        var customer = CustomerFactory.Create();
        
        Write("Enter Customer Name: ");
        customer.CustomerName = ReadLine()!;
        
        var result = await _customerService.CreateCustomerAsync(customer);
        
        if (result.Success)
            WriteLine("Customer Created Successfully!");
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
        {
            WriteLine("No customers found!");
        }
        
        ReadKey();
    }

    public async Task ViewCustomerOption()
    {
        Dialogs.MenuHeading("View Customer");
        ReadKey();
    }

    public async Task UpdateCustomerOption()
    {
        Dialogs.MenuHeading("Update Customer");
        ReadKey();
    }

    public async Task DeleteCustomerOption()
    {
        Dialogs.MenuHeading("Delete Customer");
        ReadKey();
    }
}