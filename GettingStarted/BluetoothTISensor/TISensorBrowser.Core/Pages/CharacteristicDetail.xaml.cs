using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Diagnostics;
using System.Linq;

namespace TISensorBrowser
{	
	public partial class CharacteristicDetail : ContentPage
	{	
		IAdapter adapter;
		IDevice device;
		IService service; 
		ICharacteristic characteristic;

		public CharacteristicDetail (IAdapter adapter, IDevice device, IService service, ICharacteristic characteristic)
		{
			InitializeComponent ();
			this.characteristic = characteristic;

			if (characteristic.CanUpdate) {
				characteristic.ValueUpdated += (s, e) => {
					Debug.WriteLine("characteristic.ValueUpdated");
					Device.BeginInvokeOnMainThread( () => {
						UpdateDisplay(characteristic);
					});
				};
				characteristic.StartUpdates();
			}
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
		
			if (characteristic.CanRead) {
				var c = await characteristic.ReadAsync();
				UpdateDisplay(c);
			}
		}

		protected override void OnDisappearing() 
		{
			base.OnDisappearing();
			if (characteristic.CanUpdate) {
				characteristic.StopUpdates();
			}
		}
		void UpdateDisplay (ICharacteristic c) {
			Name.Text = c.Name;
			//ID.Text = c.ID.ToString();
			ID.Text = c.ID.PartialFromUuid ();
			string hex = BitConverter.ToString(c.Value);
			//var s = (from i in c.Value where i == 0 select );
				//select i.ToString ()); //i.ToString ("X"));
			List<string> numbers = hex.Split('-').ToList<string>();
			string resultstring = "";
			int counter = 0;

			foreach (string number  in numbers) {
				counter++;
				int temp = int.Parse(number, System.Globalization.NumberStyles.HexNumber);
				if (counter == 1) { //only grab first value, can be changed to get more values
					resultstring = temp.ToString ();
				}

			}
			RawValue.Text = resultstring; 

			StringValue.Text = c.StringValue;
			StringValue.TextColor = Color.Default;
		}
	}
}

