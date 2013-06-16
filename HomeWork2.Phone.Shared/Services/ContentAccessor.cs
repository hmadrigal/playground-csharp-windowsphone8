
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace HomeWork2.Phone.Shared.Services
{

    public sealed class ContentAccessor
    {

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
