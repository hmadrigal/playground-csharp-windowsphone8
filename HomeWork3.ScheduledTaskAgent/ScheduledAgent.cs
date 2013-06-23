using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using HomeWork2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeWork2.Models;
using System.Linq;
using System.IO.IsolatedStorage;

namespace HomeWork3
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        public string Topic
        {
            get { return IsolatedStorageSettings.ApplicationSettings[TopicKeyName] as string; }
            set { IsolatedStorageSettings.ApplicationSettings[TopicKeyName] = value; }
        }
        
        public const string TopicKeyName = @"topic";


        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            try
            {
                await UpdateCycleTilesAsync();
            }
            catch (Exception ex)
            { }
            //TODO: Add code to perform your task in background
            NotifyComplete();
        }

        private async Task UpdateCycleTilesAsync()
        {
            var Photos = (await DataProvider.Instance.GetPhotos(Topic, _ => null)).ToList();
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
    }
}