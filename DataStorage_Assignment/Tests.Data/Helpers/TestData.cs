using Data.Entities;

namespace Tests.Data.Helpers;

public class TestData
{
    public static CustomerEntity MakeTestCustomer() => new()
    {
        CustomerName = "Company Inc."
    };

    public static UserEntity MakeTestUser() => new()
    {
        FirstName = "Slisk",
        LastName = "Lindqvist",
        Email = "slisk.lindqvist@querzion.com"
    };
    
    public static UserEntity MakeTestUser(string firstName, string lastName, string email) => new()
    {
        FirstName = firstName,
        LastName = lastName,
        Email = email
    };

    public static UserEntity MakeTestUser1() => MakeTestUser("Slisk", "Lindqvist", "slisk.lindqvist@querzion.com");

    public static UserEntity MakeTestUser2() => MakeTestUser("Anna", "Andersson", "anna.andersson@querzion.com");

    public static UserEntity MakeTestUser3() => MakeTestUser("Bjorn", "Karlsson", "bjorn.karlsson@querzion.com");

    public static UserEntity MakeTestUser4() => MakeTestUser("Clara", "Johansson", "clara.johansson@querzion.com");
    
    
    public static StatusTypeEntity MakeTestStatusType() => new()
    {
        StatusName = "Active"
    };

    public static ProductEntity MakeTestProduct() => new()
    {
        ProductName = "Product Inc.",
        Price = 100
    };

    public static ProjectEntity MakeTestProject() => new()
    {
        Title = "Project Doom",
        Description = "Test project creation.",
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(3),
        CustomerId = 1,
        StatusId = 1,
        UserId = 1,
        ProductId = 1
    };
}