using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BackEndSGTA.Services;
using BackEndSGTA.Data;
using FluentValidation;
using System.Text.Json;
using System.Text;
using Scrutor;

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

        // MongoDbContext
        services.AddSingleton<MongoDbContext>();

        // Controllers
        services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    });

        // FluentValidation
        services.AddFluentValidationAutoValidation()
               .AddFluentValidationClientsideAdapters();

        // Validators
        services.AddValidatorsFromAssemblyContaining<ClienteValidator>();

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("DevCors", policy =>
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
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

        // Al final de ConfigureServices, antes de AddAuthentication
        services.Scan(scan => scan
            .FromAssemblyOf<PresupuestoService>()   // Escanea el ensamblado donde están tus servicios
            .AddClasses(classes => classes.InNamespaces("BackEndSGTA.Services")) // solo tus servicios
            .AsSelfWithInterfaces()                  // Se registra como su propia clase y sus interfaces
            .WithScopedLifetime());                  // Lifetime Scoped

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

        // HttpClient para llamadas externas
        services.AddHttpClient();
    }
}
