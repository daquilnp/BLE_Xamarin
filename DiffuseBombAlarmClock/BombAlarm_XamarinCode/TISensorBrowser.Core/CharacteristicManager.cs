using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TISensorBrowser
{	
	public class CharacteristicManager
	{	
		public IAdapter adapter;
		public IDevice device;
		public IService service; 
		public ICharacteristic Characteristic; 
		public CharacteristicDetails Detail;

		ObservableCollection<ICharacteristic> characteristics;

		public CharacteristicManager (IAdapter adapter, IDevice device, IService service, Guid Scratch_Service_Used)
		{
			
			this.adapter = adapter;
			this.device = device;
			this.service = service;
			this.characteristics = new ObservableCollection<ICharacteristic> ();



			// when characteristics are discovered
			service.CharacteristicsDiscovered += (object sender, EventArgs e) => {
				Debug.WriteLine("service.CharacteristicsDiscovered");
				if (characteristics.Count == 0)
					Device.BeginInvokeOnMainThread(() => {
						foreach (var characteristic in service.Characteristics) {
							if (characteristic.ID == Scratch_Service_Used)
							{
								Debug.WriteLine("found characteristic");
								Characteristic = characteristic;
								Detail = new CharacteristicDetails (adapter, device, service, characteristic);
							}

						}
					});
			};

			// start looking for characteristics
			service.DiscoverCharacteristics ();
		}

	}
}


