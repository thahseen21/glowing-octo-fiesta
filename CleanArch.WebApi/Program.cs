using CleanArch.Application;
using CleanArch.Domain.Common.Configuration;
using CleanArch.Infrastructure;

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
