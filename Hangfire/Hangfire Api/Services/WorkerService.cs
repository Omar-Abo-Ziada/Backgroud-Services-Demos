namespace Hangfire_Api.Services;

public class WorkerService(ILogger<WorkerService> logger) : IWorkerService
{
    public void DoSomeWork()
    {
        logger.LogInformation("Doing MY Work From Scoped Service ...");
    }
}