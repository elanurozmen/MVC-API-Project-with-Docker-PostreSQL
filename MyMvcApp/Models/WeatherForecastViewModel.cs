using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyMvcApp.Models
{
    public class WeatherForecast
    {
     
        public string? Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string? Summary { get; set; }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<WeatherForecast>().HasNoKey();
}


    public class WeatherForecastViewModel
    {
        public List<WeatherForecast> Forecasts { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
