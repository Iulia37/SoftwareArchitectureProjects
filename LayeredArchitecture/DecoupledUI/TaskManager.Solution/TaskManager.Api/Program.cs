using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nelibur.ObjectMapper;
using System.Text;
using TaskManager.BusinessLogic.Services;
using TaskManager.DataAccess.Contexts;
using TaskManager.DataAccess.Repositories;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Domain.Repositories;
using TaskManager.DTO.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configurare DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

TinyMapper.Bind<Project, ProjectDTO>();
TinyMapper.Bind<ProjectDTO, Project>();

TinyMapper.Bind<TaskItem, TaskItemDTO>();
TinyMapper.Bind<TaskItemDTO, TaskItem>();

TinyMapper.Bind<User, UserDTO>();
TinyMapper.Bind<UserDTO, User>();

TinyMapper.Bind<User, LoginUserDTO>();
TinyMapper.Bind<LoginUserDTO, User>();

TinyMapper.Bind<User, RegisterUserDTO>();
TinyMapper.Bind<RegisterUserDTO, User>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAngularApp");

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowApi",
//        policy => policy.WithOrigins("https://localhost:7036")
//                        .AllowAnyHeader()
//                        .AllowAnyMethod());
//});

//var app = builder.Build();

//app.UseCors("AllowApi");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
