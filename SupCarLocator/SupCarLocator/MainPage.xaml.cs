using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SupCarLocator.Model;
using SupCarLocator.ViewModel;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
namespace SupCarLocator
{
    public partial class MainPage : PhoneApplicationPage
    {
        MainPageViewModel _viewModel;


        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            this.DataContext = _viewModel;

        }

        private void Button_Tap_1(object sender, RoutedEventArgs e)
        {
            _viewModel.add();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _viewModel.accesgps();
        }

        private void deletebutton(object sender, RoutedEventArgs e)
        {
            _viewModel.delete();
        }

        private void gotobutton(object sender, RoutedEventArgs e)
        {
            _viewModel.gotoposition();
        }
    }
}