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

        string _topic;

        public string Topic
        {
            get { return IsolatedStorageSettings.ApplicationSettings[TopicKeyName] as string; }
            set { _topic = IsolatedStorageSettings.ApplicationSettings[TopicKeyName] as string; }
        }
        public ObservableCollection<PhotoItem> Photos { get; private set; }
        public ICommand LoadCommand { get; private set; }

        private const string TopicKeyName = @"topic";

        public ConfigPageViewModel()
        {
            Photos = new ObservableCollection<PhotoItem>();
            LoadCommand = new RelayCommand(OnLoadCommandInvoked);
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(TopicKeyName))
            {
                IsolatedStorageSettings.ApplicationSettings[TopicKeyName] = "Costa Rica";
            }
        }

        private async void OnLoadCommandInvoked()
        {
            await DownloadPicturesAsync(Topic);
        }

        public async Task DownloadPicturesAsync(string queryTerm)
        {
            //await Task.Run(async () =>
            //{
            Photos.Clear();
            var photoResult = await DataProvider.Instance.GetPhotos(queryTerm, GetBitmapSource);
            foreach (var item in photoResult)
            {
                Photos.Add(item);
            }
            //});
        }

        public System.Windows.Media.Imaging.BitmapImage GetBitmapSource(System.IO.Stream stream)
        {
            System.Windows.Media.Imaging.BitmapImage bitmapSource = new System.Windows.Media.Imaging.BitmapImage();
            bitmapSource.SetSource(stream);
            return bitmapSource;
        }
    }
}
