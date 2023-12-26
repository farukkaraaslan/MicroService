using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomerAPI.Context;

public class MyDbContext : DbContext
{

    public MyDbContext() : base() { }
    public MyDbContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server= localhost;Port=5432;Database=etrade; User Id=root; Password=1234;");
    }


    public DbSet<Customer> Customers { get; set; }
}
