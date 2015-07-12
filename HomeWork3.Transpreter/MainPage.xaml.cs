using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace HomeWork3.Transpreter
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            StartTranslation();
        }

        private void StartTranslation()
        {
            Task.Run(async () =>
            {
                await SpeechHelper.Instance.Say("Di lo que quieres traducir!", SpeechHelper.Languages.esES);
                var listenResult = await SpeechHelper.Instance.Listen();
                var text = listenResult.RecognitionResult.Text;

                var tranlateUrl = new Uri(string.Format(@"http://api.apertium.org/json/translate?q={0}&langpair=es%7Cen", HttpUtility.UrlEncode(text)));
                var wc = new WebClient();
                wc.OpenReadCompleted += OnOpenReadCompleted;
                wc.OpenReadAsync(tranlateUrl);

                Dispatcher.BeginInvoke(() =>
                {
                    progressBar.IsIndeterminate = true;
                    inputText.Text = text;
                });
            });
        }

        private void OnOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            (sender as WebClient).OpenReadCompleted -= OnOpenReadCompleted;
            if (e.Error != null)
            {
                MessageBox.Show("There has been an error getting the translation.\n" + e.Error.Message);
                return;
            }
            var serializer = new DataContractJsonSerializer(typeof(TranslateResponse));
            var translateResponse = (TranslateResponse)serializer.ReadObject(e.Result);
            var text = translateResponse.responseData.translatedText;
            Dispatcher.BeginInvoke(() =>
            {
                outputText.Text = text;
                progressBar.IsIndeterminate = false;
            });

            Task.Run(async () =>
            {
                await SpeechHelper.Instance.Say(text, SpeechHelper.Languages.enUS);
            });

        }

        private void OnStartClicked(object sender, RoutedEventArgs e)
        {
            StartTranslation();
        }



    }
}