using static System.Console;
using Business.Interfaces;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class StatusTypeDialogs(IStatusTypeService statusTypeService) : IStatusTypeDialogs
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    
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
        }
    }

    private async Task CreateStatusTypeOption()
    {
        throw new NotImplementedException();
    }

    private async Task ViewAllStatusTypesOption()
    {
        throw new NotImplementedException();
    }

    private async Task ViewStatusTypeOption()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateStatusTypeOption()
    {
        throw new NotImplementedException();
    }

    private async Task DeleteStatusTypeOption()
    {
        throw new NotImplementedException();
    }
}