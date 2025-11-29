namespace ExampleBackgroundService__WebAPI.Services.Scoped;

/// <summary>
/// This is a BackgroundService (singleton) that needs to use a scoped service.
/// Since singletons cannot consume scoped dependencies directly,
/// this service manually creates a scope using <see cref="IServiceProvider.CreateScope"/>.
/// 
/// Inside this scope, it resolves <see cref="IScopedProcessingService"/> and runs it.
/// </summary>
public class ConsumeScopedServiceHostedService : BackgroundService
{
    private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

    /// <summary>
    /// Constructor receives the root <see cref="IServiceProvider"/> so we can create scopes.
    /// </summary>
    public ConsumeScopedServiceHostedService(IServiceProvider services,
        ILogger<ConsumeScopedServiceHostedService> logger)
    {
        Services = services;
        _logger = logger;
    }

    /// <summary>
    /// The application root service provider.
    /// Used to create child scopes.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// The main execution loop for the hosted service.
    /// Creates a scope and delegates work to the scoped service.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Consume Scoped Service Hosted Service running.");

        await DoWork(stoppingToken);
    }

    /// <summary>
    /// Creates a new DI scope, resolves the scoped processing service,
    /// and runs its loop until cancellation is requested.
    /// </summary>
    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Consume Scoped Service Hosted Service is working.");

        using var scope = Services.CreateScope();

        var scopedProcessingService =
            scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

        await scopedProcessingService.DoWork(stoppingToken);
    }

    /// <summary>
    /// Called when the host is stopping.
    /// </summary>
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}
