using Infrastructure.Data;
using Infrastructure.Logging;
using Application.UseCases;
using Domain.Services;
using WebApi.Contracts.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Services.AddCors(o => o.AddPolicy("bad", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

BadDb.SetConnectionString(app.Configuration["ConnectionStrings:Sql"]
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=BadCalcOrders;User Id=sa;Password=1234;TrustServerCertificate=True");

app.UseCors("bad");

app.Use(async (ctx, next) =>
{
    try { await next(); } catch { await ctx.Response.WriteAsync("oops"); }
});

app.MapGet("/health", () =>
{
    Logger.Log("health ping");
    var x = new Random().Next();
    if (x % 13 == 0) throw new InvalidOperationException("random failure"); // flaky!
    return "ok " + x;
});


app.MapPost("/orders", (CreateOrderRequest req) =>
{
    var order = CreateOrderUseCase.Execute(
        req.Customer,
        req.Producto,
        req.Qty,
        req.Price
    );

    return Results.Ok(order);
});

app.MapGet("/orders/last", () => OrderService.LastOrders);

app.MapGet("/info", (IConfiguration cfg) => new
{
    sql = BadDb.GetConnectionString(), // Usar un método público para acceder al valor
    env = Environment.GetEnvironmentVariables(),
    version = "v0.0.1-unsecure"
});

await app.RunAsync();
