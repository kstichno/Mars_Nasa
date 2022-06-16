using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsNasa.Models
{
    public class ImageOfTheDay
    {
        public string copyright { get; set; }
        public string date { get; set; }
        public string explanation { get; set; }

        [Key]
        public string hdurl { get; set; }
        public string media_type { get; set; }
        public string service_version { get; set; }
        public string title { get; set; }
        public string url { get; set; }

        [NotMapped]
        public List<SelectedListItem> Titles { get; set; }
    }

    public class SelectedListItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Weather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public int ls { get; set; }
        public string season { get; set; }
        public int min_temp { get; set; }
        public int max_temp { get; set; }
        public int pressure { get; set; }
        public string pressure_string { get; set; }
        public string atmo_opacity { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string local_uv_irradiance_index { get; set; }
        public int min_gts_temp { get; set; }
        public int max_gts_temp { get; set; }
        public int sol { get; set; }
        public string unitOfMeasure { get; set; }

        [NotMapped]
        public List<WeatherInfo> W { get; set; }
    }

    public class WeatherInfo
    {
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Season { get; set; }
        public int Sol { get; set; }
    }

    public class ChartModel
    {
        public string ChartType { get; set; }
        public string XLabels { get; set; }
        public string Colors { get; set; }
        public string DataMin { get; set; }
        public string DataMax { get; set; }
        public string Title { get; set; }
    }
}