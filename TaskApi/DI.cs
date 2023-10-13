using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using TaskApi.Services.Interfaces;
using TaskApi.Services;
using Microsoft.OpenApi.Models;

namespace TaskApi
{
    public static class DI
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "My Api - V1",
                        Version = "v1",
                    }
                );

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Jwt Authorization header using the Bearer scheme/ \r\r\r\n Enter 'Bearer' [space] and then token in the text input below. \r\n\r\n Example : \"Bearer f3c04qc08mh3n878\""
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            return services;
        }

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
