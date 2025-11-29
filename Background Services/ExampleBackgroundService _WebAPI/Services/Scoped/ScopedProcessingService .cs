namespace ExampleBackgroundService__WebAPI.Services.Scoped;

/// <summary>
/// A SCOPED service that performs long-running background work.
/// This cannot be injected directly into a BackgroundService (singleton),
/// because scoped services like(DbContext,repos ... etc) cannot be injected into singletons.
/// 
/// Instead, a BackgroundService must CREATE a scope manually and resolve this service.
/// </summary>
public class ScopedProcessingService : IScopedProcessingService
{
    private int executionCount = 0;
    private readonly ILogger _logger;

    /// <summary>
    /// Constructs this scoped service.
    /// You can inject any other scoped services here (DbContext, etc).
    /// </summary>
    public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Executes repeated work inside a loop until the cancellation token triggers.
    /// This method is called by a hosted service that created a scope.
    /// </summary>
    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            executionCount++;

            _logger.LogInformation(
                "Scoped Processing Service is working. Count: {Count}", executionCount);

            await Task.Delay(10000, stoppingToken);
        }
    }
}
