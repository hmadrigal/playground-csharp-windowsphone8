using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork2.Models
{
    public class WeatheForecastItem
    {
        public string Date { get; set; }
        public string TempMaxC { get; set; }
        public string TempMaxF { get; set; }
        public string TempMinC { get; set; }
        public string TempMinF { get; set; }
        public string WindspeedMiles { get; set; }
        public string WindspeedKmph { get; set; }
        public string Winddirection { get; set; }
        public string Winddir16Point { get; set; }
        public string WinddirDegree { get; set; }
        public string WeatherCode { get; set; }
        public string WeatherIconUrl { get; set; }
        public string WeatherDesc { get; set; }
        public string PrecipMM { get; set; }
    }

    public class WeatherCurrentItem
    {
        public string ObservationTime { get; set; }
        public string TempC { get; set; }
        public string TempF { get; set; }
        public string WeatherCode { get; set; }
        public string WeatherIconUrl { get; set; }
        public string WeatherDesc { get; set; }
        public string WindspeedMiles { get; set; }
        public string WindspeedKmph { get; set; }
        public string WinddirDegree { get; set; }
        public string Winddir16Point { get; set; }
        public string PrecipMM { get; set; }
        public string Humidity { get; set; }
        public string Visibility { get; set; }
        public string Pressure { get; set; }
        public string Cloudcover { get; set; }
    }
}
