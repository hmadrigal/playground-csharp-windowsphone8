using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2.ViewModels
{
    public partial class MainPageViewModel : BindableBase
    {
        public Windows.UI.Xaml.Media.Imaging.BitmapImage GetBitmapSource(System.IO.Stream stream)
        {
            Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            Windows.Storage.Streams.InMemoryRandomAccessStream ras = new Windows.Storage.Streams.InMemoryRandomAccessStream();
            stream.CopyTo(System.IO.WindowsRuntimeStreamExtensions.AsStreamForRead(ras));
            bitmapSource.SetSource(ras);
            return bitmapSource;
        }
    }
}
