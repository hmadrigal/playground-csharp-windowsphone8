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
        #region Topic (INotifyPropertyChanged Property)
        public string Topic
        {
            get { return _topic; }
            set { SetProperty(ref _topic, value); }
        }
        private string _topic;
        #endregion

        internal void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            var screenPhotoStats = IsoStoreHelper.LoadFromIsoStore<ScreenPhotoStats>(ScreenPhotoStats.ScreenPhotoStatsKeyName, _ => new ScreenPhotoStats());
            screenPhotoStats.Topic = Topic;
            IsoStoreHelper.SaveToIsoStore(ScreenPhotoStats.ScreenPhotoStatsKeyName, screenPhotoStats);
        }

        internal void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            var screenPhotoStats = IsoStoreHelper.LoadFromIsoStore<ScreenPhotoStats>(ScreenPhotoStats.ScreenPhotoStatsKeyName, _ => new ScreenPhotoStats());
            Topic = screenPhotoStats.Topic;
        }
    }
}
