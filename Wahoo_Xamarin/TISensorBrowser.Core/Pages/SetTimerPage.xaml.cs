using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;


namespace TISensorBrowser
{
	public partial class SetTimerPage : ContentPage
	{
		DateTime triggerTime;

		IAdapter adapter;

		String deviceName = "TICKR AACC";

		
		public SetTimerPage (IAdapter adapter)
		{
			InitializeComponent(); 
			Device.StartTimer( TimeSpan.FromSeconds(1), OnTimerTick); 

				this.adapter = adapter;
				
				

				adapter.DeviceDiscovered += (object sender, DeviceDiscoveredEventArgs e) => {
				if (e.Device.Name == deviceName){
						toService(sender, e.Device);
					}
				};

				adapter.ScanTimeoutElapsed += (sender, e) => {
					adapter.StopScanningForDevices(); // not sure why it doesn't stop already, if the timeout elapses... or is this a fake timeout we made?
					Device.BeginInvokeOnMainThread ( () => {
						IsBusy = false;
						DisplayAlert("Timeout", "Bluetooth scan timeout elapsed", "OK");
					});
				};


//				StartScanning();
		} 
		bool OnTimerTick() {

			if (t_switch.IsToggled && DateTime.Now >= triggerTime) 
			{ 
				t_switch.IsToggled = false ; 

				StartScanning();
			} 
			return true ; 
		} 
		void onTestClick( object obj, EventArgs args) {
			
			StartScanning ();
		} 
		void OnTimePickerPropertyChanged( object obj, PropertyChangedEventArgs args)
		{ if (args.PropertyName == "Time" ) {
				SetTriggerTime(); 
			} 
		} 
		void OnSwitchToggled( object obj, ToggledEventArgs args) {
			SetTriggerTime();
		} 
		void SetTriggerTime() { 
			if (t_switch.IsToggled) { 
				triggerTime = DateTime.Today + timePicker.Time; 
				if (triggerTime < DateTime.Now) { 
					triggerTime += TimeSpan.FromDays(1); 
				} 
			} 
		} 
		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {


		}
		public void toService(object sender, IDevice device){
			IsBusy = false;
			StopScanning ();
			var servicePage = new ServiceList(adapter, device);


			// load services on the next page
			Navigation.PushAsync(servicePage);


		}

		void StartScanning () {
			IsBusy = true;
			StartScanning (Guid.Empty);
		}
		void StartScanning (Guid forService) {

			if (adapter.IsScanning) {
				IsBusy = false;
				adapter.StopScanningForDevices();
				Debug.WriteLine ("StartScanning > adapter.StopScanningForDevices()");
			} else {
				
				IsBusy = true;
				adapter.StartScanningForDevices(forService);
				Debug.WriteLine ("adapter.StartScanningForDevices("+forService+")");
			}
		}

		void StopScanning () {
			// stop scanning
			new Task( () => {
				if(adapter.IsScanning) {
					IsBusy = false;
					Debug.WriteLine ("Still scanning, stopping the scan");
					adapter.StopScanningForDevices();
				}
			}).Start();
		}
	}

}
