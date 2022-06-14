using MarsNasa.Models;
using Microsoft.AspNetCore.Mvc;
using MarsNasa.DataAccess;
using System.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarsNasa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImageOfDay()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}

