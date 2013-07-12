using HomeWork2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEx
{
    public class NoteEditPageViewModel : BindableBase
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

        private int _noteId = -1;

        internal void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e, System.Windows.Navigation.NavigationContext navigationContext, System.Windows.Navigation.NavigationService navigationService)
        {
            _noteId = int.Parse(navigationContext.QueryString["noteId"]);
            if (_noteId == -1)
            {
                SelectedNote = new Note();
            }
            else
            {
                SelectedNote = Notes[_noteId];
            }
        }

        internal void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e, System.Windows.Navigation.NavigationContext navigationContext, System.Windows.Navigation.NavigationService navigationService)
        {
            if (_noteId == -1)
            {
                Notes.Add(SelectedNote);
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

    }
}
