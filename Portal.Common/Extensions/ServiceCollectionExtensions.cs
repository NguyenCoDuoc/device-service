using Sun.Core.Logging.Extensions;
using DeviceService.Domain.Extensions;
using DeviceService.Application.Extensions;
using DeviceService.Application.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Sun.Core.DataAccess.Extensions;
using FluentValidation.AspNetCore;
using DeviceService.Application.DTOS.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using DeviceService.Common.Models;

namespace DeviceService.Common.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Khai báo các dịch vụ cơ bản cho 1 service mới
    /// - Tenant
    /// - KeycloakAuthentication
    /// - SunLogging
    /// - KafkaServices
    /// - Resilience
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
                        .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
                        .AddFluentValidation();

        services.AddEndpointsApiExplorer();
        // Đọc cấu hình Swagger từ appsettings.json
        var swaggerConfig = configuration.GetSection("Swagger").Get<SwaggerConfig>();
        if (swaggerConfig != null)
        {
            services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
            {
                Title = swaggerConfig.Title,
                Version = swaggerConfig.Version,
                Description = swaggerConfig.Description,
                TermsOfService = new Uri(swaggerConfig.TermsOfService),
                Contact = new OpenApiContact
                {
                    Name = swaggerConfig.Contact.Name,
                    Email = swaggerConfig.Contact.Email,
                    Url = new Uri(swaggerConfig.Contact.Url)
                }
            });

            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
        }
        services.AddAuthorization();
        services.AddHttpContextAccessor();
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddCors(options =>
        {
            options.AddPolicy("CorsApi", policy =>
            {
                policy
                    .AllowAnyOrigin() // Cho phép bất kỳ nguồn gốc nào. Nếu bạn muốn giới hạn, hãy thay thế bằng .WithOrigins("https://your-frontend-domain.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        services.Configure<JWTAudience>(configuration.GetSection("JWT"));
        services.AddSunLogging(configuration);
        services.AddDataAccess(configuration);
        // Adding Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}