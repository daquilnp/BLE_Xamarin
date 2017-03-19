using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Diagnostics;
using System.Linq;

namespace TISensorBrowser
{	
	public class CharacteristicDetails 
	{	

		public ICharacteristic Characteristic;


		public CharacteristicDetails (IAdapter adapter, IDevice device, IService service, ICharacteristic Characteristic)
		{
			this.Characteristic = Characteristic;

		}

		public async void OnAppearing ()
		{
			if (Characteristic.CanRead) {
				var c = await Characteristic.ReadAsync();

			}
		}

		public void OnDisappearing() 
		{
			
			if (Characteristic.CanUpdate) {
				Characteristic.StopUpdates();
			}
		}
		public string UpdateValue (ICharacteristic c) {
//			Name.Text = c.Name;
//			//ID.Text = c.ID.ToString(); OTHER INFO YOU CAN GET FROM BEAN
//			ID.Text = c.ID.PartialFromUuid ();
//			= BitConverter.to(c.Value);
			byte[] hr_array  = c.Value;
			int index = 0;
			foreach (var b in hr_array) {
				Debug.WriteLine (String.Format("bit {0}: {1}",index,b));
				index++;
			}
		
			//var s = (from i in c.Value where i == 0 select );
			//select i.ToString ()); //i.ToString ("X"));
			
			return hr_array[1].ToString();

		}
	}
}



