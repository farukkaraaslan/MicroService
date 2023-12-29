
using CustomerAPI.Context;
using CustomerAPI.Helper;
using CustomerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<MyDbContext>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IServiceCallHelper, ServiceCallHelper>();
        builder.Services.AddScoped<ICapHelper,CapHelper>();

        builder.Services.AddCap(options =>
        {
            options.UseEntityFramework<MyDbContext>();
            options.UsePostgreSql("Server=localhost;Port=5432;Database=customers; User Id=root; Password=1234;");
            options.UseRabbitMQ(Roptions =>
            {

                Roptions.ConnectionFactoryOptions = Foptions =>
                {
                    Foptions.Ssl.Enabled = false;
                    Foptions.HostName = "localhost";
                    Foptions.UserName = "guest";
                    Foptions.Password = "guest";
                    Foptions.Port = 5672;
                };
            });
        });


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
