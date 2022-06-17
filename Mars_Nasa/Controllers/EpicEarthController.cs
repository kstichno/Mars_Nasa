using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsNasa.Controllers
{
    public class EpicEarthController : Controller
    {
        public IActionResult EpicEarth()
        {
            return View();
        }
    }
}
