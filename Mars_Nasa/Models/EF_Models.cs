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
}