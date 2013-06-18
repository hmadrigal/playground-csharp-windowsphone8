using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
namespace HomeWork2.Services
{

    public sealed class FileManager
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
            catch (FileNotFoundException)
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
            // Create a new folder name DataFolder.
            var dataFolder = await _localFolder.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);
            return await dataFolder.OpenStreamForReadAsync(fileKey);
        }

        public async Task SaveAsync(string fileKey, Stream inputStream)
        {
            var dataFolder = await _localFolder.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);
            var targetFile = await dataFolder.CreateFileAsync(fileKey, CreationCollisionOption.ReplaceExisting);
            using (var outputStream = await targetFile.OpenStreamForWriteAsync())
            {
                await inputStream.CopyToAsync(outputStream, 4096);
            }
        }

        public async Task SaveAsync<T>(string fileName, T instance, IObjectSerializer serializer)
        {
            var dataFolder = await _localFolder.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);
            var targetFile = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var outputStream = await targetFile.OpenStreamForWriteAsync())
            {
                serializer.Serialize<T>(outputStream, instance);
            }
        }

        public async Task<T> LoadAsync<T>(string fileName, IObjectSerializer serializer)
        {
            var dataFolder = await _localFolder.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);
            var targetFile = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            T instance = default(T);
            using (var inputStream = await targetFile.OpenStreamForReadAsync())
            {
                instance = (T)serializer.Deserialize<T>(inputStream, typeof(T));
            }
            return instance;
        }

        private void InitializeCacheManager()
        {

        }

        #region Singleton Pattern w/ Constructor
        private FileManager()
            : base()
        {
            InitializeCacheManager();
        }
        public static FileManager Instance
        {
            get
            {
                return SingletonCacheManagerCreator._Instance;
            }
        }
        private class SingletonCacheManagerCreator
        {
            private SingletonCacheManagerCreator() { }
            public static FileManager _Instance = new FileManager();
        }
        #endregion
    }


}
