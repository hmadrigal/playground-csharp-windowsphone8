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

        public TransferManager TransferManager { get { return TransferManager.Instance; } }

        private readonly Random _random = new Random();

        public string Topic
        {
            get { return IsolatedStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] as string; }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
                RaisePropertyChanged(CycleTileScheduledAgent.TopicKeyName);
            }
        }

        public ObservableCollection<PhotoItem> Photos { get; private set; }

        public ICommand LoadCommand { get; private set; }


        public MainPageViewModel()
        {
            Photos = new ObservableCollection<PhotoItem>();
            LoadCommand = new RelayCommand(OnLoadCommandInvoked);
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(CycleTileScheduledAgent.TopicKeyName))
            {
                IsolatedStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] = "Costa Rica";
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        private async void OnLoadCommandInvoked()
        {
            IsLoading = true;
            Title = Topic;

            Photos.Clear();
            await Task.Run(async () =>
            {
                var photoResult = await DataProvider.Instance.GetPhotos(Topic, (stream) => null);
                foreach (var item in photoResult)
                {
                    TransferManager.Instance.AddBackgroundTransfer(
                        new TransferPayload()
                        {
                            LocalUrl = "/shared/transfers/" + item.LocalFilename,
                            RemoteUrl = item.ExternalUrl,
                        }
                    );
                }
            });


            //await DownloadPicturesAsync(Topic);
            //if (Photos.Count == 0)
            //{
            //    IsLoading = false;
            //    return;
            //}
            //else if (Photos.Count <= 9)
            //{
            //    await UpdateTileDate(Photos);
            //    try
            //    {
            //        await LockManager.Instance.LockScreenChange(Photos[_random.Next(Photos.Count)].LocalFilename);
            //    }
            //    catch { }
            //}
            //else
            //{
            //    var photos = Photos.Skip(Photos.Count - 9);
            //    await UpdateTileDate(photos.ToList());
            //    try
            //    {
            //        await LockManager.Instance.LockScreenChange(Photos[_random.Next(Photos.Count)].LocalFilename);
            //    }
            //    catch { }
            //}
            IsLoading = false;
        }

        //private async Task UpdateTileDate(IEnumerable<PhotoItem> photos)
        //{
        //    if (photos == null || !photos.Any())
        //    {
        //        return;
        //    }

        //    var photoUris = photos.Select(pi => new Uri(pi.ExternalUrl, UriKind.RelativeOrAbsolute)).ToArray();
        //    for (int i = 0; i < photoUris.Length; i++)
        //    {
        //        LifeTimePolicyAccessor.Instance.SetTimeToLive(photoUris[i], TimeSpan.FromMinutes(30));
        //        var photoStream = await ContentAccessors.Instance.GetContent(photoUris[i], LifeTimePolicyAccessor.Instance);
        //        var fileName = string.Concat("CycleTileDataImg", i);
        //        await TileManager.Instance.SaveToSharedShellDirectory(fileName, photoStream);
        //        photoUris[i] = new Uri(TileManager.Instance.GetShellDirectoryFilePath(fileName), UriKind.RelativeOrAbsolute);
        //    }

        //    CycleTileData oCycleicon = new CycleTileData();
        //    oCycleicon.SmallBackgroundImage = new Uri("Photovoltaic-Panel.png", UriKind.Relative);
        //    // Images could be max Nine images.
        //    oCycleicon.CycleImages = photoUris;
        //    oCycleicon.Count = photoUris.Length;
        //    oCycleicon.Title = Topic; //DateTime.Now.ToString("o"); //string.Concat("New ", photoUris.Length, " pics!"); ;
        //    TileManager.Instance.SetApplicationTileData(oCycleicon);
        //}

        //public async Task DownloadPicturesAsync(string queryTerm)
        //{
        //    //await Task.Run(async () =>
        //    //{
        //    Photos.Clear();
        //    var photoResult = await DataProvider.Instance.GetPhotos(queryTerm, GetBitmapSource);
        //    foreach (var item in photoResult)
        //    {
        //        Photos.Add(item);
        //    }
        //    //});
        //}

        //public System.Windows.Media.Imaging.BitmapImage GetBitmapSource(System.IO.Stream stream)
        //{
        //    System.Windows.Media.Imaging.BitmapImage bitmapSource = new System.Windows.Media.Imaging.BitmapImage();
        //    bitmapSource.SetSource(stream);
        //    return bitmapSource;
        //}


    }
}
