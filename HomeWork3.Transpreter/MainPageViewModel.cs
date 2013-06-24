using HomeWork2.Interactivity;
using HomeWork2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Phone.Speech.Synthesis;

namespace HomeWork3
{
    public class MainPageViewModel : BindableBase
    {

        public ICommand ViewLoadedCommand { get; private set; }

        public MainPageViewModel()
        {
            ViewLoadedCommand = new RelayCommand(OnViewLoadedCommandInvoked);
        }

        private void OnViewLoadedCommandInvoked()
        {
            Say("I like cheese.");
        }

        private async void Say(string text)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            await synth.SpeakTextAsync(text);

            //// Query for a voice that speaks French. 
            //var frenchVoices = from voice in InstalledVoices.All 
            //                   where voice.Language == "fr-FR" 
            //                   select voice; 
            //// Set the voice as identified by the query. 
            //synth.SetVoice(frenchVoices.ElementAt(0));
        }
    }
}
