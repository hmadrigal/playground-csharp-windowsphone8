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
        public List<string> DownloadedExternalUrls { get; set; }

        public ScreenPhotoStats()
        {
            Topic = @"Costa Rica"; //string.Empty;
            DownloadedExternalUrls = new List<string>();
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

        public List<PhotoItem> _photoResult;

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
            var photoItem = _photoResult.FirstOrDefault(pr => pr.ExternalUrl == request.RequestUri.ToString());
            if (transferPayload == null || photoItem == null)
            {
                return;
            }
            transferPayload.Status = request.TransferStatus.ToString();
            DisplayItems.Remove(transferPayload);
            DisplayItems.Add(photoItem);
        }

        private async void OnLoadCommandInvoked()
        {
            IsLoading = true;
            var screenPhotoStats = LoadFromIsoStore<ScreenPhotoStats>(ScreenPhotoStats.ScreenPhotoStatsKeyName, _ => new ScreenPhotoStats());
            Title = screenPhotoStats.Topic;
            
            DisplayItems.Clear();
            List<object> addedItems = await Task.Run(async () =>
            {
                List<object> newItems = new List<object>();
                _photoResult = (await DataProvider.Instance.GetPhotos(screenPhotoStats.Topic, (stream) => null)).ToList();
                foreach (var item in _photoResult)
                {
                    var storedPhotoItem = DisplayItems.OfType<PhotoItem>().FirstOrDefault(p => p.ExternalUrl == item.ExternalUrl);
                    if (storedPhotoItem == null)
                    {
                        var transferPayload = new TransferPayload()
                        {
                            LocalUrl = TransferManager.Instance.GetFullTransferFilePath(item.LocalFilename),
                            RemoteUrl = item.ExternalUrl,
                            Tag = item.LocalFilename
                        };
                        newItems.Add(transferPayload);
                        TransferManager.Instance.AddBackgroundTransfer(transferPayload);
                    }
                }
                return newItems;
            });

            foreach (var item in addedItems)
            {
                DisplayItems.Add(item);
            }
            IsLoading = false;
        }


        internal void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
            {
                return;
            }
        }

        internal void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings.Save();
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
