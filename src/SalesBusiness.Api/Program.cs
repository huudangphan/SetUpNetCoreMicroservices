using MassTransit;
using Microsoft.EntityFrameworkCore;
using SalesBusiness.Api.services.Interfaces;
using SalesBusiness.Api.services.Repositories;
using SalesBusiness.Api.Data;
using SalesBusiness.Api.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ICategories, CategoryRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SalesBusinessContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDB"));
});
builder.Services.AddMassTransit(x => {
    x.AddConsumer<TestConsumer>();
    x.UsingRabbitMq((cxt, cfg) => {
        cfg.Host(new Uri("rabbitmq://localhost:4001"), h => {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("event-listener", e =>
        {
            e.ConfigureConsumer<TestConsumer>(cxt);
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
