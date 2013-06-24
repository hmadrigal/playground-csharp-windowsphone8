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

namespace HomeWork3
{
    public class MainPageViewModel : BindableBase
    {
        public SearchApiResultItem SelectedCity
        {
            get { return IsolatedStorageSettings.ApplicationSettings[App.CityInfoKeyName] as SearchApiResultItem; }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[App.CityInfoKeyName] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

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

        public ICommand ViewLoadedCommand { get; private set; }

        public MainPageViewModel()
        {
            ViewLoadedCommand = new RelayCommand(OnViewLoadedCommandInvoked);
            WeatherForecast = new ObservableCollection<WeatheForecastItem>();
        }

        private async void OnViewLoadedCommandInvoked()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(App.CityInfoKeyName))
            {
                IsolatedStorageSettings.ApplicationSettings[App.CityInfoKeyName] = new SearchApiResultItem()
                {
                    AreaName = "San Jose",
                    Country = "Costa Rica",
                    Latitude = 9.933,
                    Longitude = -84.083,
                    Region = "San Jose",
                    WeatherUrl = 0.0,
                };
            }
            IsLoading = true;
            var weatherResult = await DataProvider.Instance.GetWeatherResults(SelectedCity.Latitude, SelectedCity.Longitude);
            foreach (var item in weatherResult.Item2)
            {
                WeatherForecast.Add(item);
            }
            CurrentWeather = weatherResult.Item1;
            IsLoading = false;

        }
    }
}
