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
		Random rnd = new Random();

		CharacteristicManager cm;
		List<int> DiffuseArray = new List<int>{0, 0, 0};

		Guid Scartch_Service = new Guid ("a495ff20-c5b1-4b44-b512-1370f02d74de");
		Guid Scartch_Service_2 = new Guid ("a495ff22-c5b1-4b44-b512-1370f02d74de");
	
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
												bool diffused = cm.Detail.UpdateValue(cm.Detail.Characteristic, DiffuseArray);
												if (diffused == true){
													cm.Detail.OnDisappearing();
													adapter.DisconnectDevice (device);
													Navigation.PopToRootAsync();
												}

											});
										};

										cm.Detail.Characteristic.StartUpdates();

										DiffuseArray[0] = rnd.Next(2);
										DiffuseArray[1] = rnd.Next(2);
										DiffuseArray[2] = rnd.Next(2);
										Debug.WriteLine("Diffuse Red: " + DiffuseArray[0].ToString());
										Debug.WriteLine("Diffuse Brown: " + DiffuseArray[1].ToString());
										Debug.WriteLine("Diffuse Blue: " + DiffuseArray[2].ToString());
										bool is_cut_wire = false;
										if (DiffuseArray[0] == 1){
											red_wire_label.Text = red_wire_label.Text + "CUT";
											is_cut_wire = true;
										}
										if (DiffuseArray[1] == 1){
											brown_wire_label.Text = brown_wire_label.Text + "CUT";
											is_cut_wire = true;
										}
										if (DiffuseArray[2] == 1){
											blue_wire_label.Text = blue_wire_label.Text + "CUT";
											is_cut_wire = true;
										}
										if (is_cut_wire == false){
											DiffuseArray[0] = 1;
											red_wire_label.Text = red_wire_label.Text + "CUT";
											//if none are picked for cut wire, default to red needing to be cut
										}

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
		public void DeactivateBomb(){

		}
	}
}

