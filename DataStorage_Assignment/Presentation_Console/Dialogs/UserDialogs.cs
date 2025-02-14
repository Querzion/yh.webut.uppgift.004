using Business.Factories;
using static System.Console;
using Business.Interfaces;
using Business.Models;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class UserDialogs(IUserService userService) : IUserDialogs
{
    private readonly IUserService _userService = userService;
    
    #region Main Menu
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

            ReadKey();
        }
    }
    #endregion

    #region Create User
    public async Task CreateUserOption()
    {
        Dialogs.MenuHeading("Create User");
        
        var status = UserFactory.CreateRegistrationForm();
        
        Write("Enter User Name: ");
        status.FirstName = ReadLine()!;
        Write("Enter User Name: ");
        status.LastName = ReadLine()!;
        Write("Enter User Name: ");
        status.Email = ReadLine()!;
        
        var result = await _userService.CreateUserAsync(status);
        
        if (result.Success)
            WriteLine($"User Created Successfully!");
        else
            WriteLine($"User Creation Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
    }
    #endregion 

    #region View Users/User
    public async Task ViewAllUsersOption()
    {
        Dialogs.MenuHeading("View All Users");
        
        var result = await _userService.GetAllUsersAsync();
        
        if (result is Result<IEnumerable<User>> { Success: true, Data: not null } userResult)
        {
            foreach (var user in userResult.Data)
            {
                WriteLine($"\nID: {user.Id} \n" +
                          $"Name: {user.FirstName} {user.LastName}, Email: {user.Email}");
            }
        }
        else
            WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
    }

    public async Task ViewUserOption()
    {
        Dialogs.MenuHeading("View User");
        
        WriteLine("Choose an option:");
        WriteLine("1. Search by ID");
        WriteLine("2. Search by Email");
        WriteLine("0. Back");
        Write("Select an option: ");
        var input = ReadLine();

        switch (input)
        {
            case "1":
                await ViewUserById();
                break;
            
            case "2":
                await ViewUserByEmail();
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

    private async Task ViewUserByEmail()
    {
        Dialogs.MenuHeading("Search by Email");
                
        Write("Enter Customer Name: ");
        var nameInput = ReadLine()!;
        var searchResult = await _userService.GetUserByEmailAsync(nameInput);

        if (searchResult is Result<User> { Success: true, Data: not null } user)
        {
            WriteLine($"\nID: {user.Data.Id} \n" +
                      $"Name: {user.Data.FirstName} {user.Data.LastName}, Email: {user.Data.Email}");
        }
        else
        {
            WriteLine($"Failed! \nReason: {searchResult.ErrorMessage ?? "Unknown error."}");
        }
    }

    private async Task ViewUserById()
    {
        Dialogs.MenuHeading("Search by Id");
                
        Write("Enter Customer ID: ");
        var idInput = ReadLine();
                
        // ChatGPT Generated. (It forces the input to be of variable integer.)
        if (int.TryParse(idInput, out var userId))
        {
            var result = await _userService.GetUserByIdAsync(userId);

            if (result is Result<User> { Success: true, Data: not null } userResult)
                WriteLine($"\nID: {userResult.Data.Id} \n" +
                          $"Name: {userResult.Data.FirstName} {userResult.Data.LastName}, Email: {userResult.Data.Email}");
            else
                WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
        }
        else
        {
            WriteLine("Invalid ID format.");
        }
    }

    #endregion 
    
    #region Update User
    public async Task UpdateUserOption()
    {
        Dialogs.MenuHeading("Update User");
        
        Write("User email: ");
        var nameInput = ReadLine()!;
        
        if(!string.IsNullOrEmpty(nameInput))
        {
            var result = await _userService.GetUserByEmailAsync(nameInput);
            if (result is Result<User> { Success: true, Data: not null } userResult)
            {
                WriteLine($"\nID: {userResult.Data.Id} \n" +
                          $"Name: {userResult.Data.FirstName} {userResult.Data.LastName}, Email: {userResult.Data.Email}");
                WriteLine("");

                var userUpdateForm = UserFactory.CreateUpdateForm();
                userUpdateForm.Id = userResult.Data.Id;
                
                Write("New User First Name: ");
                var firstName = ReadLine()!;
                if (!string.IsNullOrEmpty(firstName) && firstName != userResult.Data.FirstName)
                    userUpdateForm.FirstName = firstName;
                
                Write("New User Last Name: ");
                var lastName = ReadLine()!;
                if (!string.IsNullOrEmpty(lastName) && lastName != userResult.Data.FirstName)
                    userUpdateForm.FirstName = lastName;
                
                Write("New User Email: ");
                var email = ReadLine()!;
                if (!string.IsNullOrEmpty(email) && email != userResult.Data.FirstName)
                    userUpdateForm.FirstName = email;
                
                var updateResult = await _userService.UpdateUserAsync(userResult.Data.Id, userUpdateForm);
                
                if (updateResult.Success)
                {
                    WriteLine($"id: {userResult.Data.Id} updated successfully.");
                }
                else
                {
                    WriteLine($"Failed to update! \nReason: {updateResult.ErrorMessage ?? "Unknown error."}");
                }
            }
            else
            {
                WriteLine("Field cannot be empty. Please try again.");
            }
        }
    }
    #endregion
    
    #region Delete User
    public async Task DeleteUserOption()
    {
        Dialogs.MenuHeading("Delete Customer");
        
        Write("Enter Customer ID: ");
        var idInput = ReadLine()!;

        if (!string.IsNullOrEmpty(idInput))
        {
            var user = await _userService.GetUserByIdAsync(int.Parse(idInput));
            if (user is Result<User> { Success: true, Data: not null } userResult)
            {
                var result = await _userService.DeleteUserAsync(userResult.Data.Id);
                
                if (result.Success)
                    WriteLine($"User Deleted Successfully!");
                else
                    WriteLine($"User Deletion Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
            }
            else
                WriteLine($"Failed! \nReason: {user.ErrorMessage ?? "Unknown error."}");
        }
    }
    #endregion
}