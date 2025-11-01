using DDDExample.Infrastructure.Persistence;
using DDDExample.Infrastructure.Repositories;
using DDDExample.Domain.Repositories;
using DDDExample.Application.Mappings;
using DDDExample.Application.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Leer configuración de MongoDB desde appsettings.json
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

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

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

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

// Map Controllers
app.MapControllers();

app.Run();
