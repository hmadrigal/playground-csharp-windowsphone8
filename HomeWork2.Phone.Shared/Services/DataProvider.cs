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

        public async Task<IEnumerable<SearchApiResultItem>> GetSearchResults(string query)
        {

            var uri = new Uri(string.Format(@"http://api.worldweatheronline.com/free/v1/search.ashx?q={1}&format=xml&key={0}", ApiKeyWorldWeatherOnlineCityExplorer, query), UriKind.RelativeOrAbsolute);
            var contentStream = await ContentAccessor.Instance.GetContent(uri);
            XDocument document;
            using (StreamReader reader = new StreamReader(contentStream))
            {
                document = XDocument.Load(reader);
            }
            var searchResultQuery = from resultElement in document.Root.Elements("result")
                                    select new SearchApiResultItem()
                                    {
                                        AreaName = resultElement.Element("areaName").Value,
                                        Country = resultElement.Element("country").Value,
                                        Region = resultElement.Element("region").Value,
                                        Latitude = double.Parse(resultElement.Element("latitude").Value),
                                        Longitude = double.Parse(resultElement.Element("longitude").Value),
                                    };
            return searchResultQuery;
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
