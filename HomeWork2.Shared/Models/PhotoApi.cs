using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork2.Models
{
    public class PhotoItem
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Secret { get; set; }
        public string Server { get; set; }
        public string Farm { get; set; }
        public string Title { get; set; }
        public string Ispublic { get; set; }
        public string Isfriend { get; set; }
        public string Isfamily { get; set; }
        public object SmallImage { get; set; }
        public String ExternalUrl { get; set; }
    }
}
