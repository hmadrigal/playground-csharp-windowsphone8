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
        #region ListenText (INotifyPropertyChanged Property)
        public string ListenText
        {
            get { return _listenText; }
            set { SetProperty(ref _listenText, value); }
        }
        private string _listenText;
        #endregion

        private SpeechRecognizerUI _speechRecognizerUI;

        public MainPageViewModel()
        {
            SetupVoiceCommands();
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

        internal async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Asks the user to say something
            await Say(AppResources.MainPage_WelcomeMessage);

            // Waits the user to say something, until the app recognizes what the user says
            SpeechRecognitionUIResult speechRecognitionResult = await Listen();

            // Shows what the user 
            ListenText = speechRecognitionResult.RecognitionResult.Text;

            // Asks the user to say: continue or change
            await Say(AppResources.MainPage_ConfirmMessage);
        }

        private async Task<SpeechRecognitionUIResult> Listen(SpeechRecognitionConfidence? confidence = null)
        {
            SpeechRecognitionUIResult speechRecognitionResult;
            do
            {
                _speechRecognizerUI = new SpeechRecognizerUI();
                speechRecognitionResult = await _speechRecognizerUI.RecognizeWithUIAsync();
            } while ((speechRecognitionResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
            && (confidence == null ? true : confidence.Value == speechRecognitionResult.RecognitionResult.TextConfidence));
            return speechRecognitionResult;
        }
    }
}
