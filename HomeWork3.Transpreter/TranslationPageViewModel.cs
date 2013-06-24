using HomeWork2.Interactivity;
using HomeWork2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork3
{
    public class TranslationPageViewModel : BindableBase
    {
        public ICommand ViewLoadedCommand { get; private set; }

        public TranslationPageViewModel()
        {
            ViewLoadedCommand = new RelayCommand(OnViewLoadedCommandInvoked);
        }

        private void OnViewLoadedCommandInvoked()
        {
        }

    }
}
