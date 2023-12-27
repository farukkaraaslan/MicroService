
using Microsoft.EntityFrameworkCore;
using ProductAPI.Context;
using ProductAPI.Helper;
using ProductAPI.Interface;

namespace ProductAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<MyDbContext>(options =>
        {
            options.UseNpgsql("Server=localhost;Port=5432;Database=etrade; User Id=root; Password=1234;");
        });
        builder.Services.AddTransient<IServiceCallHelper,ServiceCallHelper>();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
