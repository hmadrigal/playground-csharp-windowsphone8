using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeWork2.Services
{
    public interface IObjectSerializer
    {
        void Serialize(Stream streamObject, object objectForSerialization);
        object Deserialize(Stream streamObject, Type serializedObjectType);
        void Serialize<T>(Stream streamObject, T objectForSerialization);
        T Deserialize<T>(Stream streamObject, Type serializedObjectType);
    }


    public sealed class DataContractObjectSerializer : IObjectSerializer
    {
        public void Serialize(Stream outputStream, object objectForSerialization)
        {
            var serializer = new System.Runtime.Serialization.DataContractSerializer(objectForSerialization.GetType());
            serializer.WriteObject(outputStream, objectForSerialization);
        }

        public object Deserialize(Stream inputStream, Type serializedObjectType)
        {
            var serializer = new System.Runtime.Serialization.DataContractSerializer(serializedObjectType);
            var instance = serializer.ReadObject(inputStream);
            return instance;
        }

        public void Serialize<T>(Stream outputStream, T objectForSerialization)
        {
            var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            serializer.WriteObject(outputStream, objectForSerialization);
        }

        public T Deserialize<T>(Stream inputStream, Type serializedObjectType)
        {
            var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            T instance = default(T);
            try
            {
                instance = (T)serializer.ReadObject(inputStream);
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                instance = default(T);
            }
            return instance;
        }

        private void InitializeDataContractObjectSerializer()
        {
        }

        #region Singleton Pattern w/ Constructor
        private DataContractObjectSerializer()
            : base()
        {
            InitializeDataContractObjectSerializer();
        }
        public static DataContractObjectSerializer Instance
        {
            get
            {
                return SingletonDataContractObjectSerializerCreator._Instance;
            }
        }
        private class SingletonDataContractObjectSerializerCreator
        {
            private SingletonDataContractObjectSerializerCreator() { }
            public static DataContractObjectSerializer _Instance = new DataContractObjectSerializer();
        }
        #endregion
    }
}
