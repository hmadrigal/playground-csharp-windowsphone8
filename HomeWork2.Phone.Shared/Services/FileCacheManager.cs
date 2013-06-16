using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
namespace HomeWork2.Services
{

    public sealed class FileCacheManager
    {
        private static readonly string CacheFolderName = @"CacheFolder";
        private Windows.Storage.IStorageFolder _localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public async Task<bool> HasExpired(string fileKey)
        {
            var dataFolder = await _localFolder.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);
            var isThereFile = false;
            try
            {
                var file = await dataFolder.GetFileAsync(fileKey);
                isThereFile = true;
            }
            catch (Exception)
            {
                isThereFile = false;
            }
            return !isThereFile;
        }

        public string GetFileKey(string url)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < url.Length; i++)
            {
                hashedValue += url[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue.ToString();
        }

        public async Task<Stream> Load(string fileKey)
        {
            return await Windows.Storage.ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileKey);
        }

        public async Task SaveAsync(string fileKey, Stream input)
        {
            // Get the text data from the textbox. 
            byte[] fileBuffer = new byte[1024];

            // Create a new folder name DataFolder.
            var dataFolder = await _localFolder.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync(fileKey, CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                int lastReadPosition = 0, currentReadByteNumber = 0;
                while (0 < (currentReadByteNumber = await input.ReadAsync(fileBuffer, lastReadPosition, fileBuffer.Length)))
                {
                    s.Write(fileBuffer, 0, currentReadByteNumber);
                    lastReadPosition += currentReadByteNumber;
                }
            }
        }

        private void InitializeCacheManager()
        {

        }

        #region Singleton Pattern w/ Constructor
        private FileCacheManager()
            : base()
        {
            InitializeCacheManager();
        }
        public static FileCacheManager Instance
        {
            get
            {
                return SingletonCacheManagerCreator._Instance;
            }
        }
        private class SingletonCacheManagerCreator
        {
            private SingletonCacheManagerCreator() { }
            public static FileCacheManager _Instance = new FileCacheManager();
        }
        #endregion
    }


}
