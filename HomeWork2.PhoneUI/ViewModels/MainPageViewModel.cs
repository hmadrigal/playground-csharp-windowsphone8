using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        #region CityName (INotifyPropertyChanged Property)
        public string CityName
        {
            get { return _cityName; }
            set
            {
                if (_cityName != value)
                {
                    _cityName = value;
                    RaisePropertyChanged("CityName");
                }
            }
        }
        private string _cityName;
        #endregion
 
       
    }
}
