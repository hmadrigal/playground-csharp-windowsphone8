﻿using HomeWork2.Interactivity;
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

namespace HomeWork3
{
    public class MainPageViewModel : BindableBase
    {

        public string Topic
        {
            get { return LocalStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] as string; }
            set { LocalStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] = value; }
        }

        public ObservableCollection<PhotoItem> Photos { get; private set; }

        public ICommand LoadCommand { get; private set; }
        public ICommand NavigateToConfigCommand { get; private set; }


        public MainPageViewModel()
        {
            Photos = new ObservableCollection<PhotoItem>();
            LoadCommand = new RelayCommand(OnLoadCommandInvoked);
            NavigateToConfigCommand = new RelayCommand(OnNavigateToConfigCommandInvoked);
            if (!LocalStorageSettings.ApplicationSettings.Contains(CycleTileScheduledAgent.TopicKeyName))
            {
                LocalStorageSettings.ApplicationSettings[CycleTileScheduledAgent.TopicKeyName] = "Costa Rica";
            }
        }

        private void OnNavigateToConfigCommandInvoked()
        {
            (App.Current.RootVisual as Microsoft.Phone.Controls.PhoneApplicationFrame).Navigate(new Uri(@"/ConfigPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void OnLoadCommandInvoked()
        {
            await DownloadPicturesAsync(Topic);
            if (Photos.Count == 0)
            {
                return;
            }
            else if (Photos.Count <= 9)
            {
                await UpdateTileDate(Photos);
            }
            else
            {
                var photos = Photos.Skip(Photos.Count - 9);
                await UpdateTileDate(photos.ToList());
            }
        }

        private IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

        private async Task UpdateTileDate(IEnumerable<PhotoItem> photos)
        {
            if (photos == null || !photos.Any())
            {
                return;
            }

            var photoUris = photos.Select(pi => new Uri(pi.ExternalUrl, UriKind.RelativeOrAbsolute)).ToArray();
            for (int i = 0; i < photoUris.Length; i++)
            {
                LifeTimePolicyAccessor.Instance.SetTimeToLive(photoUris[i], TimeSpan.FromMinutes(30));
                var photoStream = await ContentAccessors.Instance.GetContent(photoUris[i], LifeTimePolicyAccessor.Instance);
                var fileName = string.Concat("CycleTileDataImg", i);
                await TileManager.Instance.SaveToSharedShellDirectory(fileName, photoStream);
                photoUris[i] = new Uri(TileManager.Instance.GetShellDirectoryFilePath(fileName), UriKind.RelativeOrAbsolute);
            }

            CycleTileData oCycleicon = new CycleTileData();
            oCycleicon.SmallBackgroundImage = new Uri("Photovoltaic-Panel.png", UriKind.Relative);
            // Images could be max Nine images.
            oCycleicon.CycleImages = photoUris;
            oCycleicon.Count = photoUris.Length;
            oCycleicon.Title = DateTime.Now.ToString("o"); //string.Concat("New ", photoUris.Length, " pics!"); ;
            TileManager.Instance.SetApplicationTileData(oCycleicon);
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
