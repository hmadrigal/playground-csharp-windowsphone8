using HomeWork2.Interactivity;
using HomeWork2.Models;
using HomeWork2.Services;
using HomeWork2.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork3
{
    public class MainPageViewModel : BindableBase
    {

        public string Topic
        {
            get { return IsolatedStorageSettings.ApplicationSettings[TopicKeyName] as string; }
            set { IsolatedStorageSettings.ApplicationSettings[TopicKeyName] = value; }
        }

        public ObservableCollection<PhotoItem> Photos { get; private set; }

        public ICommand LoadCommand { get; private set; }
        public ICommand NavigateToConfigCommand { get; private set; }

        private const string TopicKeyName = @"topic";

        public MainPageViewModel()
        {
            Photos = new ObservableCollection<PhotoItem>();
            LoadCommand = new RelayCommand(OnLoadCommandInvoked);
            NavigateToConfigCommand = new RelayCommand(OnNavigateToConfigCommandInvoked);
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(TopicKeyName))
            {
                IsolatedStorageSettings.ApplicationSettings[TopicKeyName] = "Costa Rica";
            }
        }

        private void OnNavigateToConfigCommandInvoked()
        {
            (App.Current.RootVisual as Microsoft.Phone.Controls.PhoneApplicationFrame).Navigate(new Uri(@"/ConfigPage.xaml", UriKind.RelativeOrAbsolute));
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
