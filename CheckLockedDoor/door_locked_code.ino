/* 
  This sketch reads the acceleration from the Bean's on-board accelerometer. 
  
  The acceleration readings are sent over serial and can be accessed in Arduino's Serial Monitor.
  
  To use the Serial Monitor, set Arduino's serial port to "/tmp/tty.LightBlue-Bean"
  and the Bean as "Virtual Serial" in the OS X Bean Loader.
    
  This example code is in the public domain.
*/
int door_locked = 0;
boolean connected;
void setup() {
  // Bean Serial is at a fixed baud rate. Changing the value in Serial.begin() has no effect.
  Serial.begin();   
  // Optional: Use Bean.setAccelerationRange() to set the sensitivity to something other than the default of ±2g.
}

void loop() {
  // Get the current acceleration with range of ±2g, and a conversion of 3.91×10-3 g/unit or 0.03834(m/s^2)/units. 
  AccelerationReading acceleration = Bean.getAcceleration();
     connected = Bean.getConnectionState();
  if(connected){
    // Turn the LED green
    Bean.setLed(0, 255, 0);
    
    // Format the serial output like this:    "X: 249  Y: -27   Z: -253"
    String stringToPrint = String();
    stringToPrint = stringToPrint + "X: " + acceleration.xAxis + "\tY: " + acceleration.yAxis + "\tZ: " + acceleration.zAxis;
    Serial.println(stringToPrint);
    if (acceleration.xAxis >= 0){
      door_locked = 1;
    }
    else {
      door_locked = 0;
    }
    Bean.setScratchNumber(2, door_locked);
    Bean.sleep(1000);
    
   
  }
  else{
    // Turn the LED red
    Bean.setLed(255, 0, 0);
    
  }


}



