// Start/Stop device
// Get current operation status of a device
// Notification about errors

using Microsoft.OpenApi.Models;
using WashingMachineManagementApi.Api.Middlewares;
using WashingMachineManagementApi.Infrastructure;
using WashingMachineManagementApi.Application;
using WashingMachineManagementApi.Persistence;
using WashingMachineManagementApi.Infrastructure.Identity;
using WashingMachineManagementApi.Api.Services;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Api.Swashbuckle.OperationFilters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};


services.AddPersistence(configuration);
services.AddInfrastructure(configuration);
services.AddApplication(configuration);

services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

services.AddAuthorization();
services.AddControllers();

services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Washing Machine Management Automation API", Version = "v1" });
    options.OperationFilter<AcceptLanguageHeaderFilter>();
    options.EnableAnnotations();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "Json Web Token",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication With Json Web Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

services.AddTransient<GlobalExceptionHandlerMiddleware>();
services.AddScoped<ISessionUserService, SessionUserService>();


var app = builder.Build();


app.UseCors("AllowAnyOrigin");

app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

var scope = app.Services.CreateScope();
IdentitySeeder.SeedIdentity(scope);

app.Run();
