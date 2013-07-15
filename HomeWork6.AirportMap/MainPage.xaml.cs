using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HomeWork6.AirportMap.Resources;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;

namespace HomeWork6.AirportMap
{
    public partial class MainPage : PhoneApplicationPage
    {

        private GeoCoordinate MyCoordinate = null;

        private double _accuracy = 0.0;

        List<MapTableItem> _mapItems;

        public MainPage()
        {
            InitializeComponent();
            _mapItems = new List<MapTableItem>();
            var mapLayer = new MapLayer();
            mapView.Layers.Add(mapLayer);
        }

        private void wsc_GetAirportInformationByCountryCompleted(object sender, WebServiceX.AirportServiceReference.GetAirportInformationByCountryCompletedEventArgs e)
        {
            (sender as WebServiceX.AirportServiceReference.airportSoapClient).GetAirportInformationByCountryCompleted -= wsc_GetAirportInformationByCountryCompleted;
            if (e.Error != null)
            {
                MessageBox.Show("There has been an error calling the Web Service.\n" + e.Error.Message);
                return;
            }

            Task.Run(() =>
            {
                // Gets and draws the current location
                GetCurrentCoordinate();
            });

            Task.Run(() =>
            {
                var countryFlagUrl = AppConfig.Instance.CountryFlagUrl;
                var document = XDocument.Parse(e.Result);
                var mapItems = (from tableElement in document.Root.Elements("Table")
                                select new MapTableItem()
                                {
                                    AirportCode = (string)tableElement.Element("AirportCode"),
                                    CityOrAirportName = (string)tableElement.Element("CityOrAirportName"),
                                    Country = (string)tableElement.Element("Country"),
                                    CountryAbbrviation = (string)tableElement.Element("CountryAbbrviation"),
                                    CountryCode = double.Parse((string)tableElement.Element("CountryCode") ?? "0"),
                                    GMTOffset = double.Parse((string)tableElement.Element("GMTOffset") ?? "0"),
                                    RunwayLengthFeet = double.Parse((string)tableElement.Element("RunwayLengthFeet") ?? "0"),
                                    RunwayElevationFeet = double.Parse((string)tableElement.Element("RunwayElevationFeet") ?? "0"),
                                    LatitudeDegree = double.Parse((string)tableElement.Element("LatitudeDegree") ?? "0"),
                                    LatitudeMinute = double.Parse((string)tableElement.Element("LatitudeMinute") ?? "0"),
                                    LatitudeSecond = double.Parse((string)tableElement.Element("LatitudeSecond") ?? "0"),
                                    LatitudeNpeerS = (string)tableElement.Element("LatitudeNpeerS"),
                                    LongitudeDegree = double.Parse((string)tableElement.Element("LongitudeDegree") ?? "0"),
                                    LongitudeMinute = double.Parse((string)tableElement.Element("LongitudeMinute") ?? "0"),
                                    LongitudeSeconds = double.Parse((string)tableElement.Element("LongitudeSeconds") ?? "0"),
                                    LongitudeEperW = (string)tableElement.Element("LongitudeEperW"),
                                    MetaData = countryFlagUrl,
                                }).Take(25).ToArray();

                _mapItems.Clear();
                _mapItems.AddRange(mapItems);
                //Creating a MapOverlay and adding the Grid to it.
                Dispatcher.BeginInvoke(() =>
                {
                    mapView.Layers[0].Clear();
                    for (int i = 0; i < mapItems.Length; i++)
                    {
                        //var overlay = DrawAirportMarker(mapItems[i], mapLayer);
                        //mapLayer.Add(overlay);
                        var gc = new GeoCoordinate(
                            (mapItems[i].LatitudeNpeerS == "N" ? 1.0 : -1.0) * MapTableItem.ConvertDegreeAngleToDouble(mapItems[i].LatitudeDegree, mapItems[i].LatitudeMinute, mapItems[i].LatitudeSecond),
                            (mapItems[i].LongitudeEperW == "E" ? 1.0 : -1.0) * MapTableItem.ConvertDegreeAngleToDouble(mapItems[i].LongitudeDegree, mapItems[i].LongitudeMinute, mapItems[i].LongitudeSeconds));
                        mapItems[i].Coordinate = gc;
                        DrawCurrentLocation(mapItems[i], gc, mapView.Layers[0], mapView, "AirportMarkerStyle");
                    }
                });
            });

        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ConfigPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void GetCurrentCoordinate()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;

            try
            {
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1),
                                                                                   TimeSpan.FromSeconds(10));
                _accuracy = currentPosition.Coordinate.Accuracy;
                MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Latitude, currentPosition.Coordinate.Longitude);
                Dispatcher.BeginInvoke(() =>
                {
                    DrawCurrentLocation(new MapTableItem()
                    {
                        MetaData = new System.Windows.Media.SolidColorBrush(GetColorBrush(AppConfig.Instance.Color)),
                        LatitudeDegree = MyCoordinate.Latitude,
                        LongitudeDegree = MyCoordinate.Longitude,
                    }, MyCoordinate, mapView.Layers[0], mapView, "MyLocationStyle");
                    // Sets the view pointing where the PushPin is.
                    mapView.SetView(MyCoordinate, 15, MapAnimationKind.Parabolic);
                });

