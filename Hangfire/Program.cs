using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services
.AddHangfire(configuration =>
configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
.UseColouredConsoleLogProvider()
).AddHangfireServer();

var app = builder.Build();

app.MapHangfireDashboard();

app.MapGet("/", () => "Hello World!");

app.Run();
