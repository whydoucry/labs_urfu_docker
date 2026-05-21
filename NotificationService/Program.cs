var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/notify", async () =>
{
    await Task.Delay(100);

    return Results.Ok(new
    {
        Message = "Notification sent"
    });
});

app.Run();