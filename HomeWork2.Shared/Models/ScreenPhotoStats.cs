using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork2.Models
{
    public class ScreenPhotoStats
    {
        public const string ScreenPhotoStatsKeyName = @"__ScreenPhotoStatsKeyName__";
        public string Topic { get; set; }
        public List<PhotoItem> StoredPhotos { get; set; }

        public ScreenPhotoStats()
        {
            Topic = @"Love"; //string.Empty;
            StoredPhotos = new List<PhotoItem>();
        }
    }
}
