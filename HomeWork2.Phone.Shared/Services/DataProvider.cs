using HomeWork2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeWork2.Services
{
    public sealed class DataProvider
    {
        private static readonly string ApiKeyWorldWeatherOnlineCityExplorer = @"5jf3hyq3cjbhfv7vrcd22e84";
        private static readonly string ApiKeyFlickrCityExplorer = @"f92b46d9b4aeddd0a54470a16900f7e6";

        public async Task<IEnumerable<SearchApiResultItem>> GetSearchResults(string query)
        {

            var uri = new Uri(string.Format(@"http://api.worldweatheronline.com/free/v1/search.ashx?q={1}&format=xml&key={0}", ApiKeyWorldWeatherOnlineCityExplorer, query), UriKind.RelativeOrAbsolute);
            var contentStream = await ContentAccessors.Instance.GetContent(uri, KeepStoredPolicyAccessor.Instance);
            XDocument document;
            using (StreamReader reader = new StreamReader(contentStream))
            {
                document = XDocument.Load(reader);
            }
            var searchResultQuery = from resultElement in document.Root.Elements("result")
                                    select new SearchApiResultItem()
                                    {
                                        AreaName = (string)resultElement.Element("areaName"),
                                        Country = (string)resultElement.Element("country"),
                                        Region = (string)resultElement.Element("region"),
                                        Latitude = double.Parse(resultElement.Element("latitude").Value),
                                        Longitude = double.Parse(resultElement.Element("longitude").Value),
                                    };
            return searchResultQuery;
        }

        public async Task<Tuple<WeatherCurrentItem, IEnumerable<WeatheForecastItem>>> GetWeatherResults(double latitude, double longitude)
        {
            var uri = new Uri(string.Format(@"http://api.worldweatheronline.com/free/v1/weather.ashx?q={1}%2C{2}&format=xml&num_of_days=5&key={0}", ApiKeyWorldWeatherOnlineCityExplorer, latitude, longitude));
            LifeTimePolicyAccessor.Instance.SetTimeToLive(uri, TimeSpan.FromMinutes(30));
            var contentStream = await ContentAccessors.Instance.GetContent(uri, LifeTimePolicyAccessor.Instance);
            XDocument document;
            using (StreamReader reader = new StreamReader(contentStream))
            {
                document = XDocument.Load(reader);
            }
            var currentConditionElement = document.Root.Element("current_condition");
            WeatherCurrentItem current = new WeatherCurrentItem()
            {
                Cloudcover = (string)currentConditionElement.Element("cloudcover"),
                Humidity = (string)currentConditionElement.Element("humidity"),
                ObservationTime = (string)currentConditionElement.Element("observation_time"),
                PrecipMM = (string)currentConditionElement.Element("precipMM"),
                Pressure = (string)currentConditionElement.Element("pressure"),
                TempC = (string)currentConditionElement.Element("temp_C"),
                TempF = (string)currentConditionElement.Element("temp_F"),
                Visibility = (string)currentConditionElement.Element("temp_F"),
                WeatherCode = (string)currentConditionElement.Element("weatherCode"),
                WeatherDesc = (string)currentConditionElement.Element("weatherDesc"),
                WeatherIconUrl = (string)currentConditionElement.Element("weatherIconUrl"),
                Winddir16Point = (string)currentConditionElement.Element("winddir16Point"),
                WinddirDegree = (string)currentConditionElement.Element("winddirDegree"),
                WindspeedKmph = (string)currentConditionElement.Element("windspeedKmph"),
                WindspeedMiles = (string)currentConditionElement.Element("windspeedMiles"),
            };
            //IEnumerable<WeatheForecastItem> 
            var forecastQuery = from weatherElement in document.Root.Elements("weather")
                                select new WeatheForecastItem()
                                {
                                    Date = (string)weatherElement.Element("date"),
                                    PrecipMM = (string)weatherElement.Element("precipMM"),
                                    TempMaxC = (string)weatherElement.Element("tempMaxC"),
                                    TempMaxF = (string)weatherElement.Element("tempMaxF"),
                                    TempMinC = (string)weatherElement.Element("tempMinC"),
                                    TempMinF = (string)weatherElement.Element("tempMinF"),
                                    WeatherCode = (string)weatherElement.Element("weatherCode"),
                                    WeatherDesc = (string)weatherElement.Element("weatherDesc"),
                                    WeatherIconUrl = (string)weatherElement.Element("weatherIconUrl"),
                                    Winddir16Point = (string)weatherElement.Element("winddir16Point"),
                                    WinddirDegree = (string)weatherElement.Element("winddirDegree"),
                                    Winddirection = (string)weatherElement.Element("winddirection"),
                                    WindspeedKmph = (string)weatherElement.Element("windspeedKmph"),
                                    WindspeedMiles = (string)weatherElement.Element("windspeedMiles"),
                                };
            return new Tuple<WeatherCurrentItem, IEnumerable<WeatheForecastItem>>(current, forecastQuery);
        }

        public async Task<IEnumerable<PhotoItem>> GetPhotos(string queryTerm, Func<Stream, object> GetBitmapSource = null)
        {
            var uri = new Uri(string.Format(@"http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&format=rest", ApiKeyFlickrCityExplorer, queryTerm));
            LifeTimePolicyAccessor.Instance.SetTimeToLive(uri, TimeSpan.FromMinutes(30));
            var contentStream = await ContentAccessors.Instance.GetContent(uri, LifeTimePolicyAccessor.Instance);
            XDocument document;
            using (StreamReader reader = new StreamReader(contentStream))
            {
                document = XDocument.Load(reader);
            }
            List<PhotoItem> photoItems = new List<PhotoItem>();
            foreach (var photoElement in document.Root.Element("photos").Elements("photo"))
            {
                var farm = (string)photoElement.Attribute("farm");
                var server = (string)photoElement.Attribute("server");
                var id = (string)photoElement.Attribute("id");
                var secret = (string)photoElement.Attribute("secret");
                photoItems.Add(new PhotoItem()
                             {
                                 Id = id,
                                 Owner = (string)photoElement.Attribute("owner"),
                                 Secret = secret,
                                 Server = server,
                                 Farm = farm,
                                 Title = (string)photoElement.Attribute("title"),
                                 Ispublic = (string)photoElement.Attribute("ispublic"),
                                 Isfriend = (string)photoElement.Attribute("isfriend"),
                                 Isfamily = (string)photoElement.Attribute("isfamily"),
                                 SmallImage = await GetImage(farm, server, id, secret, GetBitmapSource),
                             });
            }

            return photoItems;
        }

        private async static Task<object> GetImage(string farm, string server, string id, string secret, Func<Stream, object> GetBitmapSource = null)
        {
            var url = string.Format(@"http://farm{0}.staticflickr.com/{1}/{2}_{3}_m.jpg",
                                                 farm,//farm-id
                                                 server,//server-id
                                                 id,//id
                                                 secret//secret
                                             );

            object bitmapSource = null;
            if (bitmapSource == null)
            {
                bitmapSource = url;
            }
            else
            {
                var contentStream = await ContentAccessors.Instance.GetContent(new Uri(url), KeepStoredPolicyAccessor.Instance);
                bitmapSource = GetBitmapSource(contentStream);
            }
            return bitmapSource;
        }

        public async Task<IEnumerable<NewsItem>> GetNews(string query)
        {
            var uri = new Uri(string.Format(@"http://api.feedzilla.com/v1/articles/search.rss?q={0}", query));
            LifeTimePolicyAccessor.Instance.SetTimeToLive(uri, TimeSpan.FromMinutes(30));
            var contentStream = await ContentAccessors.Instance.GetContent(uri, LifeTimePolicyAccessor.Instance);
            XDocument document;
            using (StreamReader reader = new StreamReader(contentStream))
            {
                document = XDocument.Load(reader);
            }
            var newsQuery = from photoElement in document.Root.Element("channel").Elements("item")
                            let description = (string)photoElement.Element("description")
                            let indexLtChar = description.IndexOf("<")
                            select new NewsItem()
                             {
                                 Description = description.Substring(0, indexLtChar < 0 ? description.Length : indexLtChar).Trim(),
                                 PubDate = (string)photoElement.Element("pubDate"),
                                 SourceLabel = (string)photoElement.Element("source"),
                                 SourceUrl = (string)photoElement.Element("source").Attribute("url"),
                                 Title = (string)photoElement.Element("title"),
                             };

            return newsQuery;
        }

        private void InitializeDataProvider()
        {

        }

        #region Singleton Pattern w/ Constructor
        private DataProvider()
            : base()
        {
            InitializeDataProvider();
        }
        public static DataProvider Instance
        {
            get
            {
                return SingletonDataProviderCreator._Instance;
            }
        }
        private class SingletonDataProviderCreator
        {
            private SingletonDataProviderCreator() { }
            public static DataProvider _Instance = new DataProvider();
        }
        #endregion

    }
}
