using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2.Services
{
    public sealed class LocalStorageSettings
    {
        private const string SettingsFilename = "LocalStorageSettings.obj";

        private Dictionary<string, object> Settings
        {
            get { return _settings ?? (_settings = new Dictionary<string, object>()); }
            set { _settings = value; }
        }
        private Dictionary<string, object> _settings;


        private void InitializeLocalStorageSettings()
        {
            Load().Wait();
        }

        public async Task Save()
        {
            await FileManager.Instance.SaveAsync<Dictionary<string, object>>(SettingsFilename, Settings, DataContractObjectSerializer.Instance);
        }

        public async Task Load()
        {
            Settings = await FileManager.Instance.LoadAsync<Dictionary<string, object>>(SettingsFilename, DataContractObjectSerializer.Instance);
        }

        public bool Contains(string key)
        {
            return Settings.ContainsKey(key);
        }

        public object this[string key]
        {
            get { return Settings.ContainsKey(key) ? Settings[key] : null; }
            set
            {
                if (value == null)
                {
                    Settings.Remove(key);
                }
                else
                {
                    Settings[key] = value;
                }
            }
        }

        #region Singleton Pattern w/ Constructor
        private LocalStorageSettings()
        {
            InitializeLocalStorageSettings();
        }
        public static LocalStorageSettings ApplicationSettings
        {
            get
            {
                return SingletonLocalStorageSettingsCreator._Instance;
            }
        }



        private class SingletonLocalStorageSettingsCreator
        {
            private SingletonLocalStorageSettingsCreator() { }
            public static LocalStorageSettings _Instance = new LocalStorageSettings();
        }
        #endregion
    }
}
