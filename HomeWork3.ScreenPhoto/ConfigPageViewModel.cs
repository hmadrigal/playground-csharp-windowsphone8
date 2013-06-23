using HomeWork2.Interactivity;
using HomeWork2.Models;
using HomeWork2.Services;
using HomeWork2.ViewModels;
using HomeWork3ScheduledTasks;
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
            get { return LocalStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] as string; }
            set {
                LocalStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] = value;
                LocalStorageSettings.ApplicationSettings.Save();
            }
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
