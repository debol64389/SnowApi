using DanskeBank.Security.Jwt.Provider;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using SnowApi.Core.AppSettings;
using SnowApi.Infrastructure;
using SnowApi.Infrastructure.Interfaces;
using SnowApi.Services;
using SnowApi.Services.Interfaces;

namespace SnowApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        RegisterAuthorization(services, _configuration);

        services.AddHttpContextAccessor();
        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SnowApi - REST API",
                Version = "v1",
                Description = "Managing message templates and customers"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the JWT authorization token. It can be acquired from (ADFS access_token): </br></br> &nbsp;&nbsp; https://danske-token.danskenet.net/Home/Tokens </br></br> OR using API (access_token from /api/Token/GetToken): </br></br> &nbsp;&nbsp; https://danske-token.danskenet.net/swagger/index.html </br></br>",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddHealthChecks();
        services.AddScoped<IRepositorySql, RepositorySql>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IMessageTemplatesService, MessageTemplatesService>();
        services.Configure<ConnectionStrings>(options => _configuration.GetSection("ConnectionStrings").Bind(options));
        services.AddScoped<ICustomerUniqueIdFactory, CustomerUniqueIdFactory>();
        services.AddScoped<ICustomerValidationService, CustomerValidationService>();
        services.AddScoped<IMessageTemplatesValidationService, MessageTemplatesValidationService>();
        services.AddScoped<ICommunicationService, CommunicationService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        // Specifying the Swagger JSON endpoint.
        app.UseSwagger().UseSwaggerUI();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapControllers();
            _ = endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
        });
    }

    private static void RegisterAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        var whiteList = new DanskeBank.Security.Jwt.Provider.Models.UriList();
        configuration.GetSection("WhiteList").Bind(whiteList);

        services.AddStandardJwtAuthentication(whiteList.Urls.ConvertAll(uri => uri.AbsoluteUri));
    }
}