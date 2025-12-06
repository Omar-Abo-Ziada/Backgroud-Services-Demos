using Hangfire;

using Hangfire_Api.Presistance;
using Hangfire_Api.Services;
using Hangfire_Api.Services.Product;

using Microsoft.EntityFrameworkCore;


namespace Hangfire_Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHangfireApiServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("Default");
        if (connectionString is null)
            throw new Exception("Connection string 'Default' not found.");

        /// Note: Max Pool Size=200 means SQL connection pool can hold
        /// up to 200 connections simultaneously. Prevents pool exhaustion
        /// with many Hangfire workers.
        _ = builder.Services.AddHangfire(config =>
            config.UseSqlServerStorage(connectionString));

        _ = builder.Services.AddHangfireServer();

        return services;
    }
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        _ = services.AddScoped<IWorkerService, WorkerService>();
        _ = services.AddScoped<IProductService, ProductService>();

        return services;
    }

    public static IServiceCollection RegisterDbContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("Default")
            ?? throw new Exception("Connection string 'Default' not found.");

        _ = services.AddDbContext<Context>(options =>
        {
            _ = options.UseSqlServer(connectionString);
            _ = options.LogTo(Console.WriteLine, LogLevel.Information);

            // Log SQL queries only in Development
            if (builder.Environment.IsDevelopment())
            {
                _ = options.EnableSensitiveDataLogging();
                _ = options.EnableDetailedErrors();
            }
        });

        return services;
    }
}