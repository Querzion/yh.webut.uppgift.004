using Data.Entities;

namespace Tests.Data.Helpers;

public class TestData
{
    public static CustomerEntity MakeTestCustomer() => new()
    {
        CustomerName = "Company Inc."
    };
    public static CustomerEntity MakeTestCustomer(string customerName) => new()
    {
        CustomerName = customerName
    };
    public static CustomerEntity MakeTestCustomer1() => MakeTestCustomer("Company Inc.");
    public static CustomerEntity MakeTestCustomer2() => MakeTestCustomer("Hug Network Inc.");
    public static CustomerEntity MakeTestCustomer3() => MakeTestCustomer("Querzion Inc.");
    public static CustomerEntity MakeTestCustomer4() => MakeTestCustomer("SubstansInformation Inc.");
    
    // ________________________________________________________________________________
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
    
    // ________________________________________________________________________________
    public static StatusTypeEntity MakeTestStatusType() => new()
    {
        StatusName = "Active"
    };
    public static StatusTypeEntity MakeTestStatusType(string statusName) => new()
    {
        StatusName = statusName
    };
    public static StatusTypeEntity MakeTestStatusType1() => MakeTestStatusType("Active");
    public static StatusTypeEntity MakeTestStatusType2() => MakeTestStatusType("InActive");
    public static StatusTypeEntity MakeTestStatusType3() => MakeTestStatusType("Completed");
    public static StatusTypeEntity MakeTestStatusType4() => MakeTestStatusType("Pending");

    // ________________________________________________________________________________
    public static ProductEntity MakeTestProduct() => new()
    {
        ProductName = "Product 1",
        Price = 100
    };
    public static ProductEntity MakeTestProduct(string productName, decimal price) => new()
    {
        ProductName = productName,
        Price = price
    };
    public static ProductEntity MakeTestProduct1() => MakeTestProduct("Candy", 10);
    public static ProductEntity MakeTestProduct2() => MakeTestProduct("Car", 50000);
    public static ProductEntity MakeTestProduct3() => MakeTestProduct("Magazine", 90);
    public static ProductEntity MakeTestProduct4() => MakeTestProduct("Dog", 7000);

    // ________________________________________________________________________________
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
    public static ProjectEntity MakeTestProject(string title, string description, DateTime startDate, DateTime endDate, int customerId, int statusId, int userId, int productId) => new();
    public static ProjectEntity MakeTestProject1() => MakeTestProject("Project Doom 1", "Test Project Creation", DateTime.Now, DateTime.Now.AddDays(3), 1, 1, 1, 1 );
    public static ProjectEntity MakeTestProject2() => MakeTestProject("Project Doom 2", "Test Project Creation", DateTime.Now, DateTime.Now.AddDays(4), 1, 1, 1, 1 );
    public static ProjectEntity MakeTestProject3() => MakeTestProject("Project Doom 3", "Test Project Creation", DateTime.Now, DateTime.Now.AddDays(5), 1, 1, 1, 1 );
    public static ProjectEntity MakeTestProject4() => MakeTestProject("Project Doom 4", "Test Project Creation", DateTime.Now, DateTime.Now.AddDays(6), 1, 1, 1, 1 );
}