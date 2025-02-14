using Business.Factories;
using static System.Console;
using Business.Interfaces;
using Business.Models;
using Presentation_Console.Interfaces;

namespace Presentation_Console.Dialogs;

public class ProductDialogs(IProductService productService) : IProductDialogs
{
    private readonly IProductService _productService = productService;

    #region Menu
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
                    return;
                default:
                    WriteLine("Invalid selection. Please try again.");
                    Task.Delay(1500).Wait(); // Pause for Product to read message
                    break;
            }
        }
    }
    #endregion

    #region Create
    public async Task CreateProductOption()
    {
        Dialogs.MenuHeading("Create Product");
        
        var product = ProductFactory.CreateRegistrationForm();
        
        Write("Enter Product Name: ");
        product.ProductName = ReadLine()!;
        Write("Enter Product Price: ");
        product.Price = Convert.ToInt32(ReadLine());
        
        var result = await _productService.CreateProductAsync(product);
        
        if (result.Success)
            WriteLine($"Product Created Successfully!");
        else
            WriteLine($"Product Creation Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
        
        ReadKey();
    }
    #endregion

    #region View
    public async Task ViewAllProductsOption()
    {
        Dialogs.MenuHeading("View All Products");
        
        var result = await _productService.GetAllProductsAsync();
        
        if (result is Result<IEnumerable<Product>> { Success: true, Data: not null } productResult)
        {
            foreach (var product in productResult.Data)
            {
                WriteLine($"ID: {product.Id}, Name: {product.ProductName}, Price: {product.Price}.");
            }
        }
        else
            WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
        
        ReadKey();
    }

    public async Task ViewProductOption()
    {
        Dialogs.MenuHeading("View Product");
        
        WriteLine("Choose an option:");
        WriteLine("1. Search by ID");
        WriteLine("2. Search by Name");
        WriteLine("0. Back");
        Write("Select an option: ");
        var input = ReadLine();

        switch (input)
        {
            case "1":
                Dialogs.MenuHeading("Search by Id");
                
                Write("Enter Customer ID: ");
                var idInput = ReadLine();
                
                // ChatGPT Generated. (It forces the input to be of variable integer.)
                if (int.TryParse(idInput, out var productId))
                {
                    var result = await _productService.GetProductByIdAsync(productId);

                    if (result is Result<Product> { Success: true, Data: not null } productResultId)
                        WriteLine($"Product with ID: {productResultId.Data.Id} found. Name: {productResultId.Data.ProductName}, Price: {productResultId.Data.Price}.");
                    else
                        WriteLine($"Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
                }
                else
                {
                    WriteLine("Invalid ID format.");
                }
                
                break;
            
            case "2":
                Dialogs.MenuHeading("Search by Name");
                
                Write("Enter Customer Name: ");
                var nameInput = ReadLine()!;
                var searchResult = await _productService.GetProductByNameAsync(nameInput);

                if (searchResult is Result<Product> { Success: true, Data: not null } productResultName)
                {
                    WriteLine($"Product Found: ID: {productResultName.Data.Id}, Name: {productResultName.Data.ProductName}, Price: {productResultName.Data.Price}.");
                }
                else
                {
                    WriteLine($"Failed! \nReason: {searchResult.ErrorMessage ?? "Unknown error."}");
                }
                
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
    #endregion
    
    #region Update
    public async Task UpdateProductOption()
    {
        Dialogs.MenuHeading("Update Product");
        
        Write("Product Name: ");
        var nameInput = ReadLine()!;
        
        if(!string.IsNullOrEmpty(nameInput))
        {
            var result = await _productService.GetProductByNameAsync(nameInput);
            if (result is Result<Product> { Success: true, Data: not null } product)
            {
                WriteLine($"Id: {product.Data.Id} \nName: {product.Data.ProductName}, Price: {product.Data.Price}.");
                WriteLine("");

                var productUpdateForm = ProductFactory.CreateUpdateForm();
                productUpdateForm.Id = product.Data.Id;
                
                Write("New Product Name: ");
                var productName = ReadLine()!;
                if (!string.IsNullOrEmpty(productName) && productName != product.Data.ProductName)
                    productUpdateForm.ProductName = productName;
                
                Write("New Product Price: ");
                var priceInput = ReadLine();
                if (!string.IsNullOrEmpty(priceInput) && int.TryParse(priceInput, out int productPrice))
                {
                    if (productPrice != product.Data?.Price)  // Ensure product.Data is not null
                    {
                        productUpdateForm.Price = productPrice;
                    }
                }
                else
                {
                    WriteLine("Invalid input. Please enter a valid price.");
                }
                
                var updateResult = await _productService.UpdateProductAsync(product.Data.Id, productUpdateForm);
                
                if (updateResult.Success)
                {
                    WriteLine($"id: {product.Data.Id} updated successfully.");
                }
                else
                {
                    WriteLine($"Failed to update! \nReason: {updateResult.ErrorMessage ?? "Unknown error."}");
                }
            }
            else
            {
                WriteLine($"Field cannot be empty. Please try again.");
            }
            
            ReadKey();
        }
    }
    #endregion
    
    #region Delete
    public async Task DeleteProductOption()
    {
        Dialogs.MenuHeading("Delete Customer");
        
        Write("Enter Customer ID: ");
        var idInput = ReadLine()!;

        if (!string.IsNullOrEmpty(idInput))
        {
            var product = await _productService.GetProductByIdAsync(int.Parse(idInput));
            if (product is Result<Product> { Success: true, Data: not null } productResult)
            {
                var result = await _productService.DeleteProductAsync(productResult.Data.Id);
                
                if (result.Success)
                    WriteLine($"Product Deleted Successfully!");
                else
                    WriteLine($"Product Deletion Failed! \nReason: {result.ErrorMessage ?? "Unknown error."}");
            }
            else
                WriteLine($"Failed! \nReason: {product.ErrorMessage ?? "Unknown error."}");
        }
        
        ReadKey();
    }
    #endregion
}