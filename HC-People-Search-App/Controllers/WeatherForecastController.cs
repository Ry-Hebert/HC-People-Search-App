using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HC_People_Search_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] fName = new[]
        {
            "Jim", "Sarah", "Alex", "Amy", "Tim", "Lex", "Amos", "Lee", "Taylor", "Reagan", "Cameron", "James", "Zack", "Zach", "Alex", "Harmony", "Eliot", "Ellen", "Aphrodite", "Cleo", "Bill", "Ted", "Marcus", "Noah", "Weston", "Ben", "Katie", "Harlly", "Marly", "Susanna", "Maria", "Stanley", "Han", "Luke", "Landon", "Thor"
        };
        private static readonly string[] lName = new[]
        {
            "Smith", "Moa", "McCormick", "Caballero", "Faulkner", "Kamiesoko", "Leland", "Gosset", "Holt", "Hudgins", "Evans", "Brown", "Rawlins", "Oneill", "Oneil", "Carter", "Jackson", "Willson", "Hammond", "Ragnarson", "Erickson", "Washington", "Harper", "Fallow", "McCloud", "McCleod", "Lee", "Leeland", "Carlson", "Annson", "Clark", "Shalows", "Winchester", "Adams", "Schumester", "Kelly", "Yelnats", "Solo", "Skywalker", "Thorson", "Willcaster"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = fName[rng.Next(fName.Length)] + " " + lName[rng.Next(lName.Length)],
                TemperatureC = rng.Next(1, 99999),
                Summary = fName[rng.Next(fName.Length)]
            })
            .ToArray();
        }
    }
}
