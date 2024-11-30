using Microsoft.EntityFrameworkCore;
using Api_Project.Models; // Kendi namespace'inizi ekleyin

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

// Veritabanı bağlantısını ayarlama
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Swagger'ı etkinleştirme
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger'ı yalnızca geliştirme ortamında kullan
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// API uç noktalarını tanımlama
app.MapGet("/weatherforecast", async (WeatherDbContext dbContext) =>
{
    var forecast = await dbContext.WeatherForecasts.ToListAsync();
    return forecast;
}).WithName("GetWeatherForecast")
  .WithOpenApi();

app.MapPost("/weatherforecast", async (WeatherDbContext dbContext, WeatherForecast weather) =>
{
    dbContext.WeatherForecasts.Add(weather);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/weatherforecast/{weather.Date}", weather);
}).WithName("AddWeatherForecast");

app.Run("http://0.0.0.0:80");
