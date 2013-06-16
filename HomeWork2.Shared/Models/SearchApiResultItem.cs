﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2.Models
{
    public class SearchApiResultItem
    {
        public string AreaName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double WeatherUrl { get; set; }
    }
}
