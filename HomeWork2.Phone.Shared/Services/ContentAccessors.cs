
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
namespace HomeWork2.Services
{

    public sealed class ContentAccessors
    {
        public async Task<Stream> GetContent(Uri uri, IContentPolicyAccessor contentPolicyAccessor)
        {
            var cacheManager = FileManager.Instance;
            var fileKey = contentPolicyAccessor.GetFileKey(uri);
            if (contentPolicyAccessor.IsExpired(uri))
            {
                var inputStream = await GetWebStream(uri);
                await cacheManager.SaveAsync(fileKey, inputStream);
            }
            return await cacheManager.Load(fileKey);
        }

        private async Task<Stream> GetWebStream(Uri uri)
        {
            Stream resultStream = null;
            await Task.Run(() =>
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
        private ContentAccessors()
            : base()
        {
            InitializeContentAccessor();
        }
        public static ContentAccessors Instance
        {
            get
            {
                return SingletonContentAccessorCreator._Instance;
            }
        }
        private class SingletonContentAccessorCreator
        {
            private SingletonContentAccessorCreator() { }
            public static ContentAccessors _Instance = new ContentAccessors();
        }
        #endregion
    }


}
