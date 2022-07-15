/**************************************************************************/
/*!
    @file     Transmitter.ino
    @author   Thomas Knepshield (POWR Rocket Capstone Project (UNCG Spring 2022))
    Some code used for data logging and GPS data collection derived from library examples.
    
    Code for POWR Rocket implementing the following sensors:
    1. Adafruit MPL3115A2 --- https://www.adafruit.com/product/992
    2. Adafruit SHT31 --- https://www.adafruit.com/product/2857
    3. Adafruit Ultimate GPS --- https://www.adafruit.com/product/746
    4. Adafruit MicroSD card Breakout --- https://www.adafruit.com/product/254
      4.1. I used a 32 gb SD card but any size works
    5. Reyax RYLR 890 LoRa Module --- https://www.amazon.com/gp/product/B07NB3BK5H/ref=ox_sc_saved_image_4?smid=A2M9CC26G0VWTA&psc=1
    Arduino Board Used:
    1. Arduino Nano Every --- https://store-usa.arduino.cc/products/arduino-nano-every
      1.1. Must use a board with specs similar to or greater than this board. Arduino Nano will NOT work.
    Rocket Used
    1. Estes Loadstar --> https://www.discountrocketry.com/estes-loadstar-p-1772.html
     Do not buy any electronics (other than Reyax Module) with headers. It will be hard to assemble with them.
    Other Useful Things:
    1. Wires --> https://www.amazon.com/dp/B07TX6BX47?psc=1&ref=ppx_yo2ov_dt_b_product_details
    2. Bread Boards --> https://www.amazon.com/dp/B01EV6LJ7G?psc=1&ref=ppx_yo2ov_dt_b_product_details
    3. Serial Adapter --> https://www.amazon.com/dp/B00LODGRV8?psc=1&ref=ppx_yo2ov_dt_b_product_details
    4. Soder Set (Random one found) --> https://www.amazon.com/dp/B08R3515SF/ref=cm_sw_r_tw_dp_84MB274FP0PBHQ0C8E53
    5. Wire Strippers --> https://www.amazon.com/dp/B000JNNWQ2/ref=cm_sw_r_tw_dp_FKMH2KSDEG139P8VXCN3
    6. 9v Batteries --> Anywhere
    7. 2x LED lights --> https://www.amazon.com/dp/B06XPV4CSH/ref=cm_sw_r_tw_dp_2Z13Y5QNM745NK9KX8ZM
    Pinouts:
    1. MPL3115A2
      Vin --> 5v
      GND --> GND
      SCL --> A5
      SDA --> A4
    2. SHT31
      Vin --> 5v
      GND --> GND
      SCL --> A5
      SDA --> A4
    3. Ultimate GPS
      Vin --> 5v
      GND --> GND
      RX  --> D7
      TX  --> D8
    4. MicroSD card Breakout
      5v  --> 5v
      GND --> GND
      CLK --> D13
      DO  --> D12
      DI  --> D11
      CS  --> (ChipSelect) D10
    5. LoRa Module
      VDD --> 3.3v
      GND --> GND
      RX  --> D9
      10k Ohm Resisitor D9 --> RX
      5k Ohm Resistor RX --> GND
    6. LED Lights
      1. D2 and GND for Transmission
      2. D3 and GND for GPS
    Reyax AT Commands:
    Done in the serial monitor (in tools) using a Serial Adapter. Change Baud to 115200 and "Both NL & CR".
      1. AT
      2. AT+RESET
      3. AT+ADDRESS=1 (Change this number to your favorite number. Do not use 1)
      4. AT+IPR=38400
      5. AT+PARAMETER=12,4,1,7
      6. AT (Change baud rate to 38400 before doing this. Should get +OK)
    Description
       This code uses the described sensors to collect the barometric pressure, humidity, temperature, latitude, longitude, altitude, time and speed.
       It then will concatenate the float values into strings and transmit it using LoRa. Once it transmits the data, it will also save the
       values on a micro SD card. This SD card will hold every data point. LoRa transmitter with selected parameter space will transmit data
       with a max of 4.5 - 8km without any objects in the way.
       This code will not print anything to the seial monitor other than the setup lines to verify everything is working.
       Using RX/TX pins on the Arduino Nano Every will not work so SoftwareSerial must be used for transmissions.
       First LED will blink once the code starts transmitting data.
    Tips
       If any errors pop up it could be from incorrect wiring, failed boot or broken sensors. Incorrect wiring is a very common issue.
       Also make sure you do not have any soder touching a reset pin. That will cause issues.
    v1.0 - First release
    UNCG, POWR Rocket project members, arduino and relevant parties are not responsible for any action that results in harm due to this project.
    If you do recreate this project, let me know on Twitter (@tknep3) so I can follow along with your project and keep up with improvements.
    Please send me your data so I can use it for future research.
*/
/**************************************************************************/

#include "Adafruit_SHT31.h"
#include <Adafruit_MPL3115A2.h>
#include <SPI.h>
#include <SD.h>
#include <Arduino.h>
#include <Wire.h>
#include <SoftwareSerial.h>
#include "TinyGPS++.h"


