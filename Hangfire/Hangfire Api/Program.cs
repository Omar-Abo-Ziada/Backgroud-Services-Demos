using Hangfire;

using Hangfire_Api.Extensions;

namespace Hangfire_Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services
        _ = builder.Services.AddControllers();
        _ = builder.Services.AddOpenApi();

        _ = builder.Services.RegisterServices();
        _ = builder.Services.AddHangfireApiServices(builder);

        _ = builder.Services.RegisterDbContext(builder);

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