using Hangfire_Api.Presistance;

namespace Hangfire_Api.Services.Product;

public class ProductService(Context context) : IProductService
{
    public Task DoSomeIntensicveWork()
    {
        Thread.Sleep(10_000); // Simulate a long-running operation

        Console.WriteLine("Finished intensive work");
        return Task.CompletedTask;
    }
}