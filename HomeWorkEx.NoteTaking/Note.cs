using HomeWork2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEx
{
    public class Note : BindableBase
    {
        #region Text (INotifyPropertyChanged Property)
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        private string _text;
        #endregion

        #region Foreground (INotifyPropertyChanged Property)
        public string Foreground
        {
            get { return _foreground; }
            set { SetProperty(ref _foreground, value); }
        }
        private string _foreground;
        #endregion

        #region Background (INotifyPropertyChanged Property)
        public string Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }
        private string _background;
        #endregion

    }
}
