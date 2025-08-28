using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using OrderService.API.Data;
using OrderService.API.Services;
using OrderService.API.DTOs;
using OrderService.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService.API.Services.OrderService>();

TinyMapper.Bind<OrderDTO, Order>();
TinyMapper.Bind<Order, OrderDTO>();

TinyMapper.Bind<OrderItemDTO, OrderItem>();
TinyMapper.Bind<OrderItem, OrderItemDTO>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularApp");

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
