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
using HomeWork3ScheduledTasks;
using Windows.Phone.System.UserProfile;

namespace HomeWork3
{
    public class ScreenPhotoStats
    {
        public const string ScreenPhotoStatsKeyName = @"__ScreenPhotoStatsKeyName__";
        public string Topic { get; set; }
        public List<PhotoItem> StoredPhotos { get; set; }

        public ScreenPhotoStats()
        {
            Topic = @"Love"; //string.Empty;
            StoredPhotos = new List<PhotoItem>();
        }
    }


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

        #region IsLoading (INotifyPropertyChanged Property)
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        private bool _isLoading;
        #endregion

        public ObservableCollection<Object> DisplayItems { get; private set; }

        public ICommand LoadCommand { get; private set; }

        List<PhotoItem> _requestedPhotoItems;

        public MainPageViewModel()
        {
            DisplayItems = new ObservableCollection<Object>();
            LoadCommand = new RelayCommand(OnLoadCommandInvoked);
            TransferManager.Instance.SubscribeToProgress(OnTransferProgressChanged);
            TransferManager.Instance.SubscribeToStatus(OnTransferCompletedChanged, bt => bt.TransferStatus == Microsoft.Phone.BackgroundTransfer.TransferStatus.Completed);
        }

        public void OnTransferProgressChanged(Microsoft.Phone.BackgroundTransfer.BackgroundTransferRequest request)
        {
            var transferPayload = DisplayItems.OfType<TransferPayload>().FirstOrDefault(tp => tp.Tag == request.Tag);
            if (transferPayload == null)
            {
                return;
            }
            transferPayload.Status = string.Format("[{0}] Transfered: {1}kp", request.TransferStatus.ToString(), request.BytesReceived / 1024);
        }

        public void OnTransferCompletedChanged(Microsoft.Phone.BackgroundTransfer.BackgroundTransferRequest request)
        {
            var transferPayload = DisplayItems.OfType<TransferPayload>().FirstOrDefault(tp => tp.Tag == request.Tag);
            var requestedPhotoItem = _requestedPhotoItems.OfType<PhotoItem>().FirstOrDefault(pr => pr.ExternalUrl == request.RequestUri.ToString());
            if (transferPayload == null || requestedPhotoItem == null)
            {
                return;
            }
            DisplayItems.Remove(transferPayload);
            requestedPhotoItem.LocalFilename = transferPayload.LocalUrl;
            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    var fileStream = isoStore.OpenFile(requestedPhotoItem.LocalFilename, System.IO.FileMode.Open);
                    requestedPhotoItem.SmallImage = GetBitmapSource(fileStream);
                }
            }
            catch { }
            DisplayItems.Add(requestedPhotoItem);
            System.Diagnostics.Debug.WriteLine("Pending Transfers Count: {0}", DisplayItems.OfType<TransferPayload>().Count());
        }

        private async void OnLoadCommandInvoked()
        {
            IsLoading = true;
            DisplayItems.Clear();

            var screenPhotoStats = LoadFromIsoStore<ScreenPhotoStats>(ScreenPhotoStats.ScreenPhotoStatsKeyName, _ => new ScreenPhotoStats());
            Title = screenPhotoStats.Topic;

            var addedItems = await Task.Run(async () =>
            {
                List<object> newItems = new List<object>();
                List<PhotoItem> toRemove = new List<PhotoItem>();
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {

                    // get photo list from server
                    _requestedPhotoItems = (await DataProvider.Instance.GetPhotos(screenPhotoStats.Topic, (stream) => null)).ToList();
                    foreach (var requestedPhotoItem in _requestedPhotoItems)
                    {
                        // Is the item already downloaded?
                        var localFilePath = TransferManager.Instance.GetFullTransferFilePath(requestedPhotoItem.LocalFilename);
                        if (isoStore.FileExists(localFilePath))
                        {
                            requestedPhotoItem.LocalFilename = localFilePath;
                            newItems.Add(requestedPhotoItem);
                            toRemove.Add(requestedPhotoItem);
                        }
                        else
                        {
                            var transferPayload = new TransferPayload()
                            {
                                Status = "Queued",
                                LocalUrl = localFilePath,
                                RemoteUrl = requestedPhotoItem.ExternalUrl,
                                Tag = requestedPhotoItem.LocalFilename
                            };
                            newItems.Add(transferPayload);
                            TransferManager.Instance.AddBackgroundTransfer(transferPayload);
                        }
                    }
                    foreach (var item in toRemove)
                    {
                        _requestedPhotoItems.Remove(item);
                    }
                }
                return newItems;
            });

            // Display the new Items (from disc or background transfer.
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                foreach (var photoItem in addedItems.OfType<PhotoItem>())
                {
                    try
                    {
                        var fileStream = isoStore.OpenFile(photoItem.LocalFilename, System.IO.FileMode.Open);
                        photoItem.SmallImage = GetBitmapSource(fileStream);
                    }
                    catch { }
                }
            }
            foreach (var item in addedItems)
            {
                DisplayItems.Add(item);
            }
            IsLoading = false;
        }

        internal static System.Windows.Media.Imaging.BitmapImage GetBitmapSource(System.IO.Stream stream)
        {
            System.Windows.Media.Imaging.BitmapImage bitmapSource = new System.Windows.Media.Imaging.BitmapImage();
            bitmapSource.SetSource(stream);
            return bitmapSource;
        }

        internal void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        internal void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            var screenPhotoStats = LoadFromIsoStore<ScreenPhotoStats>(ScreenPhotoStats.ScreenPhotoStatsKeyName, _ => new ScreenPhotoStats());
            screenPhotoStats.StoredPhotos = DisplayItems.OfType<PhotoItem>().ToList();
            SaveToIsoStore(ScreenPhotoStats.ScreenPhotoStatsKeyName, screenPhotoStats);
        }

        internal static T LoadFromIsoStore<T>(string key, Func<string, T> defaultValue = null)
        {
            if (defaultValue == null)
            {
                defaultValue = _ => default(T);
            }
            return IsolatedStorageSettings.ApplicationSettings.Contains(key) ? (T)IsolatedStorageSettings.ApplicationSettings[key] : defaultValue(key);
        }

        internal static void SaveToIsoStore<T>(string key, T value)
        {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        
    }
}
