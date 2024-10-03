using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;

namespace Order.API
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCarter();

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!);

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(opt => { });
            app.UseHealthChecks("/health",
                new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return app;
        }
    }
}
