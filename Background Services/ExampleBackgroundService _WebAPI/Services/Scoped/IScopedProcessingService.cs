namespace ExampleBackgroundService__WebAPI.Services.Scoped;

public interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);

}
