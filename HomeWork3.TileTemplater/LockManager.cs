using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.System.UserProfile;

namespace HomeWork3
{

    public sealed class LockManager
    {
        public void LockScreenChangeSilently(string filePathOfTheImage, bool isAppResource = false)
        {
            try
            {
                // Only do further work if the access is granted.
                if (LockScreenManager.IsProvidedByCurrentApplication)
                {
                    // At this stage, the app is the active lock screen background provider.
                    // The following code example shows the new URI schema.
                    // ms-appdata points to the root of the local app data folder.
                    // ms-appx points to the Local app install folder, to reference resources bundled in the XAP package
                    var schema = isAppResource ? "ms-appx:///" : "ms-appdata:///Local/";
                    var uri = new Uri(schema + filePathOfTheImage, UriKind.Absolute);

                    // Set the lock screen background image.
                    LockScreen.SetImageUri(uri);

                    // Get the URI of the lock screen background image.
                    var currentImage = LockScreen.GetImageUri();
                    System.Diagnostics.Debug.WriteLine("The new lock screen background image is set to {0}", currentImage.ToString());
                }
            }
            catch 
            {
            }
        }

        public async Task LockScreenChange(string filePathOfTheImage, bool isAppResource = false)
        {
            if (!LockScreenManager.IsProvidedByCurrentApplication)
            {
                // If you're not the provider, this call will prompt the user for permission.
                // Calling RequestAccessAsync from a background agent is not allowed.
                await LockScreenManager.RequestAccessAsync();
            }

            // Only do further work if the access is granted.
            if (LockScreenManager.IsProvidedByCurrentApplication)
            {
                // At this stage, the app is the active lock screen background provider.
                // The following code example shows the new URI schema.
                // ms-appdata points to the root of the local app data folder.
                // ms-appx points to the Local app install folder, to reference resources bundled in the XAP package
                var schema = isAppResource ? "ms-appx:///" : "ms-appdata:///Local/";
                var uri = new Uri(schema + filePathOfTheImage, UriKind.Absolute);

                // Set the lock screen background image.
                LockScreen.SetImageUri(uri);

                // Get the URI of the lock screen background image.
                var currentImage = LockScreen.GetImageUri();
                System.Diagnostics.Debug.WriteLine("The new lock screen background image is set to {0}", currentImage.ToString());
            }
            else
            {
                System.Windows.MessageBox.Show("Background cant be updated as you clicked no!!");
            }
        }

        private void InitializeLockManager()
        {
        }

        #region Singleton Pattern w/ Constructor
        private LockManager()
            : base()
        {
            InitializeLockManager();
        }
        public static LockManager Instance
        {
            get
            {
                return SingletonLockManagerCreator._Instance;
            }
        }
        private class SingletonLockManagerCreator
        {
            private SingletonLockManagerCreator() { }
            public static LockManager _Instance = new LockManager();
        }
        #endregion
    }
}
