using Business.Factories;
using static System.Console;
using Business.Interfaces;
using Business.Models;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class StatusTypeDialogs(IStatusTypeService statusTypeService) : IStatusTypeDialogs
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    
    #region Main Menu
    public async Task MenuOptions()
    {
        while (true)
        {
            Clear();
            Dialogs.MenuHeading("StatusTypes Options");
            WriteLine("Choose an option:");
            WriteLine("1. Create new StatusType");
            WriteLine("2. View All StatusTypes");
            WriteLine("3. View StatusType");
            WriteLine("4. Update StatusType");
            WriteLine("5. Delete StatusType");
            WriteLine("0. Back to Main Menu");
            Write("Select an option: ");
            var input = ReadLine();

            switch (input)
            {
                case "1":
                    await CreateStatusTypeOption();
                    break;
                case "2":
                    await ViewAllStatusTypesOption();
                    break;
                case "3":
                    await ViewStatusTypeOption();
                    break;
                case "4":
                    await UpdateStatusTypeOption();
                    break;
                case "5":
                    await DeleteStatusTypeOption();
                    break;
                case "0":
                    return;
                default:
                    WriteLine("Invalid selection. Please try again.");
                    Task.Delay(1500).Wait(); // Pause for StatusType to read message
                    break;
            }

            ReadKey();
        }
    }
    #endregion
    
    #region Create StatusType
    public async Task CreateStatusTypeOption()
    {
        Dialogs.MenuHeading("Create StatusType");
        
        var status = StatusTypeFactory.CreateRegistrationForm();
        
        Write("Enter StatusType Name: ");
        status.StatusName = ReadLine()!;
        
        
        var result = await _statusTypeService.CreateStatusTypeAsync(status);
        
        if (result.Success)
            WriteLine($"StatusType Created Successfully!");
        else
            WriteLine($"StatusType Creation Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
    }
    #endregion 

    #region View StatusTypes/StatusType
    public async Task ViewAllStatusTypesOption()
    {
        Dialogs.MenuHeading("View All StatusTypes");
        
        var result = await _statusTypeService.GetAllStatusTypesAsync();
        
        if (result is Result<IEnumerable<StatusType>> { Success: true, Data: not null } statusTypeResult)
        {
            foreach (var statusType in statusTypeResult.Data)
            {
                WriteLine($"ID: {statusType.Id}, Name: {statusType.StatusName}");
            }
        }
        else
            WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
    }

    public async Task ViewStatusTypeOption()
    {
        Dialogs.MenuHeading("View StatusType");
        
        WriteLine("Choose an option:");
        WriteLine("1. Search by ID");
        WriteLine("2. Search by Name");
        WriteLine("0. Back");
        Write("Select an option: ");
        var input = ReadLine();

        switch (input)
        {
            case "1":
                await ViewStatusTypeById();
                break;
            
            case "2":
                await ViewStatusTypeByName();
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
    
    private async Task ViewStatusTypeById()
    {
        Dialogs.MenuHeading("Search by Id");
                
        Write("Enter Customer ID: ");
        var idInput = ReadLine();
                
        // ChatGPT Generated. (It forces the input to be of variable integer.)
        if (int.TryParse(idInput, out var statusTypeId))
        {
            var result = await _statusTypeService.GetStatusTypeByIdAsync(statusTypeId);

            if (result is Result<StatusType> { Success: true, Data: not null } statusTypeResultId)
                WriteLine($"StatusType with ID: {statusTypeResultId.Data.Id} found. Status: {statusTypeResultId.Data.StatusName}.");
            else
                WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
        }
        else
        {
            WriteLine("Invalid ID format.");
        }
    }
    
    private async Task ViewStatusTypeByName()
    {
        Dialogs.MenuHeading("Search by Name");
                
        Write("Enter Customer Name: ");
        var nameInput = ReadLine()!;
        var searchResult = await _statusTypeService.GetStatusByStatusNameAsync(nameInput);

        if (searchResult is Result<StatusType> { Success: true, Data: not null } statusTypeResultName)
        {
            WriteLine($"StatusType Found: ID: {statusTypeResultName.Data.Id}, Status: {statusTypeResultName.Data.StatusName}.");
        }
        else
        {
            WriteLine($"Failed! \nReason: {searchResult.ErrorMessage ?? "Unknown error."}");
        }
    }

    #endregion 
    
    #region Update StatusType
    public async Task UpdateStatusTypeOption()
    {
        Dialogs.MenuHeading("Update StatusType");
        
        Write("StatusType Name: ");
        var nameInput = ReadLine()!;
        
        if(!string.IsNullOrEmpty(nameInput))
        {
            var result = await _statusTypeService.GetStatusByStatusNameAsync(nameInput);
            if (result is Result<StatusType> { Success: true, Data: not null } statusType)
            {
                WriteLine($"Id: {statusType.Data.Id} \nStatus: {statusType.Data.StatusName}.");
                WriteLine("");

                var statusTypeUpdateForm = StatusTypeFactory.CreateUpdateForm();
                statusTypeUpdateForm.Id = statusType.Data.Id;
                
                Write("New StatusType Name: ");
                var statusTypeName = ReadLine()!;
                if (!string.IsNullOrEmpty(statusTypeName) && statusTypeName != statusType.Data.StatusName)
                    statusTypeUpdateForm.StatusName = statusTypeName;
                
                var updateResult = await _statusTypeService.UpdateStatusTypeAsync(statusType.Data.Id, statusTypeUpdateForm);
                
                if (updateResult.Success)
                {
                    WriteLine($"id: {statusType.Data.Id} updated successfully.");
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
    
    #region Delete StatusType
    public async Task DeleteStatusTypeOption()
    {
        Dialogs.MenuHeading("Delete Customer");
        
        Write("Enter Customer ID: ");
        var idInput = ReadLine()!;

        if (!string.IsNullOrEmpty(idInput))
        {
            var statusType = await _statusTypeService.GetStatusTypeByIdAsync(int.Parse(idInput));
            if (statusType is Result<StatusType> { Success: true, Data: not null } statusTypeResult)
            {
                var result = await _statusTypeService.DeleteStatusTypeAsync(statusTypeResult.Data.Id);
                
                if (result.Success)
                    WriteLine($"StatusType Deleted Successfully!");
                else
                    WriteLine($"StatusType Deletion Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
            }
            else
                WriteLine($"Failed! \nReason: {statusType.ErrorMessage ?? "Unknown error."}");
        }
    }
    #endregion
}