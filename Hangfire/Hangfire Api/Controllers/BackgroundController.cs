using Hangfire;

using Microsoft.AspNetCore.Mvc;

namespace Hangfire_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackgroundController : ControllerBase
{
    [HttpGet("FireAndForgetJob")]
    public ActionResult CreateFireAndForgetJob()
    {
        //BackgroundJob.Enqueue(() => Console.WriteLine("hello testing hangfire FireAndForgetJob"));
        _ = BackgroundJob.Enqueue(() => DoBigJob());
        return Ok("Returned from FireAndForgetJob");
    }

    [HttpGet("DelayedJob")]
    public ActionResult DelayedJob()
    {
        string jobId = BackgroundJob.Schedule(() => Console.WriteLine("hello testing hangfire DelayedJob"), TimeSpan.FromSeconds(10));
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