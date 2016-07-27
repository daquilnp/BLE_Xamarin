using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Avs;
using System.Threading.Tasks;
using Xamarin.Avs.Web.Response;
using System.Linq;

namespace AvsSampleForms
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public HomeViewModel (string productId)
        {
            alexa = new AlexaVoiceService (productId);
        }

        AlexaVoiceService alexa;
        CancellationTokenSource cancelTokenRecording;

        SpeechRecognizerState speechState = SpeechRecognizerState.Idle;
        public SpeechRecognizerState SpeechState {
            get { return speechState; }
            set {
                speechState = value;
                NotifyPropertyChanged ();
            }
        }

        public async Task Login ()
        {
            await alexa.Login ();
        }

        public ICommand ToggleSpeechRecognition {
            get {
                return new Command (toggleSpeechRecognition);
            }
        }

        void toggleSpeechRecognition ()
        {
            if (SpeechState == SpeechRecognizerState.Idle) {
                SpeechState = SpeechRecognizerState.Recording;

                Task.Factory.StartNew (async () => {
                    cancelTokenRecording = new CancellationTokenSource ();
                    var result = await alexa.RecognizeSpeech (cancelTokenRecording.Token);

                    if (result != null) {
                        foreach (var directive in result.MessageBody.Directives) {
                            if (directive.Name == DirectiveName.Speak) {
                                SpeechState = SpeechRecognizerState.Playing;
                                await alexa.Speak (result.Audio); 
                            }
                        }
                    }

                    SpeechState = SpeechRecognizerState.Idle;
                });
            } else {
                SpeechState = SpeechRecognizerState.WebRequest;
                if (!cancelTokenRecording.IsCancellationRequested)
                    cancelTokenRecording.Cancel ();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged ([CallerMemberName]string propertyName = null)
        {
            Device.BeginInvokeOnMainThread (() => 
                PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName)));
        }

        public enum SpeechRecognizerState
        {
            Idle,
            Recording,
            Playing,
            WebRequest
        }
    }
}

