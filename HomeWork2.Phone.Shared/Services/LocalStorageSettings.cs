using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2.Services
{
    //public class UserConfiguration
    //{
    //    public string Topic { get; set; }
    //}

    //public sealed class LocalStorageSettings
    //{
    //    private const string UserConfigurationFilename = "LocalUserConfiguration.obj";

    //    public UserConfiguration Configuration { get; private set; }

    //    public void Save()
    //    {
    //        FileManager.Instance.SaveAsync<UserConfiguration>(UserConfigurationFilename, Configuration, DataContractObjectSerializer.Instance)
    //            .RunSynchronously();
    //    }

    //    public void Load()
    //    {
    //        Configuration = FileManager.Instance.LoadAsync<UserConfiguration>(UserConfigurationFilename, DataContractObjectSerializer.Instance).Result;
    //    }

    //    private void Initialize()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void InitializeLocalStorageSettings()
    //    {
    //        Configuration = new UserConfiguration();
    //    }

    //    #region Singleton Pattern w/ Constructor
    //    private LocalStorageSettings()
    //        : base()
    //    {
    //        InitializeLocalStorageSettings();
    //    }
    //    public static LocalStorageSettings ApplicationSettings
    //    {
    //        get
    //        {
    //            return SingletonLocalStorageSettingsCreator._ApplicationSettings;
    //        }
    //    }
    //    private class SingletonLocalStorageSettingsCreator
    //    {
    //        private SingletonLocalStorageSettingsCreator() { }
    //        public static LocalStorageSettings _ApplicationSettings = new LocalStorageSettings();
    //    }
    //    #endregion

    //}
}
