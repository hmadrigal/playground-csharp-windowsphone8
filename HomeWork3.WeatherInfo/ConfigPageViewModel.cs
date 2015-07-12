using HomeWork2.Interactivity;
using HomeWork2.Models;
using HomeWork2.Services;
using HomeWork2.ViewModels;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using HomeWork3.WeatherInfo;
using HomeWork3.NotifyScheduleTaskAgent;

namespace HomeWork3
{
    public class ConfigPageViewModel : BindableBase
    {

        #region IsLoading (INotifyPropertyChanged Property)
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        private bool _isLoading;
        #endregion

        #region CityName (INotifyPropertyChanged Property)
        public string CityName
        {
            get { return _cityName; }
            set { SetProperty(ref _cityName, value); }
        }
        private string _cityName;
        #endregion

        public ObservableCollection<SearchApiResultItem> SearchResults { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        public ICommand SelectCityCommand { get; private set; }

        public ConfigPageViewModel()
        {
            SearchResults = new ObservableCollection<SearchApiResultItem>();
            SearchCityCommand = new RelayCommand<string>(
                OnSearchCityCommandInvoked,
                (cityName) => !string.IsNullOrEmpty(cityName) && cityName.Length > 3);

            SelectCityCommand = new RelayCommand<SearchApiResultItem>(OnSelectCityCommandInvoked);
            var weatherStats = IsoStoreHelper.LoadFromIsoStore<WeatherStats>(WeatherStats.WeatherSettingsKeyName, (k) => new WeatherStats());
            CityName = string.Concat(weatherStats.SelectedCity.AreaName, ", ", weatherStats.SelectedCity.Country);
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

        private void OnSelectCityCommandInvoked(SearchApiResultItem selectedSearchApiResultItem)
        {
            if (selectedSearchApiResultItem == null)
            {
                return;
            }
            SearchResults.Clear();
            var weatherStats = IsoStoreHelper.LoadFromIsoStore<WeatherStats>(WeatherStats.WeatherSettingsKeyName, (k) => new WeatherStats());
            weatherStats.SelectedCity = selectedSearchApiResultItem;
            IsoStoreHelper.SaveToIsoStore(WeatherStats.WeatherSettingsKeyName, weatherStats);
            CityName = string.Concat(weatherStats.SelectedCity.AreaName, ", ", weatherStats.SelectedCity.Country);
            var phoneApplicationFrame = (App.Current.RootVisual as Microsoft.Phone.Controls.PhoneApplicationFrame);
            if (phoneApplicationFrame != null && phoneApplicationFrame.CanGoBack)
            {
                phoneApplicationFrame.GoBack();
            }
        }
    }
}
