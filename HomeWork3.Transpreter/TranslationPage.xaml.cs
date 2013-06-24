using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HomeWork3
{
    public partial class TranslationPage : PhoneApplicationPage
    {

        public TranslationPageViewModel View { get { return DataContext as TranslationPageViewModel; } }

        public TranslationPage()
        {
            InitializeComponent();
            Loaded += OnTranslationPageLoaded;
        }

        private void OnTranslationPageLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnTranslationPageLoaded;
            View.ViewLoadedCommand.Execute(null);
        }
    }
}