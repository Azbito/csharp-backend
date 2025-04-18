using backend.Config;
using Microsoft.EntityFrameworkCore;

namespace backend.Config
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = Environment.GetEnvironmentVariable(
                "ConnectionStrings__DefaultConnection"
            );
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
            return services;
        }
    }
}
