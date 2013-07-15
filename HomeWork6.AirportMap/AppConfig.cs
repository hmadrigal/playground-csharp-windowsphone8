using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.AirportMap
{
    public sealed class AppConfig
    {
        public string Country
        {
            get
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("_country_"))
                {
                    IsolatedStorageSettings.ApplicationSettings["_country_"] = "Costa Rica";
                    //IsolatedStorageSettings.ApplicationSettings.Save();
                }
                return IsolatedStorageSettings.ApplicationSettings["_country_"].ToString();
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["_country_"] = value;
                //IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public string CountryFlagUrl
        {
            get
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("_countryflagurl_"))
                {
                    IsolatedStorageSettings.ApplicationSettings["_countryflagurl_"] = @"http://www.oorsprong.org/WebSamples.CountryInfo/Images/Costa_Rica.jpg";
                    //IsolatedStorageSettings.ApplicationSettings.Save();
                }
                return IsolatedStorageSettings.ApplicationSettings["_countryflagurl_"].ToString();
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["_countryflagurl_"] = value;
                //IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public string CountryListDocument
        {
            get
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("_countrylistdocument_"))
                {
                    IsolatedStorageSettings.ApplicationSettings["_countrylistdocument_"] = string.Empty;
                    //IsolatedStorageSettings.ApplicationSettings.Save();
                }
                return IsolatedStorageSettings.ApplicationSettings["_countrylistdocument_"].ToString();
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["_countrylistdocument_"] = value;
                //IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public Microsoft.Phone.Maps.Controls.MapCartographicMode CartographicMode
        {
            get
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("_cartographicmode_"))
                {
                    IsolatedStorageSettings.ApplicationSettings["_cartographicmode_"] = Microsoft.Phone.Maps.Controls.MapCartographicMode.Road.ToString();
                    //IsolatedStorageSettings.ApplicationSettings.Save();
                }
                return (Microsoft.Phone.Maps.Controls.MapCartographicMode)Enum.Parse(typeof(Microsoft.Phone.Maps.Controls.MapCartographicMode), IsolatedStorageSettings.ApplicationSettings["_cartographicmode_"].ToString());
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["_cartographicmode_"] = value.ToString();
                //IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public string Color
        {
            get
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("_color_"))
                {
                    IsolatedStorageSettings.ApplicationSettings["_color_"] = "Red";
                    //IsolatedStorageSettings.ApplicationSettings.Save();
                }
                return IsolatedStorageSettings.ApplicationSettings["_color_"].ToString();
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["_color_"] = value;
                //IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public void Save()
        {
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void InitializeAppConfig()
        {
        }

        #region Singleton Pattern w/ Constructor
        private AppConfig()
            : base()
        {
            InitializeAppConfig();
        }
        public static AppConfig Instance
        {
            get
            {
                return SingletonAppConfigCreator._Instance;
            }
        }
        private class SingletonAppConfigCreator
        {
            private SingletonAppConfigCreator() { }
            public static AppConfig _Instance = new AppConfig();
        }
        #endregion
    }


}
