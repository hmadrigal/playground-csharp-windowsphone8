using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    internal static class IsoStoreHelper
    {
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
