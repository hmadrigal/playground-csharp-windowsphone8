
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
namespace HomeWork2.Services
{

    public sealed class ContentAccessor
    {
        public async Task<Stream> GetContent(Uri uri)
        {
            var cacheManager = FileManager.Instance;
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
            Stream resultStream = null;
            await Task.Run( () =>
            {
                ManualResetEvent allDone = new ManualResetEvent(false);
                var currentRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                currentRequest.BeginGetResponse(
                    (IAsyncResult result) =>
                    {
                        HttpWebRequest request = result.AsyncState as HttpWebRequest;
                        if (request != null)
                        {
                            WebResponse response = request.EndGetResponse(result);
                            resultStream = response.GetResponseStream();
                        }
                        allDone.Set();
                    }, currentRequest);
                allDone.WaitOne();
            });
            return resultStream;
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
