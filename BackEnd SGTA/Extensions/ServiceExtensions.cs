using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BackEndSGTA.Services;
using BackEndSGTA.Data;
using FluentValidation;
using System.Text;
using System.Text.Json.Serialization;

namespace BackEndSGTA.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        var jwt = config.GetSection("Jwt").Get<Jwt>();

        if (jwt == null)
        {
            throw new InvalidOperationException("El objeto JWT no puede ser nulo.");
        }
        // DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(config.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 43))));

        // Controllers
        services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

        // FluentValidation
        services.AddFluentValidationAutoValidation()
               .AddFluentValidationClientsideAdapters();

        // Validators
        services.AddValidatorsFromAssemblyContaining<ClienteValidator>();

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
        });

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "SGTA API",
                Version = "v1",
                Description = "API para el sistema de gestión SGTA"
            });
        });

        services.AddScoped<TokenService>();


        // JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.Key!))
                };
            });

        services.AddAuthorization();
    }
}
