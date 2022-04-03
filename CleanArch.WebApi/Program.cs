using CleanArch.Application;
using CleanArch.Domain.Common.Configuration;
using CleanArch.Infrastructure;
using CleanArch.WebApi.Filters;
using CleanArch.WebApi.Middleware;
using CleanArch.WebApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var appSettings = configuration.Get<AppSettings>();
appSettings.Validate();

builder.Services.AddSingleton<IAppSettings>(appSettings);

#region Application
builder.Services.AddApplication();
#endregion

#region Infrasturcture
builder.Services.AddInfrastructure(appSettings);
#endregion

#region Filters
builder.Services.AddControllers()
    .AddFluentValidation()
    .AddMvcOptions(
        o =>
        {
            // When using FluentValidation, clear the default model validations
            o.ModelValidatorProviders.Clear();
            o.ModelMetadataDetailsProviders.Clear();
            o.Filters.Add(new ApiExceptionFilterAttribute());
        }).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
#endregion

#region Jwt Service
builder.Services.AddScoped<IJwtService, JwtService>();
#endregion

#region Token Validator 
var jwtKey = configuration.GetValue<string>("JwtSettings:Key");
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

TokenValidationParameters tokenValidation = new TokenValidationParameters
{
    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
    ValidateLifetime = true,
    ValidateAudience = false,
    ValidateIssuer = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddSingleton(tokenValidation);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = tokenValidation;
});
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region OpenApi
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArch", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "This site uses bearer token and you have to pass " + "it as Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "oauth2",
            Name = JwtBearerDefaults.AuthenticationScheme,
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header
        },
    new List<string>()
    }
    });
});
#endregion 

#region Cors Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(configuration.GetRequiredSection("AllowedHosts").Value,
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Middleware 
app.UseMiddleware<ErrorHandlingMiddleware>();
#endregion

app.UseHttpsRedirection();

app.UseCors(configuration.GetRequiredSection("AllowedHosts").Value);

#region Authorization & Authentication
app.UseAuthentication();
app.UseAuthorization();
#endregion

app.MapControllers();

app.Run();