//Make sure to download the libararies in tools/manage libraries. Type in each sensor name.

#define pin 2 // Used with the LED
#define gpsPin 3 //Used with GPS 

SoftwareSerial GPS(8, 7); // RX, TX
SoftwareSerial Reyax(8, 9); // RX, TX
TinyGPSPlus tinygps;

Adafruit_SHT31 sht31 = Adafruit_SHT31(); // Initialize SHT31 sensor
Adafruit_MPL3115A2 baro; // Setup the pressure for the MPL3115A2 sensor (value put in below)
const int chipSelect = 10; // port for CS with the SD card
const double currentPressure = 1013.26; //Change this to currect pressure in hPa for accurate results
int id = 0;


void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(pin, OUTPUT); // Initialize LED as output for Transmission
  pinMode(gpsPin, OUTPUT); // Initialize LED as output for GPS fix
  Serial.begin(9600); // Start the serial ports
  Reyax.begin(38400); // Start the Reyax module Software Serial port
  GPS.begin(9600); //Start the GPS module

  // Make sure all sensors are initialized, wired correctly and are operational

  Serial.println("SHT31 test"); //Test SHT31 Sensor
  if (! sht31.begin(0x44)) {   // Set to 0x45 for alternate i2c addr
    Serial.println("Couldn't find SHT31");
    while (1) delay(1);
  }

  Serial.println("Adafruit_MPL3115A2 test!"); //Test MPL3115A2 sensor

  if (!baro.begin()) {
    Serial.println("Could not find sensor. Check wiring.");
    while (1);
  }

  // use to set pressure for current location
  baro.setSeaPressure(currentPressure);

  Serial.print("Initializing SD card...");
  // see if the card is present and can be initialized:
  if (!SD.begin(chipSelect)) {
    Serial.println("Card failed, or not present");
    while (1);
  }
  Serial.println("card initialized.");

  Serial.println("Setup complete");
  //Loop will start once this is printed to Serial Monitor.

}

void loop() {

  while (GPS.available() > 0) {
    if (tinygps.encode(GPS.read())) { // Did a new valid sentence come in?
      digitalWrite(gpsPin, HIGH);   // turn the LED on signaling transmission was sent
      delay(100);
      digitalWrite(gpsPin, LOW);
    }
  }

  String pressureString = "";     // empty strings for data
  String altitudeString = "";
  String temperatureString = "";
  String humidityString = "";
  String latitudeString = "";
  String longitudeString = "";
  String speedString = "";
  String idString = "";
  String hourString = "";
  String minuteString = "";
  String secondString = "";
  String timeString = "";
  String dataString = "";

  pressureString.concat(baro.getPressure()); // Concatenate number values into Strings
  altitudeString.concat(tinygps.altitude.meters());
  temperatureString.concat(baro.getTemperature());
  humidityString.concat(sht31.readHumidity());
  latitudeString.concat(String(tinygps.location.lng(),6));
  longitudeString.concat(String(tinygps.location.lat(),6));
  speedString.concat(tinygps.speed.kmph());
  hourString.concat(tinygps.time.hour());
  minuteString.concat(tinygps.time.minute());
  secondString.concat(tinygps.time.second());

  timeString += hourString; // Create time string in XX:XX:XX format
  timeString += ":";
  timeString += minuteString;
  timeString += ":";
  timeString += secondString;

  idString = String(id++);

  dataString += idString;
  dataString += ",";
  dataString += temperatureString; //Create string for data transmission separated by ,
  dataString += ",";
  dataString += humidityString;
  dataString += ",";
  dataString += pressureString;
  dataString += ",";
  dataString += altitudeString;
  dataString += ",";
  dataString += latitudeString;
  dataString += ",";
  dataString += longitudeString;
  dataString += ",";
  dataString += speedString;
  dataString += ",";
  dataString += timeString;

  Serial.println(dataString);


  String first = "AT+SEND=0,"; // AT command for sending data to Software Serial port for Reyax module. Set this equal to the address for your reciever
  first += String(dataString.length()); //number of characters in the transmission string
  first += ",";
  first += String(dataString); //Data string to be sent
  Reyax.println(first); //Transmit data
  digitalWrite(pin, HIGH);   // turn the LED on signaling transmission was sent
  delay(100);
  digitalWrite(pin, LOW);


  // open the file
  File dataFile = SD.open("datalog.csv", FILE_WRITE); // Open file (1 can be open at a time)

  if (dataFile) { // if the file is available, write to it:
    dataFile.println(dataString); // Write data to SD card
    dataFile.close(); // Close the file
  }
  // if the file isn't open, error
  else {
    Serial.println("error opening datalog.txt"); //If you recieve this error then you are not saving data.
  }

  //Each Transmission takes 0.2 seconds + runtime each. If you want it to be faster, reduce the delays on the LED flash.
  //GPS will post data even if a fix is not met. Reccomended you wait until you recieve a fix before you use the device.
  //This code will run until the 9v battery becomes too weak to use. Data processiong will be needed.
}
