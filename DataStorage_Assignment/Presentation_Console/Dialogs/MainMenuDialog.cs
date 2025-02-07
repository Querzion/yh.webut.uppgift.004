using static System.Console;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class MainMenuDialog(
    ICustomerDialogs customerDialogs,
    IUserDialogs userDialogs,
    IStatusTypeDialogs statusTypeDialogs,
    IProductDialogs productDialogs,
    IProjectDialogs projectDialogs)
    : IMainMenuDialog
{
    private readonly ICustomerDialogs _customerDialogs = customerDialogs;
    private readonly IUserDialogs _userDialogs = userDialogs;
    private readonly IStatusTypeDialogs _statusTypeDialogs = statusTypeDialogs;
    private readonly IProductDialogs _productDialogs = productDialogs;
    private readonly IProjectDialogs _projectDialogs = projectDialogs;

    public async Task ShowMainMenu()
    {
        while (true)
        {
            Clear();
            Dialogs.MenuHeading("Main Menu");
            WriteLine("Choose an option:");
            WriteLine("1. StatusType Options");
            WriteLine("2. User Options");
            WriteLine("3. Customer Options");
            WriteLine("4. Product Options");
            WriteLine("5. Project Options");
            WriteLine("Q. Exit");
            Write("Select an option: ");
            var input = ReadLine()!;

            switch (input.ToLower())
            {
                case "1":
                    await _statusTypeDialogs.MenuOptions();
                    break;
                case "2":
                    await _userDialogs.MenuOptions();
                    break;
                case "3":
                    await _customerDialogs.MenuOptions();
                    break;
                case "4":
                    await _productDialogs.MenuOptions();
                    break;
                case "5":
                    await _projectDialogs.MenuOptions();
                    break;
                case "q":
                    WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;
                default:
                    WriteLine("Invalid selection. Please try again.");
                    Task.Delay(1500).Wait(); // Pause for user to read message
                    break;
            }
        }
    }
}