using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;

namespace TaskApi
{
    public static class DI
    {
        public static IServiceCollection AddMyContext(this IServiceCollection services, IConfiguration configuration) 
        {
            var cosmos = new CosmosConfig();
            configuration.GetSection("Cosmos").Bind(cosmos);

            services.AddDbContext<AppDbContext>(op => op.UseCosmos(cosmos.ConnectionString, cosmos.DatabaseName));

            return services;
        }
    }
}
