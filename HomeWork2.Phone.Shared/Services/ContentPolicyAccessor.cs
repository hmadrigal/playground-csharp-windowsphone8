using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HomeWork2.Services
{
    public abstract class BaseContentPolicyAccessor : IContentPolicyAccessor
    {
        public virtual string GetFileKey<T>(T state = default(T))
        {
            UInt64 hashedValue = 3074457345618258791ul;
            var objectString = state.ToString();
            for (int i = 0; i < objectString.Length; i++)
            {
                hashedValue += objectString[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue.ToString();
        }

        public virtual bool IsExpired<T>(T state = default(T))
        {
            var key = GetFileKey(state);
            return !FileExists(key);
        }

        protected bool FileExists(string fileName)
        {
            var dataFolderTask = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync(FileManager.CacheFolderName, CreationCollisionOption.OpenIfExists).AsTask();
            dataFolderTask.Wait();
            var dataFolder = dataFolderTask.Result;
            var isThereFile = false;
            try
            {
                var getFileTask = dataFolder.GetFileAsync(fileName).AsTask();
                getFileTask.Wait();
                isThereFile = getFileTask.Exception == null;
            }
            catch (AggregateException e)
            {
                isThereFile = false;
            }
            return !isThereFile;
        }
    }

    public sealed class KeepStoredPolicyAccessor : BaseContentPolicyAccessor
    {
        private void InitializeAlivePolicyAccessor()
        {
        }

        #region Singleton Pattern w/ Constructor
        private KeepStoredPolicyAccessor()
            : base()
        {
            InitializeAlivePolicyAccessor();
        }
        public static KeepStoredPolicyAccessor Instance
        {
            get
            {
                return SingletonAlivePolicyAccessorCreator._Instance;
            }
        }
        private class SingletonAlivePolicyAccessorCreator
        {
            private SingletonAlivePolicyAccessorCreator() { }
            public static KeepStoredPolicyAccessor _Instance = new KeepStoredPolicyAccessor();
        }
        #endregion

    }

    public sealed class LifeTimePolicyAccessor : BaseContentPolicyAccessor
    {
        private Dictionary<string, TimeSpan> _awaitTimeSpanTable;

        public void SetTimeToLive<T>(T state, TimeSpan awaitTimeSpan)
        {
            var key = GetFileKey(state);
            _awaitTimeSpanTable[key] = awaitTimeSpan;
        }

        public override bool IsExpired<T>(T state = default(T))
        {
            var fileName = GetFileKey(state);
            var dataFolderTask = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync(FileManager.CacheFolderName, CreationCollisionOption.OpenIfExists).AsTask();
            dataFolderTask.Wait();
            var dataFolder = dataFolderTask.Result;
            var isThereFile = false;
            var hasExpired = false;
            try
            {
                var getFileTask = dataFolder.GetFileAsync(fileName).AsTask();
                getFileTask.Wait();
                isThereFile = getFileTask.Exception == null;

                var file = getFileTask.Result;
                var awaitTimeSpan = _awaitTimeSpanTable[fileName];
                hasExpired = (DateTime.Now > (file.DateCreated + awaitTimeSpan));
                if (hasExpired)
                {
                    var deleteFileTask = file.DeleteAsync().AsTask();
                    deleteFileTask.Wait();
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                isThereFile = false;
                hasExpired = false;
            }
            return isThereFile && !hasExpired;
        }

        private void InitializeKeepForAWhilePolicyAccessor()
        {
            _awaitTimeSpanTable = new Dictionary<string, TimeSpan>();
        }
        
        #region Singleton Pattern w/ Constructor
        private LifeTimePolicyAccessor()
            : base()
        {
            InitializeKeepForAWhilePolicyAccessor();
        }
        public static LifeTimePolicyAccessor Instance
        {
            get
            {
                return SingletonKeepForAWhilePolicyAccessorCreator._Instance;
            }
        }
        private class SingletonKeepForAWhilePolicyAccessorCreator
        {
            private SingletonKeepForAWhilePolicyAccessorCreator() { }
            public static LifeTimePolicyAccessor _Instance = new LifeTimePolicyAccessor();
        }
        #endregion


    }
}
