using BudgetManager.Application.Services;
using BudgetManager.Application.Data;
using BudgetManager.Infrastructure.Services;
using BudgetManager.Infrastructure.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Reflection;
using BudgetManager.Infrastructure.Data.Extensions;
using BudgetManager.Application.Users.RegisterUser;
using BudgetManager.Application.Exceptions.Handler;
using BudgetManager.Infrastructure.Middlewares;
using BudgetManager.Application.Users.LoginUser;
using BudgetManager.Application.Users.UpdateUser;
using BudgetManager.Application.Users.GetUsers;
using BudgetManager.Application.Transactions.CreateTransaction;
using BudgetManager.Application.Users.GetUserById;
using BudgetManager.Domain.Models;
using BudgetManager.Infrastructure.Data.Interceptors;
using MediatR;
using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Application.Categories.CreateCategory;
using BudgetManager.Application.Categories.UpdateCategory;
using BudgetManager.Application.Categories.GetCatogiresByUser;
using BudgetManager.Application.Categories.DeleteCategory;

namespace BudgetManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddBudgetManagerAuth(configuration)
            .AddApiServices()
            .AddMapping()
            .AddSmtpMail();

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddExceptionHandler<CustomExceptionHandler>();

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

            var xmlFile = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
            var xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) ?? string.Empty, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        services.AddCurrentUser();

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

        app.UseCurrentUser();

        app.MapControllers();

        app.UseExceptionHandler(options => { });

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
        services.AddAutoMapper(cfg =>
        {
            cfg.CreateMap<LoginUserRequest, LoginUserQuery>();
            cfg.CreateMap<RegisterUserRequest, RegisterUserCommand>();
            cfg.CreateMap<UpdateUserRequest, UpdateUserCommand>();
            cfg.CreateMap<GetUsersRequest, GetUsersQuery>();
            cfg.CreateMap<CreateTransactionRequest, CreateTransactionCommand>();
            cfg.CreateMap<User, GetUserByIdResponse>();
            cfg.CreateMap<User, GetUsersResponse>();
            cfg.CreateMap<UserId, Guid>().ConvertUsing(src => src.Value);
            cfg.CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
            cfg.CreateMap<CategoryId, Guid>().ConvertUsing(src => src.Value);
            cfg.CreateMap<Category, GetCategoriesByUserResponse>();
            cfg.CreateMap<GetCategoriesRequest, GetCategoriesByUserQuery>();
            cfg.CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
            cfg.CreateMap<DeleteCategoryRequest, DeleteCategoryCommand>();
            cfg.CreateMap<TransactionId, Guid>().ConvertUsing(src => src.Value);
            }, typeof(DependencyInjection).Assembly);

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .AddInterceptors(
                new DispatchDomainEventsInterceptor(serviceProvider.GetRequiredService<IMediator>()),
                new AuditableEntityInterceptor(serviceProvider.GetRequiredService<ICurrentUser>()));
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
        await DatabaseExtensions.SeedAsync(context);
    }

    private static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CurrentUserMiddleware>();
    }

    private static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<CurrentUserMiddleware>();
        return services;
    }

    private static IServiceCollection AddSmtpMail(this IServiceCollection services)
    {
        services.AddFluentEmail("no-reply@budgetmanager.com")
                .AddSmtpSender("localhost", 25);

        return services;
    }
}