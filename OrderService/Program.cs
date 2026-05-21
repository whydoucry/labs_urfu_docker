using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Redis
var redis = await ConnectionMultiplexer.ConnectAsync("redis:6379");

builder.Services.AddSingleton(redis.GetDatabase());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/orders", async (Order order, IDatabase redisDb) =>
{
    var orderId = Guid.NewGuid().ToString();

    await redisDb.StringSetAsync(
        $"order:{orderId}",
        System.Text.Json.JsonSerializer.Serialize(order)
    );

    return Results.Ok(new
    {
        OrderId = orderId,
        Status = "Created"
    });
});

app.Run();

record Order(string ProductName, int Quantity);