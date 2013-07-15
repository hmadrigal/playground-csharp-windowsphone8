using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.AirportMap
{
    public class CountryInfo : BindableBase
    {
        #region ISOCode (INotifyPropertyChanged Property)
        public string ISOCode
        {
            get { return _iSOCode; }
            set { SetProperty(ref _iSOCode, value); }
        }
        private string _iSOCode;
        #endregion

        #region Name (INotifyPropertyChanged Property)
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;
        #endregion

        #region CountryFlagUrl (INotifyPropertyChanged Property)
        public string CountryFlagUrl
        {
            get { return _countryFlagUrl; }
            set { SetProperty(ref _countryFlagUrl, value); }
        }
        private string _countryFlagUrl;
        #endregion


    }
}
