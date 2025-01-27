using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<StatusTypeEntity> StatusTypes { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    
}