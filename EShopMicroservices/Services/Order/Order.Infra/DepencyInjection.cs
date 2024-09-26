using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Infra
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            //Add services to the container
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
