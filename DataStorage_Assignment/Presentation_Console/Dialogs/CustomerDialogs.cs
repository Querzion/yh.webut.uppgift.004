using static System.Console;
using Business.Interfaces;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class CustomerDialogs(ICustomerService customerService, IMainMenuDialog mainMenuDialog) : ICustomerDialogs
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IMainMenuDialog _mainMenuDialog = mainMenuDialog;
    
    public async Task MenuOptions()
    {
        while (true)
        {
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
                    await _mainMenuDialog.ShowMainMenu();
                    break;
                default:
                    WriteLine("Invalid selection. Please try again.");
                    Task.Delay(1500).Wait(); // Pause for user to read message
                    break;
            }
        }
    }

    public async Task CreateCustomerOption()
    {
        throw new NotImplementedException();
    }

    public async Task ViewAllCustomersOption()
    {
        throw new NotImplementedException();
    }

    public async Task ViewCustomerOption()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCustomerOption()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCustomerOption()
    {
        throw new NotImplementedException();
    }
}