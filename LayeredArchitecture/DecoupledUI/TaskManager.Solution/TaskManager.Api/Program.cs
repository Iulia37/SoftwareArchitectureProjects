using Microsoft.EntityFrameworkCore;
using TaskManager.BusinessLogic.Services;
using TaskManager.DataAccess.Contexts;
using TaskManager.DataAccess.Repositories;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Repositories;
using Nelibur.ObjectMapper;
using TaskManager.Domain.Models;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowBlazor");

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
