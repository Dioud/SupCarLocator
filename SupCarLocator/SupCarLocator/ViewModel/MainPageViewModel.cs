using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupCarLocator.Model;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Microsoft.Phone.Tasks;
using System.Net;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Devices.Sensors;

namespace SupCarLocator.ViewModel
{
    class MainPageViewModel : PageViewModelBase
    {
        Accelerometer accelerometer;
        private GeoCoordinate _center;
        public GeoCoordinate center
        {
            get { return _center; }
            set
            {
                _center = value;
                RaisePropertyChanged("center");
            }
        }


        public MainPageViewModel()
        {
            var geoService = new GeolocalisationServices();
            if (geoService.GetGeolocalisations().Count != 0)
            {
                Geolocalisation tttt = geoService.GetLastGeo();
                float lat = Convert.ToSingle(tttt.latitude);
                float longitude = Convert.ToSingle(tttt.longitude);
                center = new GeoCoordinate(lat, longitude);
            }
            else
            {
                center = new GeoCoordinate(0, 0);
            }

            if (accelerometer == null)
            {

                accelerometer = new Accelerometer();
                accelerometer.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(accelerometer_ReadingChanged);
                accelerometer.Start();
            }
        }
        void accelerometer_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyReadingChanged(e));
        }

        void MyReadingChanged(AccelerometerReadingEventArgs e)
        {
            if (accelerometer != null)
            {
                var geoService = new GeolocalisationServices();
                if (e.Z > 0.95)
                {
                    add();
                    accelerometer.Stop();
                    accelerometer = null;
                }
            }
        }
        public async void add()
        {
            var geoService = new GeolocalisationServices();
            if (geoService.GetGeolocalisations().Count != 0)
            {
                result = MessageBox.Show("Delete current position ?", "Confirmation", MessageBoxButton.OKCancel);
            }

            if (result == MessageBoxResult.OK || geoService.GetGeolocalisations().Count == 0)
            {

                Geolocator geolocator = new Geolocator();
                geolocator.DesiredAccuracyInMeters = 50;
                
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromSeconds(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                string latitude = geoposition.Coordinate.Latitude.ToString();
                string longitude = geoposition.Coordinate.Longitude.ToString();

                Geolocalisation geodata = new Geolocalisation();

                geodata.latitude = latitude;
                geodata.longitude = longitude;

                geoService.AddGeo(geodata);

                Geolocalisation tttt = geoService.GetLastGeo();
                center = new GeoCoordinate(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);

            }
        }

        public void delete()
        {
            var geoService = new GeolocalisationServices();

            if (geoService.GetGeolocalisations().Count != 0)
            {
                result = MessageBox.Show("Delete current position ?", "Confirmation", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    geoService.DeleteGeo();
                    center = new GeoCoordinate(0, 0);
                }
            }
        }
        public void gotoposition()
        {
            var geoService = new GeolocalisationServices();
            if (geoService.GetGeolocalisations().Count != 0)
            {
                BingMapsTask bingMapsTask = new BingMapsTask();
                Geolocalisation tttt = geoService.GetLastGeo();
                float lat = Convert.ToSingle(tttt.latitude);
                float longitude = Convert.ToSingle(tttt.longitude);

                bingMapsTask.Center = new GeoCoordinate(lat, longitude);
                bingMapsTask.ZoomLevel = 2;
                bingMapsTask.Show();
            }

        }

        public void accesgps()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                return;
            }
            else
            {
                var result = MessageBox.Show("This app accesses your phone's location. Is that ok ?", "Confirmation", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }

        }

        public MessageBoxResult result { get; set; }
    }
}
