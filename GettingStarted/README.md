#Usage
This is an example code based off of Monkey.Robotics Xamarin BluetoothTISensor ( https://github.com/xamarin/Monkey.Robotics ) that I modified. Do not forget to change
the target in the AndroidManifest.xml in the Properties folder of TISensorBrowser.Droid (if you are using Android with Xamarin).

#Scratch Characteristics

The Scratch Characteristics is one of the ways Lightblue Bean sends and recieves information. ( https://punchthrough.com/bean/guides/features/scratch-chars/)
The code provided here sends the current temperature from the Bean to your phone through Scratch Characteristic 2. 
To find the temperature value, once the device has been connected and you selected your device, click on the Service 
with the id "a495ff20-c5b1-4b44-b512-1370f02d74de", this is the id of the Scratch Characteristics. Once in the Characteristic
list look for the Characteristic with the id "a495ff22-c5b1-4b44-b512-1370f02d74de", this stores the value of the 
Scratch Characteristic 2 (the current temperature). 
