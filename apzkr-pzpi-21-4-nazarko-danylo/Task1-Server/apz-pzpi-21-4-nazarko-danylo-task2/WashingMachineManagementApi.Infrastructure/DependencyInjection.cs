using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Identity.Mongo;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WashingMachineManagementApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity(configuration)
            .AddAuthenticationWithJwt(configuration)
            .AddServices();

        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        AddMongoDbIdentity();

        return services;

        void AddMongoDbIdentity()
        {
            var connectionString = $"mongodb://mongodb.cuqmbr.home:27017/identity";

            services.AddIdentityMongoDbProvider<
                Identity.MongoDb.Models.ApplicationUser<string>,
                Identity.MongoDb.Models.ApplicationRole<string>, 
                string>(
                    identity =>
                    {
                    },
                    mongo =>
                    {
                        mongo.ConnectionString = connectionString;
                    });
        }

        void AddPostgreSQLIdentity()
        {
            // TODO: Add PostgreSQL Identity connector
        }
    }

    private static IServiceCollection AddAuthenticationWithJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["Infrastructure:Authentication:JsonWebToken:Audience"],
                    ValidIssuer = configuration["Infrastructure:Authentication:JsonWebToken:Issuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Infrastructure:Authentication:JsonWebToken:IssuerSigningKey"]))
                };
            });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
