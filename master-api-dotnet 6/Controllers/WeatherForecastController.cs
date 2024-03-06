using master_api_dotnet_6.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace master_api_dotnet_6.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppDbContext _context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
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

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            var user = new TblUser
            {
                ConfirmPassword="",
                Email="",
                IsActive=true,
                IsDelete=false,
                LastActionDatetime=DateTime.UtcNow,
                Mobile="",
                Password="",
                PreviousPassword="",
                UserName="Akash"
            };
            _context.TblUsers.Add(user);
            await _context.SaveChangesAsync();
            return Ok(200);
        }
    }
}