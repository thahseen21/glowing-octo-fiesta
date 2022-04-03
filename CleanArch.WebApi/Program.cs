using CleanArch.Application;
using CleanArch.Domain.Common.Configuration;
using CleanArch.Infrastructure;
using CleanArch.WebApi.Filters;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var appSettings = configuration.Get<AppSettings>();
appSettings.Validate();


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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
