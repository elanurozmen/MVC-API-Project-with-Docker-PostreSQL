using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("PostgresConnection");


builder.Services.AddDbContext<CheckDbContext>(options =>
    options.UseNpgsql(conn ?? throw new InvalidOperationException("Connection string 'PostgresConnection' not found.")));

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();