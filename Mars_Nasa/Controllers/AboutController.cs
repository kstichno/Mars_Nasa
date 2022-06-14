using Microsoft.AspNetCore.Mvc;

namespace MarsNasa.Controllers
{
    public class AboutController : Controller
    {
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
