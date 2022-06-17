using MarsNasa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MarsNasa.DataAccess;
using System.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MarsNasa.Controllers
{
    public class ImageOfDayController : Controller
    {
        HttpClient httpClient;

        static string BASE_URL = "https://api.nasa.gov/planetary/apod?";
        static string API_KEY = "&api_key=ldD32JgpHGnzPJyxRttoohWCJw7xOM4nNMBK3A0Q";

        public ApplicationDbContext dbContext;

        public ImageOfDayController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> APOD()
        {
            ImageOfTheDay image = null;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = BASE_URL + API_KEY;
            string apiStr = "";

            httpClient.BaseAddress = new Uri(url);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    apiStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!apiStr.Equals(string.Empty))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    image = JsonConvert.DeserializeObject<ImageOfTheDay>(apiStr);
                    dbContext.Image.Add(image);
                    await dbContext.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }
            GetTitles(image, dbContext);
            return View(image);
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Weather()
        {
            return View();
        }

        [HttpPost]
        public ActionResult APOD(IFormCollection formcollection)
        {
            ImageOfTheDay image = dbContext.Image
                                    .Where(i => i.title == formcollection["Name"].ToString())
                                    .First();
            GetTitles(image, dbContext);

            return View(image);
        }

        private static void GetTitles(ImageOfTheDay image, ApplicationDbContext dbContext)
        {
            image.Titles = (from p in dbContext.Image.AsEnumerable()
                            select new SelectedListItem
                            {
                                Name = p.title,
                                Id = p.hdurl
                            }).ToList();
        }
    }
}


