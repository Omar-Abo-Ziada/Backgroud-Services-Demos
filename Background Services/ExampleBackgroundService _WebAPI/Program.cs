using ExampleBackgroundService__WebAPI.Services.Queued;

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

        //builder.Services.AddHostedService<MyBackGroundService>();
        //builder.Services.AddHostedService<TimedHostedService>();
        //builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
        //builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();

        builder.Services.AddSingleton<MonitorLoop>();
        builder.Services.AddHostedService<QueuedHostedService>();

        builder.Services.AddSingleton<IBackgroundTaskQueue>(ctx =>
        {
            if (!int.TryParse(builder.Configuration["BackgroundServices.QueueCapacity"], out var queueCapacity))
                queueCapacity = 100;
            return new BackgroundTaskQueue(queueCapacity);
        });

        // Add hosted service that runs background jobs
        builder.Services.AddHostedService<QueuedHostedService>();

        // Resolve MonitorLoop after building the app

        var app = builder.Build();

        var monitorLoop = app.Services.GetRequiredService<MonitorLoop>();
        monitorLoop.StartMonitorLoop();

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
