using Domain.Contracts;
using E_Commerce.Middlewares;

namespace E_Commerce.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            // Create Object From Type Implementing IDbInitializer
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }
        public static WebApplication UseCustomMiddlewareException(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionsHandlingMiddleware>();
            return app;

        }


    }
}
