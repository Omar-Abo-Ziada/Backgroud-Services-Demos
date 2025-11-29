namespace ExampleBackgroundService__WebAPI.Services;

/// <summary>
/// A simple background service that runs continuously while the application is running.
/// Demonstrates the basic pattern of <see cref="BackgroundService"/>:
/// a long-running loop that exits when the cancellation token is triggered.
/// </summary>
internal class MyBackGroundService(ILogger<MyBackGroundService> _logger) : BackgroundService
{
    /// <summary>
    /// The main execution loop for this background service.
    /// Runs until the host triggers a cancellation (on shutdown).
    /// </summary>
    /// <param name="stoppingToken">Triggered when the host is shutting down.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Background service is running at: {time}", DateTimeOffset.Now);

            // Example work happens here...
            await Task.Delay(5000, stoppingToken);
        }
    }
}
