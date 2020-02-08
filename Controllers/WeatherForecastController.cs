using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace testing_seriloq_By_docs.Controllers
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

        [HttpGet]
        public IActionResult Get()
        {
            #region Standart logging

            _logger.LogWarning("Warning baby by User {UserName}", "zoom7oom@gmai.com");
            _logger.LogError("Warning baby");

            // So after this you can seach for User Registered in search bar
            _logger.LogInformation("User Registered: email: {username}", "shamilms@code.edu.az");


            #endregion

            #region Log a collection

            // we can log a collection

            _logger.LogError("User {email} registered a collection: {collection}",
                             "zoom7oom@gmail.com", new[] { "Creedence", "Clearwater" });

            #endregion

            #region Logging eventId and Exception

            var _event = new EventId(14, "User registered");
            _logger.LogError(_event, "SOme message {message}", "A big change is gonna come");

            // So this can be found in exceptions section
            _logger.LogError(new Exception("Db Exception"), "SOme message");

            #endregion

            #region Scope loggin


            using (var scope = _logger.BeginScope("Db Update Exception"))
            {
                _logger.LogInformation("Some Db Info ");
                _logger.LogWarning("Some Db Warning Info");
            }

            #endregion
            // or


            #region In this way user will be serialized

            var event1 = new EventId(14, "EF Error");
            var user = new UserRegistered();
            user.Name = "Steve";
            user.Lastname = "Corney";
            user.Email = "heart@of.gold";

            _logger.LogInformation(event1, "UserRegistered: {@User}", user);
            _logger.LogInformation(event1, "UserRegistered: {@User}", new { Name = "Steve", Age = 14});
            _logger.LogError(event1, "UserRegistered: {@User}", user);


            #endregion


            #region User in scope will be in log body

            using (var scope = _logger.BeginScope("UserRegistration: {@User}", user))
            {
                _logger.LogInformation("New user registered {Email}", user.Email);
            }

            #endregion

            return Redirect("http://localhost:5341");
        }


        public struct UserRegistered
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public int Age { get; set; }

           
        }
    }
}
