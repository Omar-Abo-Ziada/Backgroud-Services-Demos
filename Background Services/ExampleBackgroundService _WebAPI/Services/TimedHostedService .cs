namespace ExampleBackgroundService__WebAPI.Services;

/// <summary>
/// Demonstrates implementing <see cref="IHostedService"/> directly.
/// Uses a <see cref="Timer"/> to execute work on a fixed interval.
/// This service does NOT run a continuous loop like BackgroundService.
/// </summary>
public class TimedHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer? _timer = null;

    /// <summary>
    /// Constructs the timed service with logging support.
    /// </summary>
    public TimedHostedService(ILogger<TimedHostedService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Called when the host starts.
    /// Sets up a timer that triggers every 5 seconds.
    /// </summary>
    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    /// <summary>
    /// Executed every time the timer interval elapses.
    /// </summary>
    private void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);

        _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
    }

    /// <summary>
    /// Called when the host is shutting down.
    /// Disables the timer so it stops triggering work.
    /// </summary>
    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Disposes the timer to free resources.
    /// </summary>
    public void Dispose()
    {
        _timer?.Dispose();
    }
}
