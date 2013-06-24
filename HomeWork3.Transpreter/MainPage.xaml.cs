using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HomeWork3.Resources;

namespace HomeWork3.Transpreter
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPageViewModel View { get { return DataContext as MainPageViewModel; } }

        public MainPage()
        {
            InitializeComponent();
            Loaded += OnMainPageLoaded;

        }

        private void OnMainPageLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnMainPageLoaded;
            View.ViewLoadedCommand.Execute(null);
        }

    }
}