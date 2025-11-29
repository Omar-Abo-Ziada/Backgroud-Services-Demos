namespace ExampleBackgroundService__WebAPI.Services.Queued;

public interface IBackgroundTaskQueue
{
    ValueTask EnqueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem);

    ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
        CancellationToken cancellationToken);
}