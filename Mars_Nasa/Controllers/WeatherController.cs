using MarsNasa.Models;
using MarsNasa.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace MarsNasa.Controllers
{
    public class WeatherController : Controller
    {
        HttpClient httpClient;

        static string BASE_URL = "https://api.maas2.apollorion.com/";
        static string[] days = { "1700", "1760", "1820", "1880", "1940", "2000", "2050", "2110", "2170", "2230", "2290", "2350" };

        public ApplicationDbContext dbContext;

        public WeatherController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Weather()
        {
            Weather weather = null;
            
            for (int i = 0; i < days.Length; i++)
            {
                string url = BASE_URL + days[i];
                string apiStr = "";

                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
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
                        //JsonConvert is part of the NewtonSoft.Json Nuget package
                        weather = JsonConvert.DeserializeObject<Weather>(apiStr);
                        dbContext.Weather.Add(weather);
                        await dbContext.SaveChangesAsync();
                    }

                }
                catch (Exception e)
                {
                    // This is a useful place to insert a breakpoint and observe the error message
                    Console.WriteLine(e.Message);
                }
            }
            weather.W = (from s in dbContext.Weather.AsEnumerable()
                                select new WeatherInfo
                                {
                                    MaxTemp = s.max_temp,
                                    MinTemp = s.min_temp,
                                    Sunrise = s.sunrise,
                                    Sunset = s.sunset,
                                    UnitOfMeasure = s.unitOfMeasure,
                                    Season = s.season,
                                    Sol = s.sol
                                    
                                }).ToList();


            int[] MaxTemps = new int[12];
            int[] MinTemps = new int[12];
            string[] Labels = { "mo1", "mo2", "mo3", "mo4", "mo5", "mo6", "mo7", "mo8", "mo9", "mo10", "mo11", "mo12" };

            for (int i = 0; i<weather.W.Count; i++)
            {
                MaxTemps[i] = weather.W[i].MaxTemp;
                MinTemps[i] = weather.W[i].MinTemp;
            }

            ChartModel Model = new ChartModel
            {
                ChartType = "line",
                XLabels = String.Join(",", Labels.Select(d => "'" + d + "'")),
                DataMax = String.Join(",", MaxTemps.Select(d => d)),
                DataMin = String.Join(",", MinTemps.Select(d => d)),
                Title = "Mars (Gale Crater) Yearly Temperature Cycle"
            };


            return View(Model);
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

