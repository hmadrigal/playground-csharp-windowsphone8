using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork2.Models
{
    public class AreaName
    {
        public string value { get; set; }
    }

    public class Country
    {
        public string value { get; set; }
    }

    public class Region
    {
        public string value { get; set; }
    }

    public class WeatherUrl
    {
        public string value { get; set; }
    }

    public class Result
    {
        public List<AreaName> areaName { get; set; }
        public List<Country> country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string population { get; set; }
        public List<Region> region { get; set; }
        public List<WeatherUrl> weatherUrl { get; set; }
    }

    public class SearchApi
    {
        public List<Result> result { get; set; }
    }

    public class SearchApiResult
    {
        public SearchApi search_api { get; set; }
    }

}
