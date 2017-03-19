using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TISensorBrowser
{	
	public partial class ServiceList : ContentPage
	{	
		IAdapter adapter;
		IDevice device;
		IService Service; 
	

		CharacteristicManager cm;


		Guid Scartch_Service = new Guid ("0000180d-0000-1000-8000-00805f9b34fb");
		Guid Scartch_Service_2 = new Guid ("00002a37-0000-1000-8000-00805f9b34fb");
		//Heart Rate: 0000180d-0000-8000-00805f9b34fb
		//Heart Rate Measurement: 0002a37-0000-1000-8000-00805f9b34fb
	
		bool foundScratchflag = false;

		public ServiceList (IAdapter adapter, IDevice device)
		{
			InitializeComponent ();
			this.adapter = adapter;
			this.device = device;


			// when device is connected
			adapter.DeviceConnected += (s, e) => {
				device = e.Device; // do we need to overwrite this?

				// when services are discovered
				device.ServicesDiscovered += (object se, EventArgs ea) => {
					if (foundScratchflag == false){
						
					Debug.WriteLine("device.ServicesDiscovered");
					
						if (foundScratchflag == false)
						Device.BeginInvokeOnMainThread(() => {

							foreach (IService service in device.Services) {
								if (service.ID == Scartch_Service && foundScratchflag == false){
										
									Service = service;
									Debug.WriteLine(Service.ID.ToString());
									foundScratchflag = true;

								}
							}
							if (foundScratchflag == true){
							
								cm = new CharacteristicManager(adapter, device, Service, Scartch_Service_2);
									if (cm.Detail.Characteristic.CanUpdate) {
										cm.Detail.Characteristic.ValueUpdated += (object sender, CharacteristicReadEventArgs eve) => {
											Debug.WriteLine("Characteristic.ValueUpdated");
											Device.BeginInvokeOnMainThread( () => {
												string heart_rate = cm.Detail.UpdateValue(cm.Detail.Characteristic);
												hr_label.Text = String.Format("HR: {0}",heart_rate) ;

											});
										};

										cm.Detail.Characteristic.StartUpdates();


									}
							}
							});
						Debug.WriteLine("Start Discovery");
						cm.service.DiscoverCharacteristics();

					}
				};
				Device.BeginInvokeOnMainThread (() => {
					IsBusy = false;
				});
				// start looking for services
			
				device.DiscoverServices ();

			};
				
			DisconnectButton.Activated += (sender, e) => {
				cm.Detail.OnDisappearing();
				adapter.DisconnectDevice (device);
				Navigation.PopToRootAsync();
			};
		}
			
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			if (foundScratchflag == false) {
				Debug.WriteLine ("No services, attempting to connect to device");
				IsBusy = true;
				// start looking for the device
				adapter.ConnectToDevice (device); 
			}
		}
		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {

		}

	}
}

