using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace AvsSampleForms
{
    public partial class HomePage : ContentPage
    {
        const string ProductId = "xamarinsample";

        HomeViewModel viewModel;

        public HomePage ()
        {
            InitializeComponent ();

            viewModel = new HomeViewModel (ProductId);

            BindingContext = viewModel;

            // Detect state changes to manipulate animations
            viewModel.PropertyChanged += (sender, e) => {
                if (e.PropertyName == "SpeechState") {
                    switch (viewModel.SpeechState) {
                    case HomeViewModel.SpeechRecognizerState.Idle:
                        doAnimateRotate = false;
                        doAnimateSpeech = false;
                        buttonRecord.Text = "RECORD";
                        buttonRecord.IsEnabled = true;
                        buttonRecord.BackgroundColor = Color.FromHex ("dc2338");
                        break;
                    case HomeViewModel.SpeechRecognizerState.Playing:
                        doAnimateRotate = false;
                        doAnimateSpeech = false;
                        animateSpeech ();
                        buttonRecord.Text = "... playing ...";
                        buttonRecord.IsEnabled = false;
                        buttonRecord.BackgroundColor = Color.FromHex ("0080ff");
                        break;
                    case HomeViewModel.SpeechRecognizerState.Recording:
                        doAnimateRotate = false;
                        doAnimateSpeech = false;
                        animateSpeech ();
                        buttonRecord.Text = "... recording ...";
                        buttonRecord.IsEnabled = true;
                        buttonRecord.BackgroundColor = Color.FromHex ("dc2338");
                        break;
                    case HomeViewModel.SpeechRecognizerState.WebRequest:
                        doAnimateRotate = false;
                        doAnimateSpeech = false;
                        animateRotate ();
                        buttonRecord.Text = "... reticulating splines ...";
                        buttonRecord.IsEnabled = false;
                        buttonRecord.BackgroundColor = Color.FromHex ("0080ff");
                        break;
                    }
                }
            };

            viewModel.SpeechState = HomeViewModel.SpeechRecognizerState.Idle;
        }

        bool doAnimateRotate = false;
        bool doAnimateSpeech = false;
        async Task animateSpeech ()
        {
            if (doAnimateSpeech)
                return;
            
            doAnimateSpeech = true;

            var rnd = new Random ();

            while (doAnimateSpeech) {
                var timeVariation = rnd.Next (-50, 50);
                var sizeVariation = rnd.NextDouble () * 0.7;

                await shape.ScaleTo (sizeVariation + 1.1, 100 + (uint)timeVariation, Easing.SpringIn);

                timeVariation = rnd.Next (-50, 50);
                await shape.ScaleTo (1.0, 100 + (uint)timeVariation, Easing.SpringOut);
            }
        }

        async Task animateRotate ()
        {
            if (doAnimateRotate)
                return;

            doAnimateRotate = true;
            while (doAnimateRotate) {
                //await shape.RotateYTo (180, 200, Easing.CubicIn);
                await shape.RotateYTo (360, 1000, Easing.CubicIn);
                await shape.RotateYTo (0, 1000, Easing.CubicIn);
            }
        }

        protected override async void OnAppearing ()
        {
            base.OnAppearing ();

            // Attempt login every time the page comes up
            // The library will handle this transparently if we're already logged in
            // and refresh our auth token if need be, or show the login dialog if needed
            await viewModel.Login ();
        }
    }
}

