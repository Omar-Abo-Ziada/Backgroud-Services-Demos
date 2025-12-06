using Hangfire;

using Hangfire_Api.Services;
using Hangfire_Api.Services.Product;

using Microsoft.AspNetCore.Mvc;

namespace Hangfire_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackgroundController(IProductService productService) : ControllerBase
{

    [HttpGet("FireAndForget")]
    public async Task<ActionResult> FireAndForget()
    {
        //BackgroundJob.Enqueue(() => Console.WriteLine("hello testing hangfire FireAndForgetJob"));

        _ = BackgroundJob.Enqueue(() => DoBigJob());

        //_ = BackgroundJob.Enqueue<IWorkerService>(workerService => workerService.DoSomeWork());

        //_ = BackgroundJob.Enqueue<IProductService>(productService => productService.GetProductsCountAsync());

        return Ok($"Returned From Fire And Forget");
    }


    [HttpGet("DoWorkForeground")]
    public async Task<ActionResult> DoWorkForeground()
    {
        //BackgroundJob.Enqueue(() => Console.WriteLine("hello testing hangfire FireAndForgetJob"));

        //_ = BackgroundJob.Enqueue(() => DoBigJob());

        //_ = BackgroundJob.Enqueue<IWorkerService>(workerService => workerService.DoSomeWork());

        await productService.DoSomeIntensicveWork();

        //_ = BackgroundJob.Enqueue<IProductService>(productService => productService.GetProductsCountAsync());

        return Ok($"Returned After Doing Some Intensive Work ...");
    }

    [HttpGet("DoWorkBackground")]
    public async Task<ActionResult> DoWorkBackground()
    {
        //BackgroundJob.Enqueue(() => Console.WriteLine("hello testing hangfire FireAndForgetJob"));

        //_ = BackgroundJob.Enqueue(() => DoBigJob());

        //_ = BackgroundJob.Enqueue<IWorkerService>(workerService => workerService.DoSomeWork());

        _ = BackgroundJob.Enqueue<IProductService>(productService => productService.DoSomeIntensicveWork());

        return Ok($"Returned Immediately and left the intensive work to run in background...");
    }

    [HttpGet("DelayedJob")]
    public ActionResult DelayedJob()
    {
        //string jobId = BackgroundJob.Schedule(() => Console.WriteLine("hello testing hangfire DelayedJob"), TimeSpan.FromSeconds(10));
        string jobId = BackgroundJob.Schedule<IWorkerService>(workerService => workerService.DoSomeWork(), TimeSpan.FromSeconds(10));
        _ = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continue after the delayed job."));
        return Ok("Returned from DelayedJob");
    }

    [HttpGet("ContinuationJob")]
    public ActionResult ContinuationJob()
    {
        string jobId = BackgroundJob.Enqueue(() => Console.WriteLine("hello testing hangfire ContinuationJob"));
        _ = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation!"));
        return Ok("Returned from ContinuationJob");
    }

    [HttpGet("RecurringJob")]
    public IActionResult recurringJob()
    {
        RecurringJob.AddOrUpdate("myrecurringjob", () => Console.WriteLine("hello testing hangfire RecurringJob"), Cron.Minutely());
        //sendmail();
        return Ok("recurring job is done");
    }

    [HttpGet("BatchJob")]
    public IActionResult BatchJob()
    {
        /*
        Pro Only
        This feature is a part of Hangfire Pro package set
        */
        //var batchId = BatchJob .StartNew(x =>
        //{
        //    x.Enqueue(() => Console.WriteLine("BatchJob Job 1"));
        //    x.Enqueue(() => Console.WriteLine("BatchJob Job 2"));
        //});

        return NotFound("This feature is a part of Hangfire Pro package set");
    }

    // --------- Private Helpers ---------
    private void Job1()
    {
        Console.WriteLine("BatchJob Job 1");
    }

    private void Job2()
    {
        Console.WriteLine("BatchJob Job 2");
    }

    // Must Be Public to be callable from Hangfire server
    // If u wanna  work around it make it call another private method
    public void DoBigJob()
    {
        Console.WriteLine("Starting DoBigJob");

        for (int i = 0; i < 5_00_000; i++)
        {
            Console.WriteLine($"loop number : {i} ");
        }

        Console.WriteLine("Ending DoBigJob");
    }
}