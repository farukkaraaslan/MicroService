
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
        builder.Services.AddDbContext<MyDbContext>();
        builder.Services.AddTransient<IServiceCallHelper, ServiceCallHelper>();
        builder.Services.AddTransient<ICapHelper,CapHelper>();

        builder.Services.AddCap(options =>
        {
            options.UseEntityFramework<MyDbContext>();
            options.UsePostgreSql("Server=localhost;Port=5432;Database=products; User Id=root; Password=1234;");
            options.UseDashboard(o => o.PathMatch = "/cap-dashboard");
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
