using ExampleBackgroundService__WebAPI.Services;
using ExampleBackgroundService__WebAPI.Services.Scoped;

namespace ExampleBackgroundService__WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddHostedService<MyBackGroundService>();
        builder.Services.AddHostedService<TimedHostedService>();
        builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
        builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
