using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using RestaurantService.API.Data;
using RestaurantService.API.DTOs;
using RestaurantService.API.Services;
using RestaurantService.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService.API.Services.RestaurantService>();

TinyMapper.Bind<RestaurantService.API.Models.Restaurant, RestaurantDTO>();
TinyMapper.Bind<RestaurantDTO, RestaurantService.API.Models.Restaurant>();

TinyMapper.Bind<MenuItem, MenuItemDTO>();
TinyMapper.Bind<MenuItemDTO, MenuItem>();

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

app.UseStaticFiles();

app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
