using Microsoft.EntityFrameworkCore;
using ProductAPI.Model;

namespace ProductAPI.Context;

public class MyDbContext : DbContext
{
    public MyDbContext() : base() { }
    public MyDbContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=products; User Id=root; Password=1234;");
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
