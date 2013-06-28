using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using HomeWork3;
using HomeWork3.NotifyScheduleTaskAgent;
using System.Threading.Tasks;
using HomeWork2.Services;
using Microsoft.Phone.Shell;
using System;
using System.Linq;

namespace HomeWork3ScheduledTasks
{
    public class DynamicScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static DynamicScheduledAgent()
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
        protected override void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background
            try
            {
                UpdateContent();
            }
            catch { }
            NotifyComplete();
        }

        private async Task UpdateContent()
        {
            var weatherStats = IsoStoreHelper.LoadFromIsoStore<WeatherStats>(WeatherStats.WeatherSettingsKeyName, (k) => new WeatherStats());
            var weatherResult = await DataProvider.Instance.GetWeatherResults(weatherStats.SelectedCity.Latitude, weatherStats.SelectedCity.Longitude);

            var oIconicTileData = new IconicTileData();
            oIconicTileData.Title = DateTime.Now.ToString("o");
            oIconicTileData.WideContent1 = string.Format("NOW: {0}", weatherResult.Item1.WeatherDesc);
            oIconicTileData.WideContent2 = string.Format("Tomorrow: {0}", weatherResult.Item2.FirstOrDefault().WeatherDesc);
            oIconicTileData.WideContent3 = string.Format("After tomorrow: {0}", weatherResult.Item2.Skip(1).FirstOrDefault().WeatherDesc);
            oIconicTileData.Count = 3;
            TileManager.Instance.SetApplicationTileData(oIconicTileData);
        }
    }
}