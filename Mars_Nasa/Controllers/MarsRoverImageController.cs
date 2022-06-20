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
    public class MarsRoverImageController : Controller
    {
        HttpClient httpClient;

        static string BASE_URL = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?";
        static string API_KEY = "api_key=tJeW3Rn902fFaWsMj3Aid6x5W9vvsHlp6XsY7698";

        public ApplicationDbContext dbContext;

        public MarsRoverImageController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> rovers()
        {
            MarsRoverImage image = null;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            string dateStr = "&earth_date=2015-6-30";
            string url = BASE_URL + API_KEY + dateStr;
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
                    image = JsonConvert.DeserializeObject<MarsRoverImage>(apiStr);
                    dbContext.RoverImage.Add(image);
                    await dbContext.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }
            
            
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

              

        
    }
}




