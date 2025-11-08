using DDDExample.Infrastructure.Persistence;
using DDDExample.Infrastructure.Repositories;
using DDDExample.Domain.Repositories;
using DDDExample.Application.Mappings;
using DDDExample.Application.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;

using DDDExample.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DDDExample.Application.Settings;
using DDDExample.API.Middleware; // <- Asegúrate de tener este using

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Leer configuración de MongoDB desde appsettings.json
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<MemoryMetricsSettings>(builder.Configuration.GetSection("MemoryMetrics"));
builder.Services.AddScoped<IMemoryMetricsRepository, MemoryMetricsRepository>();
builder.Services.AddHostedService<MemoryMetricsService>();

builder.Services.AddScoped<IResponseTimeLogRepository, ResponseTimeLogRepository>();





// Registrar cliente MongoDB
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Registrar base de datos
builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

// AutoMapper (forma actual)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registrar repositorios y servicios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTP → HTTPS
app.UseHttpsRedirection();
app.UseResponseTimeLog();
// Map Controllers
app.MapControllers();

app.Run();
