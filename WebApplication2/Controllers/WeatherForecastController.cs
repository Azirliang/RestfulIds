using Microsoft.AspNetCore.Mvc;
using static WebApplication2.CommaDelimeterArrayModelBinder;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpGet("{ids:regex(^(\\w{{8}}(-\\w{{4}}){{3}}-\\w{{12}},)+(\\w{{8}}(-\\w{{4}}){{3}}-\\w{{12}},*)*$)}")]
        //[HttpGet("{ids:regex(^(\\d{ { 1,19} },)+(\\d{{1,19}},*)*$)}")]
        //[HttpGet("{ids:regex(^(\\w{{36}},)+(\\w{{36}},*)*$)}")]
        //[HttpGet("{ids:regex(^(\\w{{36}},)+(\\w{{36}},)*$)}")]
        [HttpGet("{ids:regex(^(\\w{{8}}(-\\w{{4}}){{3}}-\\w{{12}},*)*$)}")]
        public IEnumerable<WeatherForecast> Get([IdsBinder] Guid[] ids)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}