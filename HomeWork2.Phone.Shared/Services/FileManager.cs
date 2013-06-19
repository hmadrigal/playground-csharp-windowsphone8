using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
namespace HomeWork2.Services
{
    public sealed class FileManager
    {
        internal static readonly string CacheFolderName = @"CacheFolder";
        private Windows.Storage.IStorageFolder _localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

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
                try
                {
                    instance = (T)serializer.Deserialize<T>(inputStream, typeof(T));
                }
                catch 
                {
                    instance = default(T);
                }
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
