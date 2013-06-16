using HomeWork2.Interactivity;
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

        private void OnSearchCityCommandInvoked(string cityName)
        {

        }

        private void OnSaveCityCommandInvoked(string cityName)
        {
        }
    }
}
