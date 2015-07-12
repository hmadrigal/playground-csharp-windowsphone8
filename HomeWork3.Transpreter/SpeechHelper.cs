using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;

namespace HomeWork3
{

    public sealed class SpeechHelper
    {

        private SpeechRecognizerUI _speechRecognizerUI;

        public static class Languages
        {
            public static readonly string esES = "es-ES";
            public static readonly string enUS = "en-US";
        }

        public async Task Say(string text, string filterLanguage = null)
        {
            // Query for a voice that speaks French.
            filterLanguage = filterLanguage ?? Languages.esES;
            IEnumerable<VoiceInformation> voices = from voice in InstalledVoices.All
                                                   where voice.Language == filterLanguage
                                                   select voice;

            // Set the voice as identified by the query.
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetVoice(voices.ElementAt(0));
            await synth.SpeakTextAsync(text);

        }

        public async Task<SpeechRecognitionUIResult> Listen()
        {
            SpeechRecognitionUIResult speechRecognitionResult;
            _speechRecognizerUI = new SpeechRecognizerUI();
            speechRecognitionResult = await _speechRecognizerUI.RecognizeWithUIAsync();
            return speechRecognitionResult;
        }

        private void InitializeHelper()
        {

        }

        #region Singleton Pattern w/ Constructor
        private SpeechHelper()
            : base()
        {
            InitializeHelper();
        }
        public static SpeechHelper Instance
        {
            get
            {
                return SingletonHelperCreator._Instance;
            }
        }
        private class SingletonHelperCreator
        {
            private SingletonHelperCreator() { }
            public static SpeechHelper _Instance = new SpeechHelper();
        }
        #endregion
    }

}
