using BudgetManager.Application.Services;
using BudgetManager.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace BudgetManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddBudgetManagerAuth(configuration)
            .AddApiServices()
            .AddMapping();

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Budget Manager API",
                Version = "v1",
                Description = "API documentation for Budget Manager"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' followed by your JWT token."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Budget Manager API v1");
                options.DefaultModelsExpandDepth(-1);
            });
        }

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

    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        return services;
    }
}