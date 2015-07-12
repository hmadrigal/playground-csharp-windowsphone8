using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Microsoft.Phone.Maps.Controls;

namespace HomeWork6.AirportMap
{
    public partial class ConfigPage : PhoneApplicationPage
    {
        public class ConfigPageViewModel
        {
            public ObservableCollection<CountryInfo> Countries { get; private set; }
            public ObservableCollection<string> CartographicModes { get; private set; }
            public ObservableCollection<string> AvailableColors { get; private set; }

            public ConfigPageViewModel()
            {
                Countries = new ObservableCollection<CountryInfo>();
                CartographicModes = new ObservableCollection<string>(Enum.GetNames(typeof(MapCartographicMode)));
                AvailableColors = new ObservableCollection<string>()
                {
                    "Black",
                    "Blue",
                    "Brown",
                    "Cyan",
                    "DarkGray",
                    "Gray",
                    "Green",
                    "LightGray",
                    "Magenta",
                    "Orange",
                    "Purple",
                    "Red",
                    "Transparent",
                    "White",
                    "Yellow",
                };
            }
        }
        public ConfigPageViewModel ViewModel { get; set; }

        public ConfigPage()
        {
            InitializeComponent();
            ViewModel = new ConfigPageViewModel();
            DataContext = ViewModel;
            Loaded += OnConfigPageLoaded;
        }

        private void OnConfigPageLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnConfigPageLoaded;
            if (string.IsNullOrEmpty(AppConfig.Instance.CountryListDocument))
            {
                var wc = new WebClient();
                wc.DownloadStringCompleted += wc_DownloadStringCompleted;
                wc.DownloadStringAsync(new Uri("http://www.oorsprong.org/websamples.countryinfo/CountryInfoService.wso/FullCountryInfoAllCountries"));
            }
            else
            {
                LoadCountryListDocument(AppConfig.Instance.CountryListDocument);
            }

        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            (sender as WebClient).DownloadStringCompleted -= wc_DownloadStringCompleted;
            if (e.Error != null)
            {
                MessageBox.Show("There was an error getting the list of countries.\n" + e.Error.Message);
                return;
            }

            var countryListDocument = e.Result;
            AppConfig.Instance.CountryListDocument = countryListDocument;
            LoadCountryListDocument(countryListDocument);
        }

        private void LoadCountryListDocument(string countryListDocument)
        {
            var document = XDocument.Parse(countryListDocument);
            var countries = from countryInfoElements in document.Root.Elements("tCountryInfo")
                            select new CountryInfo()
                            {
                                Name = (string)countryInfoElements.Element("sName"),
                                CountryFlagUrl = (string)countryInfoElements.Element("sCountryFlag"),
                            };
            foreach (var item in countries)
            {
                ViewModel.Countries.Add(item);
            }
            var foundCountry = ViewModel.Countries.FirstOrDefault(c => c.Name == AppConfig.Instance.Country);
            if (foundCountry == null)
            {
                foundCountry = ViewModel.Countries.FirstOrDefault();
            }
            countryPicker.SelectedItem = foundCountry;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }
            cartographicModePicker.SelectedItem = AppConfig.Instance.CartographicMode.ToString();
            colorPicker.SelectedItem = AppConfig.Instance.Color;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if ((countryPicker.SelectedItem as CountryInfo) == null)
            {
                return;
            }
            AppConfig.Instance.Country = (countryPicker.SelectedItem as CountryInfo).Name;
            AppConfig.Instance.CountryFlagUrl = (countryPicker.SelectedItem as CountryInfo).CountryFlagUrl;
            AppConfig.Instance.Color = colorPicker.SelectedItem.ToString();
            AppConfig.Instance.CartographicMode = (MapCartographicMode)Enum.Parse(typeof(MapCartographicMode), cartographicModePicker.SelectedItem.ToString());
            AppConfig.Instance.Save();
        }
    }
}