using static System.Console;
using Business.Interfaces;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class ProductDialogs(IProductService productService, IMainMenuDialog mainMenuDialog) : IProductDialogs
{
    private readonly IProductService _productService = productService;
    private readonly IMainMenuDialog _mainMenuDialog = mainMenuDialog;
    
    public async Task MenuOptions()
    {
        while (true)
        {
            Clear();
            Dialogs.MenuHeading("Products Options");
            WriteLine("Choose an option:");
            WriteLine("1. Create new Product");
            WriteLine("2. View All Products");
            WriteLine("3. View Product");
            WriteLine("4. Update Product");
            WriteLine("5. Delete Product");
            WriteLine("0. Back to Main Menu");
            Write("Select an option: ");
            var input = ReadLine();

            switch (input)
            {
                case "1":
                    await CreateProductOption();
                    break;
                case "2":
                    await ViewAllProductsOption();
                    break;
                case "3":
                    await ViewProductOption();
                    break;
                case "4":
                    await UpdateProductOption();
                    break;
                case "5":
                    await DeleteProductOption();
                    break;
                case "0":
                    await _mainMenuDialog.ShowMainMenu();
                    break;
                default:
                    WriteLine("Invalid selection. Please try again.");
                    Task.Delay(1500).Wait(); // Pause for Product to read message
                    break;
            }
        }
    }

    private async Task CreateProductOption()
    {
        throw new NotImplementedException();
    }

    private async Task ViewAllProductsOption()
    {
        throw new NotImplementedException();
    }

    private async Task ViewProductOption()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateProductOption()
    {
        throw new NotImplementedException();
    }

    private async Task DeleteProductOption()
    {
        throw new NotImplementedException();
    }
}