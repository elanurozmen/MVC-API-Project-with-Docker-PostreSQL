using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using MyMvcApp.Models;
using System.Diagnostics;

namespace MyMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private PostgresDbService _postgresDbService;
     private readonly HttpClient _httpClient;

        public HomeController(PostgresDbService postgresDbService, HttpClient httpClient)
        {
            _postgresDbService = postgresDbService;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
{
    string apiUrl = "http://localhost:5257/weatherforecast";
    var model = new WeatherForecastViewModel();

    
    try
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<List<WeatherForecast>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            if (weatherData != null)
            {
                foreach (var weather in weatherData)
                {
                    if (DateTime.TryParse(weather.Date, out var parsedDate))
                    {
                        await _postgresDbService.SaveWeatherDataAsync(
                            parsedDate.ToString("yyyy-MM-dd"), // Formatlanmış tarih
                            weather.TemperatureC, 
                            weather.TemperatureF, 
                            weather.Summary ?? "N/A"
                        );
                    }
                    else
                    {
                        model.ErrorMessage = "Geçersiz tarih formatı: " + weather.Date;
                    }
                }

                model.Forecasts = weatherData;
                return View(model);
            }
            else
            {
                model.ErrorMessage = "Veri bulunamadı.";
                return View(model);
            }
        }
        else
        {
            // ErrorViewModel ile hata mesajı gönderiliyor
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = $"Servis yanıt vermiyor. Durum Kodu: {response.StatusCode}" });
        }
    }
    catch (HttpRequestException ex)
    {
        // Hata mesajını ErrorViewModel'e ekleyip görünümü gönderiyoruz
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = $"API çağrısı sırasında bir hata oluştu: {ex.Message}" });
    }
    catch (Exception ex)
    {
        // Hata mesajını ErrorViewModel'e ekleyip görünümü gönderiyoruz
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = $"Bilinmeyen bir hata oluştu: {ex.Message}" });
    }
}

    }
}

