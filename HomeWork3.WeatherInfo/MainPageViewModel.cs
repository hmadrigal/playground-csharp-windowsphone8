using HomeWork2.Interactivity;
using HomeWork2.Models;
using HomeWork2.Services;
using HomeWork2.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Phone.Shell;
using Windows.Phone.System.UserProfile;
using HomeWork3.WeatherInfo;
using HomeWork3.NotifyScheduleTaskAgent;

namespace HomeWork3
{
    public class MainPageViewModel : BindableBase
    {

        #region CityName (INotifyPropertyChanged Property)
        public string CityName
        {
            get { return _cityName; }
            set { SetProperty(ref _cityName, value); }
        }
        private string _cityName;
        #endregion

        #region IsLoading (INotifyPropertyChanged Property)
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        private bool _isLoading;
        #endregion

        #region CurrentWeather (INotifyPropertyChanged Property)
        public WeatherCurrentItem CurrentWeather
        {
            get { return _currentWeather; }
            set { SetProperty(ref _currentWeather, value); }
        }
        private WeatherCurrentItem _currentWeather;
        #endregion

        public ObservableCollection<WeatheForecastItem> WeatherForecast { get; private set; }

        public MainPageViewModel()
        {
            WeatherForecast = new ObservableCollection<WeatheForecastItem>();
        }

        private async void LoadContent()
        {
            var weatherStats = IsoStoreHelper.LoadFromIsoStore<WeatherStats>(WeatherStats.WeatherSettingsKeyName, (k) => new WeatherStats());
            IsLoading = true;
            CityName = string.Concat(weatherStats.SelectedCity.AreaName, ", ", weatherStats.SelectedCity.Country);
            var weatherResult = await DataProvider.Instance.GetWeatherResults(weatherStats.SelectedCity.Latitude, weatherStats.SelectedCity.Longitude);
            foreach (var item in weatherResult.Item2)
            {
                WeatherForecast.Add(item);
            }
            CurrentWeather = weatherResult.Item1;
            IsLoading = false;

        }

        internal void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadContent();
        }
    }
}
