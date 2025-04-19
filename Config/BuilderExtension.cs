using backend.Routes;

namespace backend.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseAppRoutes(this WebApplication app)
        {
            app.MapUserRoutes();
            app.MapPostRoutes();
            app.MapAuthRoutes();

            return app;
        }
    }
}
