using backend.Controllers;
using backend.Interfaces;
using backend.Repositories;
using backend.Services;
using backend.Utils;
using FluentValidation;

namespace backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IStringCase, StringCase>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserController, UserController>();
            services.AddScoped<IPostController, PostController>();
            services.AddScoped<AuthController>();

            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

            services.AddScoped<IUtils, backend.Utils.Utils>();

            services.AddControllers();

            return services;
        }
    }
}
