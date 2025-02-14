using System.Globalization;
using System.Linq;
using Business.Factories;
using static System.Console;
using Business.Interfaces;
using Business.Models;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class ProjectDialogs(IProjectService projectService, ICustomerService customerService, IUserService userService, IStatusTypeService statusTypeService, IProductService productService) : IProjectDialogs
{
    private readonly IProjectService _projectService = projectService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IUserService _userService = userService;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    private readonly IProductService _productService = productService;
    
    public async Task MenuOptions()
    {
        while (true)
        {
            Clear();
            Dialogs.MenuHeading("Projects Options");
            WriteLine("Choose an option:");
            WriteLine("1. Create new Project");
            WriteLine("2. View All Projects");
            WriteLine("3. View Project");
            WriteLine("4. Update Project");
            WriteLine("5. Delete Project");
            WriteLine("0. Back to Main Menu");
            Write("Select an option: ");
            var input = ReadLine();

            switch (input)
            {
                case "1":
                    await CreateProjectOption();
                    break;
                case "2":
                    await ViewAllProjectsOption();
                    break;
                case "3":
                    await ViewProjectOption();
                    break;
                case "4":
                    await UpdateProjectOption();
                    break;
                case "5":
                    await DeleteProjectOption();
                    break;
                case "0":
                    return;
                default:
                    WriteLine("Invalid selection. Please try again.");
                    Task.Delay(1500).Wait(); // Pause for Project to read message
                    break;
            }

            ReadKey();
        }
    }

    #region Create Project
    public async Task CreateProjectOption()
    {
        Dialogs.MenuHeading("Create Project");
        
        var status = ProjectFactory.CreateRegistrationForm();
        
        Write("Enter Project Title: ");
        status.Title = ReadLine()!;
        Write("Enter Project Description: ");
        status.Description = ReadLine()!;
        
        // DateTime bits are from ChatGPT
        // Get valid Start Date
        DateTime startDate;
        while (true)
        {
            Write("Enter Project Start Date (yyyy/MM/dd): ");
            if (DateTime.TryParseExact(ReadLine(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                status.StartDate = startDate;
                break;
            }
            WriteLine("Invalid date format. Please enter the date in yyyy/MM/dd format.");
        }

        // Get valid End Date
        while (true)
        {
            Write("Enter Project End Date (yyyy/MM/dd): ");
            DateTime endDate;
            if (DateTime.TryParseExact(ReadLine(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                status.EndDate = endDate;
                break;
            }
            WriteLine("Invalid date format. Please enter the date in yyyy/MM/dd format.");
        }
        status.CustomerId = GetValidInteger("Enter Project CustomerId: ");
        status.StatusId = GetValidInteger("Enter Project StatusId: ");
        status.UserId = GetValidInteger("Enter Project UserId: ");
        status.ProductId = GetValidInteger("Enter Project ProductId: ");
        
        var result = await _projectService.CreateProjectAsync(status);
        
        if (result.Success)
            WriteLine($"Project Created Successfully!");
        else
            WriteLine($"Project Creation Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
    }
    
    // ChatGPT Suggestion
    private static int GetValidInteger(string prompt)
    {
        while (true)
        {
            Write(prompt);
            if (int.TryParse(ReadLine(), out var value))
                return value;
            WriteLine("Invalid input. Please enter a valid integer.");
        }
    }
    #endregion 

    #region View Projects/Project
    public async Task ViewAllProjectsOption()
    {
        Dialogs.MenuHeading("View All Projects");
        
        var iResult = await _projectService.GetAllProjectsAsync();
        
        if (iResult is Result<IEnumerable<Project>> { Success: true, Data: not null } projectResult)
        {
            foreach (var project in projectResult.Data)
            {
                var (customerName, statusName, userName, productName, productPrice) = await GetProjectDetailsAsync(
                    project.CustomerId,
                    project.StatusId,
                    project.UserId,
                    project.ProductId
                );
                
                WriteLine($"\nID: {project.Id} \n" +
                          $"Title: {project.Title}, \n" +
                          $"Description: {project.Description}, \n" +
                          $"Starts: {project.StartDate}, Ends: {project.EndDate}, \n" +
                          $"Customer: {customerName}, \n" +
                          $"Status: {statusName}, \n" +
                          $"Assigned User: {userName}, \n" +
                          $"Service: {productName} ",
                          $"Price: {productPrice} SEK/h");
            }
        }
        else
            WriteLine($"Failed! \nReason: {iResult.ErrorMessage ?? "Unknown error."}");
    }

    public async Task ViewProjectOption()
    {
        Dialogs.MenuHeading("View Project");
        
        WriteLine("Choose an option:");
        WriteLine("1. Search by ID");
        WriteLine("2. Search by Email");
        WriteLine("0. Back");
        Write("Select an option: ");
        var input = ReadLine();

        switch (input)
        {
            case "1":
                await ViewProjectById();
                break;
            
            case "2":
                await ViewProjectByName();
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
    
    private async Task ViewProjectById()
    {
        Dialogs.MenuHeading("Search by Id");
                
        Write("Enter Customer ID: ");
        var idInput = ReadLine();
                
        // ChatGPT Generated. (It forces the input to be of variable integer.)
        if (int.TryParse(idInput, out var projectId))
        {
            var iResult = await _projectService.GetProjectByIdAsync(projectId);

            if (iResult is Result<Project> { Success: true, Data: not null } projectResult)
            {
                var (customerName, statusName, userName, productName, productPrice) = await GetProjectDetailsAsync(
                    projectResult.Data.CustomerId,
                    projectResult.Data.StatusId,
                    projectResult.Data.UserId,
                    projectResult.Data.ProductId
                );
                
                WriteLine($"\nID: {projectResult.Data.Id} \n" +
                          $"Title: {projectResult.Data.Title}, \n" +
                          $"Description: {projectResult.Data.Description}, \n" +
                          $"Starts: {projectResult.Data.StartDate}, Ends: {projectResult.Data.EndDate}, \n" +
                          $"Customer: {customerName}, \n" +
                          $"Status: {statusName}, \n" +
                          $"Assigned User: {userName}, \n" +
                          $"Service: {productName} - Price: {productPrice} SEK/h");
            }
            else
                WriteLine($"Failed! \nReason: {iResult.ErrorMessage ?? "Unknown error."}");
        }
        else
        {
            WriteLine("Invalid ID format.");
        }
    }
    
    private async Task ViewProjectByName()
    {
        Dialogs.MenuHeading("Search by Email");
                
        Write("Enter Customer Name: ");
        var nameInput = ReadLine()!;
        var iResult = await _projectService.GetProjectByNameAsync(nameInput);

        if (iResult is Result<Project> { Success: true, Data: not null } projectResult)
        {
            var (customerName, statusName, userName, productName, productPrice) = await GetProjectDetailsAsync(
                projectResult.Data.CustomerId,
                projectResult.Data.StatusId,
                projectResult.Data.UserId,
                projectResult.Data.ProductId
            );
                
            WriteLine($"\nID: {projectResult.Data.Id} \n" +
                      $"Title: {projectResult.Data.Title}, \n" +
                      $"Description: {projectResult.Data.Description}, \n" +
                      $"Starts: {projectResult.Data.StartDate}, Ends: {projectResult.Data.EndDate}, \n" +
                      $"Customer: {customerName}, \n" +
                      $"Status: {statusName}, \n" +
                      $"Assigned User: {userName}, \n" +
                      $"Service: {productName} - Price: {productPrice} SEK/h");
        }
        else
        {
            WriteLine($"Failed! \nReason: {iResult.ErrorMessage ?? "Unknown error."}");
        }
    }
    
    public async Task<(string customerName, string statusName, string userName, string productName, decimal price)> GetProjectDetailsAsync(int customerId, int statusId, int userId, int productId)
    {
        // Retrieve the customer, status, user, and product asynchronously
        var customerResult = await _customerService.GetCustomerByIdAsync(customerId);
        var statusResult = await _statusTypeService.GetStatusTypeByIdAsync(statusId);
        var userResult = await _userService.GetUserByIdAsync(userId);
        var productResult = await _productService.GetProductByIdAsync(productId);

        // Cast each result to Result<T> to access Data
        var customer = customerResult as Result<Customer>;
        var status = statusResult as Result<StatusType>;
        var user = userResult as Result<User>;
        var product = productResult as Result<Product>;

        // Safely get customer name
        string customerName = customer?.Success == true && customer?.Data != null 
            ? customer.Data.CustomerName 
            : "No customer assigned";

        // Safely get status name
        string statusName = status?.Success == true && status?.Data != null 
            ? status.Data.StatusName 
            : "No status provided";

        // Safely get user name
        string userName = user?.Success == true && user?.Data != null 
            ? $"{user.Data.FirstName} {user.Data.LastName}" 
            : "No user assigned";

        // Safely get product name
        string productName = product?.Success == true && product?.Data != null 
            ? product.Data.ProductName 
            : "No service provided";
                
        decimal productPrice = product?.Success == true && product?.Data != null 
            ? product.Data.Price
            : 0;

        return (customerName, statusName, userName, productName, productPrice);
    }
    #endregion 
    
    #region Update Project
    public async Task UpdateProjectOption()
    {
        Dialogs.MenuHeading("Update Project");
        
        Write("Project email: ");
        var nameInput = ReadLine()!;
        
        if(!string.IsNullOrEmpty(nameInput))
        {
            var iResult = await _projectService.GetProjectByNameAsync(nameInput);
            if (iResult is Result<Project> { Success: true, Data: not null } projectResult)
            {
                var (customerName, statusName, userName, productName, productPrice) = await GetProjectDetailsAsync(
                    projectResult.Data.CustomerId,
                    projectResult.Data.StatusId,
                    projectResult.Data.UserId,
                    projectResult.Data.ProductId
                );
                
                WriteLine($"\nID: {projectResult.Data.Id} \n" +
                          $"Title: {projectResult.Data.Title}, \n" +
                          $"Description: {projectResult.Data.Description}, \n" +
                          $"Starts: {projectResult.Data.StartDate}, Ends: {projectResult.Data.EndDate}, \n" +
                          $"Customer: {customerName}, \n" +
                          $"Status: {statusName}, \n" +
                          $"Assigned User: {userName}, \n" +
                          $"Service: {productName} - Price: {productPrice} SEK/h");

                var projectUpdateForm = ProjectFactory.CreateUpdateForm();
                projectUpdateForm.Id = projectResult.Data.Id;
                
                Write("New Project Title: ");
                var title = ReadLine()!;
                if (!string.IsNullOrEmpty(title) && title != projectResult.Data.Title)
                    projectUpdateForm.Title = title;
                
                Write("New Project Description: ");
                var description = ReadLine()!;
                if (!string.IsNullOrEmpty(description) && description != projectResult.Data.Description)
                    projectUpdateForm.Description = description;
                
                // Parse Start Date
                Write("New Project Start Date (yyyy/MM/dd): ");
                var startDateInput = ReadLine();
                if (!string.IsNullOrEmpty(startDateInput) && DateTime.TryParseExact(startDateInput, "yyyy/MM/dd", 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                {
                    projectUpdateForm.StartDate = startDate;
                }
                else if (!string.IsNullOrEmpty(startDateInput))
                {
                    WriteLine("Invalid date format. Please enter the date in yyyy/MM/dd format.");
                }

                // Parse End Date
                Write("New Project End Date (yyyy/MM/dd): ");
                var endDateInput = ReadLine();
                if (!string.IsNullOrEmpty(endDateInput) && DateTime.TryParseExact(endDateInput, "yyyy/MM/dd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                {
                    projectUpdateForm.EndDate = endDate;
                }
                else if (!string.IsNullOrEmpty(endDateInput))
                {
                    WriteLine("Invalid date format. Please enter the date in yyyy/MM/dd format.");
                }
                
                var newCustomerId = GetValidInteger("New Project CustomerId: ");
                if (newCustomerId != null && newCustomerId != projectResult.Data.CustomerId) 
                    projectUpdateForm.CustomerId = newCustomerId;
              
                var newStatusId = GetValidInteger("New Project StatusId: ");
                if (newStatusId != null && newStatusId != projectResult.Data.StatusId) 
                    projectUpdateForm.StatusId = newStatusId;
                
                var newUserId = GetValidInteger("New Project UserId: ");
                if (newUserId != null && newUserId != projectResult.Data.StatusId) 
                    projectUpdateForm.StatusId = newUserId;
                
                var newProductId = GetValidInteger("New Project ProductId: ");
                if (newProductId != null && newProductId != projectResult.Data.ProductId) 
                    projectUpdateForm.ProductId = newProductId;
                
                var updateIResult = await _projectService.UpdateProjectAsync(projectResult.Data.Id, projectUpdateForm);
                
                if (updateIResult.Success)
                {
                    WriteLine($"ID: {projectResult.Data.Id} updated successfully.");
                }
                else
                {
                    WriteLine($"Failed to update! \nReason: {updateIResult.ErrorMessage ?? "Unknown error."}");
                }
            }
            else
            {
                WriteLine("Field cannot be empty. Please try again.");
            }
        }
    }
    #endregion
    
    #region Delete Project
    public async Task DeleteProjectOption()
    {
        Dialogs.MenuHeading("Delete Project");
        
        Write("Enter Project ID: ");
        var idInput = ReadLine()!;

        if (!string.IsNullOrEmpty(idInput))
        {
            var iResult = await _projectService.GetProjectByIdAsync(int.Parse(idInput));
            if (iResult is Result<Project> { Success: true, Data: not null } projectResult)
            {
                var result = await _projectService.DeleteProjectAsync(projectResult.Data.Id);
                
                if (result.Success)
                    WriteLine($"Project Deleted Successfully!");
                else
                    WriteLine($"Project Deletion Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
            }
            else
                WriteLine($"Failed! \nReason: {iResult.ErrorMessage ?? "Unknown error."}");
        }
    }
    #endregion
}