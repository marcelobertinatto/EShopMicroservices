using System.Reflection;

namespace Order.API
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //services.AddCarter();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            //app.MapCarter();

            return app;
        }
    }
}
