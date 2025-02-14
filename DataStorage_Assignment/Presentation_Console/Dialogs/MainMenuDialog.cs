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
            WriteLine("5. Project Options (Depends on 1-4)");
            WriteLine("Q. Exit");
            Write("Select an option: ");
            var input = ReadLine()!;

            switch (input.ToLower())
            {
                case "1":
                    await statusTypeDialogs.MenuOptions();
                    break;
                case "2":
                    await userDialogs.MenuOptions();
                    break;
                case "3":
                    await customerDialogs.MenuOptions();
                    break;
                case "4":
                    await productDialogs.MenuOptions();
                    break;
                case "5":
                    await projectDialogs.MenuOptions();
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