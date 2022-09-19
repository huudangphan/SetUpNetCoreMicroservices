using Manufacture.Api.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Manufacture.Api.services.Interfaces;
using Manufacture.Api.services.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IProduct,ProductRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ManufactureContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDB"));
});

builder.Services.AddMassTransit(options => {
    options.UsingRabbitMq((context,cfg) => {
        cfg.Host(new Uri("rabbitmq://localhost:4001"), h => {
            h.Username("guest");
            h.Password("guest");
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
