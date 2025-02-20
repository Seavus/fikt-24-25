using BudgetManager.Application.Services;
using BudgetManager.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BudgetManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddBudgetManagerAuth(configuration)
            .AddApiServices();

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static IServiceCollection AddBudgetManagerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddTransient<ITokenService, TokenService>();

        var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();

        if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.Key) || string.IsNullOrEmpty(jwtOptions.Issuer))
        {
            throw new InvalidOperationException("JWT options are not configured properly.");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;  
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true, 
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = jwtOptions.Issuer,
                   ValidAudience = jwtOptions.Issuer,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
               };
           });

        return services;
    }
}