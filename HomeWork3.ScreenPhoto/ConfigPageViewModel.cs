using HomeWork2.Interactivity;
using HomeWork2.Models;
using HomeWork2.Services;
using HomeWork2.ViewModels;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork3
{
    public class ConfigPageViewModel : BindableBase
    {

        public string Topic
        {
            get { return IsolatedStorageSettings.ApplicationSettings[MainPageViewModel.TopicKeyName] as string; }
            set { IsolatedStorageSettings.ApplicationSettings[MainPageViewModel.TopicKeyName] = value; }
        }

        public ObservableCollection<PhotoItem> Photos { get; private set; }

        public ICommand LoadCommand { get; private set; }
        

        public ConfigPageViewModel()
        {
            LoadCommand = new RelayCommand(OnLoadCommandInvoked);
        }

        private async void OnLoadCommandInvoked()
        {
        }

    }
}
