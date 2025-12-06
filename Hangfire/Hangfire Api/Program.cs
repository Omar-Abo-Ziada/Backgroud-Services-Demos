using Hangfire;

namespace Hangfire_Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services
        _ = builder.Services.AddControllers();
        _ = builder.Services.AddOpenApi();

        /// Note: Max Pool Size=200 means SQL connection pool can hold
        /// up to 200 connections simultaneously. Prevents pool exhaustion
        /// with many Hangfire workers.
        _ = builder.Services.AddHangfire(config =>
            config.UseSqlServerStorage(
                "Data Source=.;Initial Catalog=Hangfire-DB;" +
                "Integrated Security=true;Max Pool Size=200;" +
                "Trust Server Certificate=True;"));

        _ = builder.Services.AddHangfireServer();

        WebApplication app = builder.Build();

        // Configure pipeline
        if (app.Environment.IsDevelopment())
        {
            _ = app.MapOpenApi();
        }

        _ = app.UseHttpsRedirection();
        _ = app.UseAuthorization();
        _ = app.MapControllers();

        _ = app.UseHangfireDashboard();

        app.Run();
    }
}