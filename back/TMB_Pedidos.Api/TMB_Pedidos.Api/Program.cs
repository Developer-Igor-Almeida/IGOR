using Microsoft.EntityFrameworkCore;
using TMB_Pedidos.Application.Orders;
using TMB_Pedidos.Domain.Orders;
using TMB_Pedidos.Infrastructure.Database;
using TMB_Pedidos.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("TMB_Connection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use HTTPS apenas fora do container
if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != "true")
{
    app.UseHttpsRedirection();
}

// Swagger sempre habilitado dentro do container
if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TMB Pedidos API V1");
        c.RoutePrefix = string.Empty; // acessar em /
    });
}

app.UseAuthorization();

app.MapControllers();

// **IMPORTANTE**: escuta em todas interfaces dentro do container
app.Urls.Add("http://0.0.0.0:5000");

app.Run();
