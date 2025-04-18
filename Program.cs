using backend.Config;
using backend.Controllers;
using backend.Interfaces;
using backend.Repositories;
using backend.Routes;
using backend.Services;
using backend.Utils;
using DotNetEnv;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<UserService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddScoped<IUtils, Utils>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapUserRoutes();

app.Run();
