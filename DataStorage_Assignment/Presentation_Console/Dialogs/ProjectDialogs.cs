using static System.Console;
using Business.Interfaces;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class ProjectDialogs(IProjectService projectService) : IProjectDialogs
{
    private readonly IProjectService _projectService = projectService;
    
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
        }
    }

    private async Task CreateProjectOption()
    {
        Dialogs.MenuHeading("Create Project");
    }

    private async Task ViewAllProjectsOption()
    {
        Dialogs.MenuHeading("View All Projects");
    }

    private async Task ViewProjectOption()
    {
        Dialogs.MenuHeading("View Project");
    }

    private async Task UpdateProjectOption()
    {
        Dialogs.MenuHeading("Update Project");
    }

    private async Task DeleteProjectOption()
    {
        Dialogs.MenuHeading("Delete Project");
    }
}