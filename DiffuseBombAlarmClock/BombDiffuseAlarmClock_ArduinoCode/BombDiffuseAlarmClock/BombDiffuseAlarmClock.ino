/* 
  This sketch reads the ambient temperature from the Bean's on-board temperature sensor. 
  
  The temperature readings are sent over serial and can be accessed in Arduino's Serial Monitor.
  
  To use the Serial Monitor, set Arduino's serial port to "/tmp/tty.LightBlue-Bean" 
  and the Bean as "Virtual Serial" in the OS X Bean Loader.
    
  This example code is in the public domain.
*/
int speaker_pin = 5;
int red_wire = 1;
int brown_wire = 2;
int blue_wire = 3;
bool connected = false;
uint8_t buf[2];
void setup() {
  Bean.setLed(0, 0, 0);
  pinMode(speaker_pin, OUTPUT);
  pinMode(red_wire, INPUT_PULLUP);
  pinMode(brown_wire, INPUT_PULLUP);
  pinMode(blue_wire, INPUT_PULLUP);
  // Bean Serial is at a fixed baud rate. Changing the value in Serial.begin() has no effect.
  Serial.begin();
}

void loop() {
  // Get the current ambient temperature in degrees Celsius with a range of -40 C to 87 C.

   connected = Bean.getConnectionState();
  if(connected){
    // Turn the LED green
    Bean.setLed(0, 255, 0);
    digitalWrite(speaker_pin, HIGH);
    
    buf[0] = digitalRead(red_wire);
    buf[1] = digitalRead(brown_wire);
    buf[2] = digitalRead(blue_wire);
    Bean.setScratchData(2, buf, 3);
   
  }
  else{
    // Turn the LED red
    Bean.setLed(255, 0, 0);
    digitalWrite(speaker_pin, LOW);
    
    
  }

    Bean.sleep(1500); 
}
