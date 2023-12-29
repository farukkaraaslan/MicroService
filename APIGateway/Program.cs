using CacheManager.Core;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("ocelot.json");
        builder.Services.AddOcelot().AddCacheManager(
            x =>
            {
                x.WithRedisConfiguration("redis", config =>
                {
                    config.WithAllowAdmin()
                    .WithDatabase(0)
                    .WithEndpoint("localhost", 6379);
                })
                .WithJsonSerializer()
                .WithRedisCacheHandle("redis")
                .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromSeconds(10));
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        await app.UseOcelot();
        app.Run();
    }
}