using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using TaskApi.Services.Interfaces;
using TaskApi.Services;

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
        public static IServiceCollection AddStorageManaganer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IStorageManager, BlobStorageManager>();
            services.Configure<BlobStorageOptions>(configuration.GetSection("BlobStorage"));

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
