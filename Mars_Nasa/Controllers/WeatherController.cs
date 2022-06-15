using Microsoft.AspNetCore.Mvc;

namespace MarsNasa.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Weather()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ImageOfDay()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }
    }
}
