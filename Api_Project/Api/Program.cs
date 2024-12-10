using Microsoft.EntityFrameworkCore;
using Api_Project.Models;
using Api_Project.Services;
var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.Configure<CheckDatabaseSettings>(
    builder.Configuration.GetSection("CheckDb"));
    
builder.Services.AddSingleton<CheckService>();

builder.Services.AddDbContext<CheckDbContext>(options =>
    options.UseNpgsql(conn ?? throw new InvalidOperationException("Connection string 'PostgresConnection' not found.")));

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

    app.UseSwagge();
    app.UseSwaggerU();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();