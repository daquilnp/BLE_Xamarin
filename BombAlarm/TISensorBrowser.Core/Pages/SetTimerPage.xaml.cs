using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace TISensorBrowser
{
	public partial class SetTimerPage : ContentPage
	{
		DateTime triggerTime;

		public SetTimerPage ()
		{
			InitializeComponent(); 
			Device .StartTimer( TimeSpan.FromSeconds(1), OnTimerTick); 
		} 
		bool OnTimerTick() {

			if (t_switch.IsToggled && DateTime.Now >= triggerTime) 
			{ 
				t_switch.IsToggled = false ; DisplayAlert( "Timer Alert" , "The '" + entry.Text + "' timer has elapsed" , "OK" );
			} 
			return true ; 
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
	}
}
