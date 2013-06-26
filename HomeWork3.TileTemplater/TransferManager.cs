using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.BackgroundTransfer;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeWork3
{
    public class TransferPayload : INotifyPropertyChanged
    {
        #region RemoteUrl (INotifyPropertyChanged Property)
        public string RemoteUrl
        {
            get { return _remoteUrl; }
            set { SetProperty(ref _remoteUrl, value); }
        }
        private string _remoteUrl;
        #endregion

        #region LocalUrl (INotifyPropertyChanged Property)
        public string LocalUrl
        {
            get { return _localUrl; }
            set { SetProperty(ref _localUrl, value); }
        }
        private string _localUrl;
        #endregion

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyNames"></param>
        protected virtual void RaisePropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null || propertyNames.Length == 0)
            {
                return;
            }
            for (int i = 0; i < propertyNames.Length; i++)
            {
                OnPropertyChanged(propertyNames[i]);
            }
        }
    }

    public sealed class TransferManager
    {
        public ObservableCollection<TransferPayload> CurrentTransfers
        {
            get { return _currentTransfers ?? (_currentTransfers = Load(TransferManagerListKeyName, _ => new ObservableCollection<TransferPayload>())); }
        }
        private ObservableCollection<TransferPayload> _currentTransfers;

        private readonly string TransferManagerListKeyName = @"__TransferManagerListKey__";

        private void Save<T>(string key, T item)
        {
            IsolatedStorageSettings.ApplicationSettings[key] = item;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private T Load<T>(string key, Func<string, T> defaultValue = null)
        {
            if (defaultValue == null)
            {
                defaultValue = (s) => default(T);
            }
            return IsolatedStorageSettings.ApplicationSettings.Contains(key) ? (T)IsolatedStorageSettings.ApplicationSettings[key] : defaultValue(key);
        }

        public void AddBackgroundTransfer(params TransferPayload[] transferPayloads)
        {
            foreach (var transferPayload in transferPayloads)
            {
                if (CurrentTransfers.Any(mr => mr.RemoteUrl == transferPayload.RemoteUrl))
                {
                    continue;
                }
                var request = new BackgroundTransferRequest(new Uri(transferPayload.RemoteUrl), new Uri(transferPayload.LocalUrl, UriKind.RelativeOrAbsolute));
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => { CurrentTransfers.Add(transferPayload); });
                BackgroundTransferService.Add(request);
                request.TransferProgressChanged += OnRequestTransferProgressChanged;
                request.TransferStatusChanged += OnRequestTransferStatusChanged;
            }
            Save(TransferManagerListKeyName, CurrentTransfers);
        }

        private void AttachEventsToBackgroundTransfers()
        {
            foreach (var item in BackgroundTransferService.Requests)
            {
                item.TransferProgressChanged += OnRequestTransferProgressChanged;
                item.TransferStatusChanged += OnRequestTransferStatusChanged;
            }
        }

        private void DettachEventsToBackgroundTransfers()
        {
            foreach (var item in BackgroundTransferService.Requests)
            {
                item.TransferProgressChanged -= OnRequestTransferProgressChanged;
                item.TransferStatusChanged -= OnRequestTransferStatusChanged;
            }
        }

        private void OnRequestTransferStatusChanged(object sender, BackgroundTransferEventArgs e)
        {

        }

        private void OnRequestTransferProgressChanged(object sender, BackgroundTransferEventArgs e)
        {
            switch (e.Request.TransferStatus)
            {
                case TransferStatus.Completed:
                    var foundRequest = CurrentTransfers.FirstOrDefault(request => request.LocalUrl == e.Request.DownloadLocation.ToString());
                    if (foundRequest != null)
                    {
                        System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => { CurrentTransfers.Remove(foundRequest); });
                        Save(TransferManagerListKeyName, CurrentTransfers);
                    }
                    e.Request.Dispose();
                    //BackgroundTransferService.Remove(e.Request);
                    break;
                case TransferStatus.None:
                    break;
                case TransferStatus.Paused:
                    break;
                case TransferStatus.Transferring:
                    break;
                case TransferStatus.Unknown:
                    break;
                case TransferStatus.Waiting:
                    break;
                case TransferStatus.WaitingForExternalPower:
                    break;
                case TransferStatus.WaitingForExternalPowerDueToBatterySaverMode:
                    break;
                case TransferStatus.WaitingForNonVoiceBlockingNetwork:
                    break;
                case TransferStatus.WaitingForWiFi:
                    break;
                default:
                    break;
            }
        }

        private void InitializeTransferManager()
        {
        }

        public void Deactivated()
        {
            DettachEventsToBackgroundTransfers();
        }

        public void Activated()
        {
            AttachEventsToBackgroundTransfers();
        }


        #region Singleton Pattern w/ Constructor
        private TransferManager()
            : base()
        {
        }
        public static TransferManager Instance
        {
            get
            {
                return SingletonTransferManagerCreator._Instance;
            }
        }
        private class SingletonTransferManagerCreator
        {
            private SingletonTransferManagerCreator() { }
            public static TransferManager _Instance = new TransferManager();
        }
        #endregion
    }


}
