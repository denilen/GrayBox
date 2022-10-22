using App.Metrics;
using App.Metrics.Counter;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Dto;

namespace Prometheus.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMetrics _metrics;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                     CounterOptions counter,
                                     MetricsBuilder metricsBuilder, IMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecastDto> Get()
    {
        _metrics.Measure.Counter.Increment(MetricsRegistry.SampleCounter);
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
                         {
                             Date = DateTime.Now.AddDays(index),
                             TemperatureC = Random.Shared.Next(-20, 55),
                             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                         })
                         .ToArray();
    }
}