using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api_Project.Models
{
    public class WeatherDbContext : DbContext
    {
        
        public required DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            : base(options)
        {
        }
    }
}