                // Computes the sizes of the flags
                for (int i = 0; i < _mapItems.Count; i++)
                {
                    var mapItem = _mapItems[i];
                    if (mapItem.Coordinate == null)
                    {
                        mapItem.ScaleFactor = 1.0;
                        mapItem.Distance = 0;
                        continue;
                    }
                    //Microsoft.Xna.Framework.MathHelper.Lerp(
                    mapItem.Distance = distance(mapItem.Coordinate.Latitude, mapItem.Coordinate.Longitude, MyCoordinate.Latitude, MyCoordinate.Longitude, 'K');// mapItem.Coordinate.GetDistanceTo(MyCoordinate);
                }
                var minDist =_mapItems.Min(mi => mi.Distance);
                var maxDist = _mapItems.Max(mi => mi.Distance);
                if (minDist == 0 || maxDist == 0 || minDist == maxDist)
                    return;
                Dispatcher.BeginInvoke(() =>
                {
                    for (int i = 0; i < _mapItems.Count; i++)
                    {
                        var mapItem = _mapItems[i];
                        mapItem.ScaleFactor = (mapItem.Distance / maxDist);
                        mapItem.ScaleFactor = 1- (mapItem.ScaleFactor < 0.5d ? 0.5d : mapItem.ScaleFactor);
                    }
                });

            }
            catch
            {
                // Couldn't get current location - location might be disabled in settings
                MessageBox.Show("Current location cannot be obtained. Check that location service is turned on in phone settings.");
            }
        }

        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        private void DrawCurrentLocation(MapTableItem dc, GeoCoordinate gc, MapLayer mapLayer, Map myMap, string style)
        {
            var myControlMark = new ContentControl()
            {
                DataContext = dc,
                Style = Resources[style] as Style,
            };

            MapOverlay overlay = new MapOverlay();
            overlay.Content = myControlMark;
            overlay.GeoCoordinate = gc;
            overlay.PositionOrigin = new Point(0.5, 0.5);
            mapLayer.Add(overlay);
        }

        private System.Windows.Media.Color GetColorBrush(string p)
        {
            switch (p)
            {
                case "Black": return System.Windows.Media.Colors.Black; break;
                case "Blue": return System.Windows.Media.Colors.Blue; break;
                case "Brown": return System.Windows.Media.Colors.Brown; break;
                case "Cyan": return System.Windows.Media.Colors.Cyan; break;
                case "DarkGray": return System.Windows.Media.Colors.DarkGray; break;
                case "Gray": return System.Windows.Media.Colors.Gray; break;
                case "Green": return System.Windows.Media.Colors.Green; break;
                case "LightGray": return System.Windows.Media.Colors.LightGray; break;
                case "Magenta": return System.Windows.Media.Colors.Magenta; break;
                case "Orange": return System.Windows.Media.Colors.Orange; break;
                case "Purple": return System.Windows.Media.Colors.Purple; break;
                case "Red": return System.Windows.Media.Colors.Red; break;
                case "Transparent": return System.Windows.Media.Colors.Transparent; break;
                case "White": return System.Windows.Media.Colors.White; break;
                case "Yellow": return System.Windows.Media.Colors.Yellow; break;
                default:
                    return System.Windows.Media.Colors.White;
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            mapView.CartographicMode = AppConfig.Instance.CartographicMode;
            var wsc = new WebServiceX.AirportServiceReference.airportSoapClient();
            wsc.GetAirportInformationByCountryCompleted += wsc_GetAirportInformationByCountryCompleted;
            wsc.GetAirportInformationByCountryAsync(AppConfig.Instance.Country);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            AppConfig.Instance.Save();
        }
    }
}