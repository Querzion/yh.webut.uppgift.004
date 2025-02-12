using static System.Console;
using Business.Interfaces;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class UserDialogs(IUserService userService) : IUserDialogs
{
    private readonly IUserService _userService = userService;
    
    public async Task MenuOptions()
    {
        while (true)
        {
            Clear();
            Dialogs.MenuHeading("Users Options");
            WriteLine("Choose an option:");
            WriteLine("1. Create new User");
            WriteLine("2. View All Users");
            WriteLine("3. View User");
            WriteLine("4. Update User");
            WriteLine("5. Delete User");
            WriteLine("0. Back to Main Menu");
            Write("Select an option: ");
            var input = ReadLine();

            switch (input)
            {
                case "1":
                    await CreateUserOption();
                    break;
                case "2":
                    await ViewAllUsersOption();
                    break;
                case "3":
                    await ViewUserOption();
                    break;
                case "4":
                    await UpdateUserOption();
                    break;
                case "5":
                    await DeleteUserOption();
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

    private async Task CreateUserOption()
    {
        Dialogs.MenuHeading("Create User");
    }

    private async Task ViewAllUsersOption()
    {
        Dialogs.MenuHeading("View All Users");
    }

    private async Task ViewUserOption()
    {
        Dialogs.MenuHeading("View User");
    }

    private async Task UpdateUserOption()
    {
        Dialogs.MenuHeading("Update User");
    }

    private async Task DeleteUserOption()
    {
        Dialogs.MenuHeading("Delete User");
    }
}