using HomeWork2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork3
{
    public class ScreenPhotoStats
    {
        public const string ScreenPhotoStatsKeyName = @"__ScreenPhotoStatsKeyName__";
        public string Topic { get; set; }
        public List<PhotoItem> StoredPhotos { get; set; }

        public ScreenPhotoStats()
        {
            Topic = @"Dayna Baby Lou"; //string.Empty;
            StoredPhotos = new List<PhotoItem>();
        }
    }
}
