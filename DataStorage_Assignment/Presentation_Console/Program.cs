using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation_Console.Dialogs;
using Presentation_Console.Interfaces;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlite("Data Source=../../../../Data/Databases/SQLite_Database.db"))
    .AddScoped<IStatusTypeRepository, StatusTypeRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IProjectRepository, ProjectRepository>()

    .AddScoped<IStatusTypeService, StatusTypeService>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IProjectService, ProjectService>()

    .AddScoped<IStatusTypeDialogs, StatusTypeDialogs>()
    .AddScoped<IUserDialogs, UserDialogs>()
    .AddScoped<ICustomerDialogs, CustomerDialogs>()
    .AddScoped<IProductDialogs, ProductDialogs>()
    .AddScoped<IProjectDialogs, ProjectDialogs>()
    .AddScoped<IMainMenuDialog, MainMenuDialog>();

var serviceProvider = services.BuildServiceProvider();
var menuDialog = serviceProvider.GetRequiredService<IMainMenuDialog>();
await menuDialog.ShowMainMenu();