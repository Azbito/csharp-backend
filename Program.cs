using backend.Config;
using backend.Extensions;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationConfig();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAppServices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseAppRoutes();

app.Run();
