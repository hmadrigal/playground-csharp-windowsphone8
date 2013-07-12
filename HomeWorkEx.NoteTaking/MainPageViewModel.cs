﻿using HomeWork2.Interactivity;
using HomeWork2.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeWorkEx
{
    public class MainPageViewModel : BindableBase
    {
        #region SelectedNote (INotifyPropertyChanged Property)
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set { SetProperty(ref _selectedNote, value); }
        }
        private Note _selectedNote;
        #endregion

        public ObservableCollection<Note> Notes
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("_notes_"))
                {
                    return IsolatedStorageSettings.ApplicationSettings["_notes_"] as ObservableCollection<Note>;
                }
                return new ObservableCollection<Note>();
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["_notes_"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public ICommand AddCommandInvoked { get; private set; }

        public MainPageViewModel()
        {
            Notes = new ObservableCollection<Note>();
            AddCommandInvoked = new RelayCommand(OnAddCommandInvokedInvoked);
        }

        private void OnAddCommandInvokedInvoked()
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/NoteEditPage.xaml?noteId=-1", UriKind.RelativeOrAbsolute));
        }
    }
}
