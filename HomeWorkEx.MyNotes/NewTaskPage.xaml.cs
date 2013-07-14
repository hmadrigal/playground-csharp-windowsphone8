/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

// Directive for the data model.
using LocalDatabaseSample.Model;

namespace sdkLocalDatabaseCS
{
    public partial class NewTaskPage : PhoneApplicationPage
    {
        public NewTaskPage()
        {
            InitializeComponent();

            // Set the page DataContext property to the ViewModel.
            this.DataContext = App.ViewModel;
            this.foregroundPicker.ItemsSource = App.ViewModel.Colors;
            this.backgroundPicker.ItemsSource = App.ViewModel.Colors;
        }

        //private void appBarOkButton_Click(object sender, EventArgs e)
        //{
        //    // Confirm there is some text in the text box.
        //    if (newTaskNameTextBox.Text.Length > 0)
        //    {
        //        // Create a new to-do item.
        //        ToDoItem newToDoItem = new ToDoItem
        //        {
        //            ItemName = newTaskNameTextBox.Text,
        //            Category = (ToDoCategory)categoriesListPicker.SelectedItem
        //        };

        //        // Add the item to the ViewModel.
        //        App.ViewModel.AddToDoItem(newToDoItem);

        //        // Return to the main page.
        //        if (NavigationService.CanGoBack)
        //        {
        //            NavigationService.GoBack();
        //        }
        //    }
        //}

        private int currentId = int.MinValue;

        //private void appBarCancelButton_Click(object sender, EventArgs e)
        //{
        //    // Return to the main page.
        //    if (NavigationService.CanGoBack)
        //    {
        //        NavigationService.GoBack();
        //    }
        //}

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                if (!int.TryParse(NavigationContext.QueryString["id"], out currentId))
                {
                    currentId = int.MinValue;
                }
                else
                {
                    var todo = App.ViewModel.AllToDoItems.FirstOrDefault(t => t.ToDoItemId == currentId);
                    newTaskNameTextBox.Text = todo.ItemName;
                    this.foregroundPicker.SelectedItem = todo.Foreground;
                    this.backgroundPicker.SelectedItem = todo.Background;
                }
            }
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (currentId < 0)
            {
                // Create a new to-do item.
                ToDoItem newToDoItem = new ToDoItem
                {
                    ItemName = newTaskNameTextBox.Text,
                    Foreground = this.foregroundPicker.SelectedItem.ToString(),
                    Background = this.backgroundPicker.SelectedItem.ToString(),
                    Category = (ToDoCategory)categoriesListPicker.SelectedItem
                };

                // Add the item to the ViewModel.
                App.ViewModel.AddToDoItem(newToDoItem);
            }
            else
            {
                var todo = App.ViewModel.AllToDoItems.FirstOrDefault(t => t.ToDoItemId == currentId);
                todo.ItemName = newTaskNameTextBox.Text;
                todo.Foreground = this.foregroundPicker.SelectedItem.ToString();
                todo.Background = this.backgroundPicker.SelectedItem.ToString();
                App.ViewModel.SaveChangesToDB();
            }
        }
    }
}
