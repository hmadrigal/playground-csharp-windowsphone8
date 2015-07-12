using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.AirportMap
{
    public class MapTableItem : BindableBase
    {
        #region AirportCode (INotifyPropertyChanged Property)
        public string AirportCode
        {
            get { return _airportCode; }
            set { SetProperty(ref _airportCode, value); }
        }
        private string _airportCode;
        #endregion

        #region CityOrAirportName (INotifyPropertyChanged Property)
        public string CityOrAirportName
        {
            get { return _cityOrAirportName; }
            set { SetProperty(ref _cityOrAirportName, value); }
        }
        private string _cityOrAirportName;
        #endregion

        #region Country (INotifyPropertyChanged Property)
        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }
        private string _country;
        #endregion

        #region CountryAbbrviation (INotifyPropertyChanged Property)
        public string CountryAbbrviation
        {
            get { return _countryAbbrviation; }
            set { SetProperty(ref _countryAbbrviation, value); }
        }
        private string _countryAbbrviation;
        #endregion

        #region CountryCode (INotifyPropertyChanged Property)
        public double CountryCode
        {
            get { return _countryCode; }
            set { SetProperty(ref _countryCode, value); }
        }
        private double _countryCode;
        #endregion

        #region GMTOffset (INotifyPropertyChanged Property)
        public double GMTOffset
        {
            get { return _gMTOffset; }
            set { SetProperty(ref _gMTOffset, value); }
        }
        private double _gMTOffset;
        #endregion

        #region RunwayLengthFeet (INotifyPropertyChanged Property)
        public double RunwayLengthFeet
        {
            get { return _runwayLengthFeet; }
            set { SetProperty(ref _runwayLengthFeet, value); }
        }
        private double _runwayLengthFeet;
        #endregion

        #region RunwayElevationFeet (INotifyPropertyChanged Property)
        public double RunwayElevationFeet
        {
            get { return _runwayElevationFeet; }
            set { SetProperty(ref _runwayElevationFeet, value); }
        }
        private double _runwayElevationFeet;
        #endregion

        #region LatitudeDegree (INotifyPropertyChanged Property)
        public double LatitudeDegree
        {
            get { return _latitudeDegree; }
            set { SetProperty(ref _latitudeDegree, value); }
        }
        private double _latitudeDegree;
        #endregion

        #region LatitudeMinute (INotifyPropertyChanged Property)
        public double LatitudeMinute
        {
            get { return _latitudeMinute; }
            set { SetProperty(ref _latitudeMinute, value); }
        }
        private double _latitudeMinute;
        #endregion

        #region LatitudeSecond (INotifyPropertyChanged Property)
        public double LatitudeSecond
        {
            get { return _latitudeSecond; }
            set { SetProperty(ref _latitudeSecond, value); }
        }
        private double _latitudeSecond;
        #endregion

        #region LatitudeNpeerS (INotifyPropertyChanged Property)
        public string LatitudeNpeerS
        {
            get { return _latitudeNpeerS; }
            set { SetProperty(ref _latitudeNpeerS, value); }
        }
        private string _latitudeNpeerS;
        #endregion

        #region LongitudeDegree (INotifyPropertyChanged Property)
        public double LongitudeDegree
        {
            get { return _longitudeDegree; }
            set { SetProperty(ref _longitudeDegree, value); }
        }
        private double _longitudeDegree;
        #endregion

        #region LongitudeMinute (INotifyPropertyChanged Property)
        public double LongitudeMinute
        {
            get { return _longitudeMinute; }
            set { SetProperty(ref _longitudeMinute, value); }
        }
        private double _longitudeMinute;
        #endregion

        #region LongitudeSeconds (INotifyPropertyChanged Property)
        public double LongitudeSeconds
        {
            get { return _longitudeSeconds; }
            set { SetProperty(ref _longitudeSeconds, value); }
        }
        private double _longitudeSeconds;
        #endregion

        #region LongitudeEperW (INotifyPropertyChanged Property)
        public string LongitudeEperW
        {
            get { return _longitudeEperW; }
            set { SetProperty(ref _longitudeEperW, value); }
        }
        private string _longitudeEperW;
        #endregion

        #region MetaData (INotifyPropertyChanged Property)
        public object MetaData
        {
            get { return _metaData; }
            set { SetProperty(ref _metaData, value); }
        }
        private object _metaData;
        #endregion

        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            //Decimal degrees = 
            //   whole number of degrees, 
            //   plus minutes divided by 60, 
            //   plus seconds divided by 3600

            return degrees + (minutes / 60) + (seconds / 3600);
        }

        public System.Device.Location.GeoCoordinate Coordinate { get; set; }
        public double Distance { get; set; }
        #region ScaleFactor (INotifyPropertyChanged Property)
        public double ScaleFactor
        {
            get { return _scaleFactor; }
            set { SetProperty(ref _scaleFactor, value); }
        }
        private double _scaleFactor;
        #endregion
        
    }
}
