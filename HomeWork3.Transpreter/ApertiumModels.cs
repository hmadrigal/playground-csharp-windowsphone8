using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    [DataContract]
    public class ResponseData
    {
        [DataMember]
        public string translatedText { get; set; }
    }

    [DataContract]
    public class TranslateResponse
    {
        [DataMember]
        public ResponseData responseData { get; set; }
        [IgnoreDataMember]
        public string responseDetails { get; set; }
        [DataMember]
        public int responseStatus { get; set; }
    }
}
