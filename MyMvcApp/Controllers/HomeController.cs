Home controller:
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyMvcApp.Controllers
{
    public class HomeController : Controller
    {
        // private readonly PostgresDbService _postgresDbService;
        //  public HomeController(IConfiguration configuration)
        // {
        //     // Bağlantı dizelerini al
        //     var postgresConnection = configuration.GetConnectionString("PostgresConnection");

        //     // Servisleri başlat
        //     _postgresDbService = new PostgresDbService(postgresConnection);

        // }
       // Weather Forecast modelini tanımlıyoruz.
        public class WeatherForecast
        {
            public string Date { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }
            public int TemperatureF { get; set; }
        }

      // GET: /Home/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // API URL'si
            string apiUrl = "http://localhost:5257/weatherforecast";

            try
            {
                // HttpClient ile API isteği gönder
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl);

                    // Yanıtı kontrol et
                    if (response.IsSuccessStatusCode)
                    {
                        // JSON yanıtını string olarak al
                        var json = await response.Content.ReadAsStringAsync();

                        // JSON'u WeatherForecast listesine dönüştür
                        var weatherData = JsonSerializer.Deserialize<List<WeatherForecast>>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // JSON'daki büyük/küçük harf farkını göz ardı et
                        });

                        // if (weatherData != null)
                        // {
                        //     foreach (var weather in weatherData)
                        //     {
                        //         // İki veritabanına aynı anda yaz
                        //         await _postgresDbService.SaveWeatherDataAsync(weather.Date, weather.TemperatureC, weather.Summary);
                                
                        //     }
                        // }
                        //Veriyi View'e gönder
                        return View(weatherData);
                    }
                    else
                    { 
                        // Başarısız yanıt durumu için hata mesajı
                        ViewBag.ErrorMessage = $"Servis yanıt vermiyor. Durum Kodu: {response.StatusCode}";
                        return View("Error");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // HTTP isteği sırasında oluşan hata
                ViewBag.ErrorMessage = $"API çağrısı sırasında bir hata oluştu: {ex.Message}";
                return View("Error");
            }
            catch (Exception ex)
            {
                // Genel hata durumu
                ViewBag.ErrorMessage = $"Bilinmeyen bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }
    }
}