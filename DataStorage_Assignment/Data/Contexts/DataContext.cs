using Data.Entities;
using Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    // ChatGPT override.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlite(DatabaseHelper.GetSQLiteDatabaseConnectionString()) // Your SQLite connection string
                .UseLazyLoadingProxies(); // Enable lazy loading
        }
    }
    
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<StatusTypeEntity> StatusTypes { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
}