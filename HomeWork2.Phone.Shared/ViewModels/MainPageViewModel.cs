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
        #region Title (INotifyPropertyChanged Property)
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _title;
        #endregion

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

        #region IsLoading (INotifyPropertyChanged Property)
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        private bool _isLoading;
        #endregion

        public ObservableCollection<WeatheForecastItem> WeatherForecast { get; private set; }

        public ObservableCollection<SearchApiResultItem> SearchResults { get; private set; }

        public ObservableCollection<PhotoItem> Photos { get; private set; }

        public ObservableCollection<NewsItem> News { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        public ICommand SelectCityCommand { get; private set; }

        public ICommand ViewLoadedCommand { get; private set; }

        public MainPageViewModel()
        {
            SearchResults = new ObservableCollection<SearchApiResultItem>();
            WeatherForecast = new ObservableCollection<WeatheForecastItem>();
            Photos = new ObservableCollection<PhotoItem>();
            News = new ObservableCollection<NewsItem>();

            SearchCityCommand = new RelayCommand<string>(
                OnSearchCityCommandInvoked,
                (cityName) => !string.IsNullOrEmpty(cityName) && cityName.Length > 3);

            SelectCityCommand = new RelayCommand<SearchApiResultItem>(OnSelectCityCommandInvoked);

            Title = @"City Explorer";
            ViewLoadedCommand = new RelayCommand(OnViewLoadedCommandInvoked);
        }

        private async void OnViewLoadedCommandInvoked()
        {
            SelectedCity = await FileManager.Instance.LoadAsync<SearchApiResultItem>("SelectedCity.obj", DataContractObjectSerializer.Instance);
            if (SelectedCity != null)
            {
                await LoadSelectedCity();
            }
        }

        private async void OnSelectCityCommandInvoked(SearchApiResultItem selectedSearchApiResultItem)
        {
            SelectedCity = selectedSearchApiResultItem;
            SearchResults.Clear();

            if (SelectedCity == null)
            {
                return;
            }

            await FileManager.Instance.SaveAsync<SearchApiResultItem>("SelectedCity.obj", SelectedCity, DataContractObjectSerializer.Instance);

            await LoadSelectedCity();
        }

        private async System.Threading.Tasks.Task LoadSelectedCity()
        {
            IsLoading = true;
            Title = string.Concat(SelectedCity.Region, ", ", SelectedCity.Country);
            Photos.Clear();
            News.Clear();
            WeatherForecast.Clear();
            CurrentWeather = null;

            var photoResult = await DataProvider.Instance.GetPhotos(string.Concat(SelectedCity.AreaName, " ", SelectedCity.Country));
            foreach (var item in photoResult)
            {
                Photos.Add(item);
            }

            var newsResult = await DataProvider.Instance.GetNews(string.Format("{0} {1}", SelectedCity.AreaName, SelectedCity.Country));
            if (!newsResult.Any())
            {
                newsResult = await DataProvider.Instance.GetNews(string.Format("{0} {1}", SelectedCity.AreaName, SelectedCity.Region));
            }
            foreach (var item in newsResult)
            {
                News.Add(item);
            }

            var weatherResult = await DataProvider.Instance.GetWeatherResults(SelectedCity.Latitude, SelectedCity.Longitude);
            foreach (var item in weatherResult.Item2)
            {
                WeatherForecast.Add(item);
            }
            CurrentWeather = weatherResult.Item1;
            IsLoading = false;
        }

        private async void OnSearchCityCommandInvoked(string cityName)
        {
            IsLoading = true;
            SearchResults.Clear();
            var searchResults = await DataProvider.Instance.GetSearchResults(cityName);
            foreach (var item in searchResults.OrderBy(i => string.Concat(i.Country, i.Region, i.AreaName)))
            {
                SearchResults.Add(item);
            }
            IsLoading = false;
        }

    }
}
