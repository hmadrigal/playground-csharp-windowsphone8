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
using Microsoft.Phone.Scheduler;

namespace HomeWork3
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPageViewModel View { get { return DataContext as MainPageViewModel; } }
        private string periodicTaskName = @"CycleTileScheduledAgent";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
#if !DEBUG
            ContentPanel.Children.Remove(StartBackgroundWork);   
#endif
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            View.LoadCommand.Execute(null);
            StartPeriodicAgent();
        }

        private void StartPeriodicAgent()
        {
            // Obtain a reference to the period task, if one exists
            var periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule
            if (periodicTask != null)
            {
                try
                {
                    ScheduledActionService.Remove(periodicTaskName);
                }
                catch (Exception)
                {
                }
            }

            periodicTask = new PeriodicTask(periodicTaskName);


            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            periodicTask.Description = "This demonstrates a periodic task.";

            // Place the call to Add in a try block in case the user has disabled agents
            try
            {
                ScheduledActionService.Add(periodicTask);
                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
                //#if DEBUG
                //                ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(60));
                //#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by an internal error.");
                }
                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }

        }

        private void OnStartBackgroundWorkClicked(object sender, RoutedEventArgs e)
        {
            var foo = Microsoft.Phone.Scheduler.ScheduledActionService.Find(periodicTaskName);
            Microsoft.Phone.Scheduler.ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(10));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}