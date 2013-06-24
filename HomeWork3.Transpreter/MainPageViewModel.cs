using HomeWork2.Interactivity;
using HomeWork2.ViewModels;
using HomeWork3.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Speech.VoiceCommands;

namespace HomeWork3
{
    public class MainPageViewModel : BindableBase
    {
        #region Source (INotifyPropertyChanged Property)
        public string Source
        {
            get { return _myProperty; }
            set { SetProperty(ref _myProperty, value); }
        }
        private string _myProperty;
        #endregion

        #region Destination (INotifyPropertyChanged Property)
        public string Destination
        {
            get { return _destination; }
            set { SetProperty(ref _destination, value); }
        }
        private string _destination;
        #endregion
        
        public ICommand TurnOnSpeechRecognizerUICommand { get; private set; }

        public ICommand ViewLoadedCommand { get; private set; }

        public ICommand TurnOnSpeechSynthesizerCommand { get; private set; }

        private SpeechRecognizerUI _speechRecognizerUI;

        public MainPageViewModel()
        {
            SetupVoiceCommands();
            ViewLoadedCommand = new RelayCommand(OnViewLoadedCommandInvoked);
            TurnOnSpeechRecognizerUICommand = new RelayCommand(OnTurnOnSpeechRecognizerUICommandInvoked);
            TurnOnSpeechSynthesizerCommand = new RelayCommand<string>(OnTurnOnSpeechSynthesizerCommandInvoked);
        }

        private async void OnTurnOnSpeechSynthesizerCommandInvoked(string text)
        {
            await Say(text);
        }

        private async void OnTurnOnSpeechRecognizerUICommandInvoked()
        {
            _speechRecognizerUI = new SpeechRecognizerUI();
            SpeechRecognitionUIResult recoResult = await _speechRecognizerUI.RecognizeWithUIAsync();
            if (recoResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
            {
                Source = recoResult.RecognitionResult.Text;
            }
        }


        private async void OnViewLoadedCommandInvoked()
        {
            await Say(AppResources.MainPage_WelcomeMessage);
            OnTurnOnSpeechRecognizerUICommandInvoked();
        }

        private async void SetupVoiceCommands()
        {
            await VoiceCommandService.InstallCommandSetsFromFileAsync(new Uri("ms-appx:///VCD.xml", UriKind.RelativeOrAbsolute));
        }



        private async Task Say(string text)
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
