
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
namespace HomeWork2.Services
{

    public sealed class ContentAccessor
    {
        public async Task<Stream> GetContent(Uri uri)
        {
            var cacheManager = FileCacheManager.Instance;
            var fileKey = cacheManager.GetFileKey(uri.ToString());
            if (await cacheManager.HasExpired(fileKey))
            {
                var inputStream = await GetWebStream(uri);
                await cacheManager.SaveAsync(fileKey, inputStream);
            }
            return await cacheManager.Load(fileKey);
        }

        private async Task<Stream> GetWebStream(Uri uri)
        {
            var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(uri);
            return stream;
        }

        private void InitializeContentAccessor()
        {
        }

        #region Singleton Pattern w/ Constructor
        private ContentAccessor()
            : base()
        {
            InitializeContentAccessor();
        }
        public static ContentAccessor Instance
        {
            get
            {
                return SingletonContentAccessorCreator._Instance;
            }
        }
        private class SingletonContentAccessorCreator
        {
            private SingletonContentAccessorCreator() { }
            public static ContentAccessor _Instance = new ContentAccessor();
        }
        #endregion
    }


}
