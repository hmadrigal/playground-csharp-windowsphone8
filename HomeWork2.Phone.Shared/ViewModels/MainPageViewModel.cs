using HomeWork2.Interactivity;
using HomeWork2.Models;
using HomeWork2.Services;
using System;
using System.Collections.Generic;
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
            set { SetProperty(ref _searchApiResultItem, value); }
        }
        private SearchApiResultItem _searchApiResultItem;
        #endregion
        

        public ICommand SearchCityCommand { get; private set; }

        public ICommand SaveCityCommand { get; private set; }

        public MainPageViewModel()
        {
            SaveCityCommand = new RelayCommand<string>(
                OnSaveCityCommandInvoked,
                (cityName) => !string.IsNullOrEmpty(cityName)
                );
            SearchCityCommand = new RelayCommand<string>(
                OnSearchCityCommandInvoked,
                (cityName) => !string.IsNullOrEmpty(cityName) && cityName.Length > 3);
        }

        private async void OnSearchCityCommandInvoked(string cityName)
        {
            var searchResults = await DataProvider.Instance.GetSearchResults(cityName);
        }

        private void OnSaveCityCommandInvoked(string cityName)
        {
        }
    }
}
