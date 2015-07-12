using HomeWork2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3.NotifyScheduleTaskAgent
{
    public class WeatherStats
    {
        public static readonly string WeatherSettingsKeyName = @"__WeatherSettings__";

        public SearchApiResultItem SelectedCity { get; set; }

        public bool AreNotificationsEnabled { get; set; }

        public WeatherStats()
        {
            AreNotificationsEnabled = true;
            SelectedCity = new SearchApiResultItem()
            {
                AreaName = "San Jose",
                Country = "Costa Rica",
                Latitude = 9.933,
                Longitude = -84.083,
                Region = "San Jose",
                WeatherUrl = 0.0,
            };
        }
    }
}
