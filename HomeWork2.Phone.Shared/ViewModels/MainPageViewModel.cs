using HomeWork2.Interactivity;
using HomeWork2.Models;
using HomeWork2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HomeWork2.ViewModels
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

        #region SelectedCity (INotifyPropertyChanged Property)
        public SearchApiResultItem SelectedCity
        {
            get { return _searchApiResultItem; }
            set { SetProperty(ref _searchApiResultItem, value); OnPropertyChanged("SelectedCityMessage"); }
        }
        private SearchApiResultItem _searchApiResultItem;
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

        public ObservableCollection<SearchApiResultItem> SearchResults { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        public ICommand SelectCityCommand { get; private set; }

        public ICommand SaveCityCommand { get; private set; }

        public MainPageViewModel()
        {
            SearchResults = new ObservableCollection<SearchApiResultItem>();
            WeatherForecast = new ObservableCollection<WeatheForecastItem>();

            SaveCityCommand = new RelayCommand<string>(
                OnSaveCityCommandInvoked,
                (cityName) => !string.IsNullOrEmpty(cityName)
                );

            SearchCityCommand = new RelayCommand<string>(
                OnSearchCityCommandInvoked,
                (cityName) => !string.IsNullOrEmpty(cityName) && cityName.Length > 3);

            SelectCityCommand = new RelayCommand<SearchApiResultItem>(OnSelectCityCommandInvoked);
        }

        private async void OnSelectCityCommandInvoked(SearchApiResultItem selectedSearchApiResultItem)
        {
            SelectedCity = selectedSearchApiResultItem;
            SearchResults.Clear();

            if (SelectedCity == null)
            {
                return;
            }

            var weatherResult = await DataProvider.Instance.GetWeatherResults(SelectedCity.Latitude, SelectedCity.Longitude);
            WeatherForecast.Clear();
            foreach (var item in weatherResult.Item2)
            {
                WeatherForecast.Add(item);
            }
            CurrentWeather = weatherResult.Item1;

            var photoResult = await DataProvider.Instance.GetPhotos(SelectedCity.Latitude, SelectedCity.Longitude);

            var newsResult = await DataProvider.Instance.GetNews(string.Format("{0} {1}", SelectedCity.AreaName, SelectedCity.Country));
            if (!newsResult.Any())
            {
                newsResult = await DataProvider.Instance.GetNews(string.Format("{0} {1}", SelectedCity.AreaName, SelectedCity.Region));
            }

        }

        private async void OnSearchCityCommandInvoked(string cityName)
        {
            SearchResults.Clear();
            var searchResults = await DataProvider.Instance.GetSearchResults(cityName);
            foreach (var item in searchResults.OrderBy(i => string.Concat(i.Country, i.Region, i.AreaName)))
            {
                SearchResults.Add(item);
            }
        }

        private void OnSaveCityCommandInvoked(string cityName)
        {
        }
    }
}
