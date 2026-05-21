using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// GET /products - получить все продукты
app.MapGet("/products", async (AppDbContext db) => 
    await db.Products.ToListAsync());

// GET /products/{id} - получить продукт по ID
app.MapGet("/products/{id}", async (int id, AppDbContext db) => 
    await db.Products.FindAsync(id) is Product product 
        ? Results.Ok(product) 
        : Results.NotFound());

// POST /products - создать новый продукт
app.MapPost("/products", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});

app.Run();
