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
        HttpClient httpClient;

        static string BASE_URL = "https://api.nasa.gov/planetary/apod?api_key=";
        static string API_KEY = "ldD32JgpHGnzPJyxRttoohWCJw7xOM4nNMBK3A0Q";

        public ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string url = BASE_URL + API_KEY;
            string apiStr = "";

            ImageOfTheDay image = null;
            httpClient.BaseAddress = new Uri(url);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    apiStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!apiStr.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    image = JsonConvert.DeserializeObject<ImageOfTheDay>(apiStr);
                    image.id = Guid.NewGuid();
                    dbContext.Images.Add(image);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return View(image);
        }
    }
}

