using Microsoft.AspNetCore.Mvc;

namespace BigBrother.Controllers
{
    [ApiController]
    public class BigBroController : Controller
    {
        [HttpGet]
        [Route("bb/users")]
        public IActionResult Get()
        {
            Models.SoftwareUsageContext context = new Models.SoftwareUsageContext();

            var logins = from x in context.SoftwareUsages
                         select x.Login;

            return Ok(logins.Distinct());
        }

        [HttpGet]
        [Route("bb/users/{login}")]
        public IActionResult Get(string login)
        {
            Models.SoftwareUsageContext context = new Models.SoftwareUsageContext();

            var applicationData = from x in context.SoftwareUsages
                                  where x.Login == login
                                  select x.Login;

            return Ok(applicationData.Distinct().ToList());
        }

        [HttpGet]
        [Route("bb/totalappusage")]
        public IActionResult Get3()
        {
            Models.SoftwareUsageContext context = new Models.SoftwareUsageContext();

            var applicationData = from x in context.SoftwareUsages
                                  select x;

            var appTimes = from x in applicationData
                           group x by new { x.ApplicationName } into g
                           select new AppTime()
                           {
                               Name = g.Key.ApplicationName,
                               Value = (from x in g select x.Time).Sum()
                           };

            return Ok(appTimes);
        }

        [HttpGet]
        [Route("bb/users/{login}/appusage")]
        public IActionResult Get2(string login)
        {
            Models.SoftwareUsageContext context = new Models.SoftwareUsageContext();

            var applicationData = from x in context.SoftwareUsages
                                  where x.Login == login
                                  select x;

            var appTimes = from x in applicationData
                           group x by new { x.ApplicationName } into g
                           select new AppTime()
                           {
                               Name = g.Key.ApplicationName,
                               Value = (from x in g select x.Time).Sum()
                           };

            return Ok(appTimes);
        }
    }

    public class AppTime
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
