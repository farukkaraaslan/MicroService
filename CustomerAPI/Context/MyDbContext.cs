using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System;

namespace CustomerAPI.Context;

public class MyDbContext : DbContext
{

    public MyDbContext() : base() { }
    public MyDbContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=10.10.10.2;Port=5432;Database=etrade; User Id=root; Password=1234;");
    }


    public DbSet<Customer> Customers { get; set; }
}
