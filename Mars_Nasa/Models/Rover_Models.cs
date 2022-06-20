using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsNasa.Models
{

    public class MarsRoverImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int marsroverimageid { get; set; }
        public List<Photo> photos { get; set; }

  
    }
    
    public class Photo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public int sol { get; set; }
        public MarsCamera marscamera { get; set; }
        [Key] 
        public string img_src { get; set; }
        public string earth_date { get; set; }
        
    }

    public class MarsCamera
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int marscameraid { get; set; }
        public string full_name { get; set; }
    }



}