var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => $"Current time: {DateTime.Now:HH:mm:ss}");

app.MapGet("/time", () => new 
{
    CurrentTime = DateTime.Now,
    TimeZone = TimeZoneInfo.Local.DisplayName,
    UtcTime = DateTime.UtcNow
});

app.Run();
