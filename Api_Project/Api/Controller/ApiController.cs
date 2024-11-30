using Microsoft.AspNetCore.Mvc;
using Api_Project.Models;

namespace Api_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherDbContext _context;

        public WeatherController(WeatherDbContext context)
        {
            _context = context;
        }

        // POST: api/Weather
        [HttpPost]
        public async Task<IActionResult> CreateWeatherForecast([FromBody] WeatherForecast weatherForecast)
        {
            if (weatherForecast == null)
            {
                return BadRequest("Weather forecast data is invalid.");
            }

            await _context.WeatherForecasts.AddAsync(weatherForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWeatherForecastById), new { id = weatherForecast.Id }, weatherForecast);
        }

        // GET: api/Weather/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeatherForecastById(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return Ok(weatherForecast);
        }
    }
}
