/**************************************************************************/
/*!
    @file     Reciever.ino
    @author   Thomas Knepshield & Sage Bonfield (POWR Rocket Capstone Project (UNCG Spring 2022))
    Some code used for data logging and Reyax recieve parsing derived from library examples.
     
    Code for POWR Rocket implementing the following sensors:
    1.Adafruit MicroSD card Breakout --- https://www.adafruit.com/product/254
      1.1. I used a 32 gb SD card but any size works
    2. Reyax RYLR 890 LoRa Module --- https://www.amazon.com/gp/product/B07NB3BK5H/ref=ox_sc_saved_image_4?smid=A2M9CC26G0VWTA&psc=1
     Do not buy anything (other than Reyax Module) with headers unless you want the reciever on a breadboard.
    Other Useful Things:
    1. Wires --> https://www.amazon.com/dp/B07TX6BX47?psc=1&ref=ppx_yo2ov_dt_b_product_details
    2. Bread Boards --> https://www.amazon.com/dp/B01EV6LJ7G?psc=1&ref=ppx_yo2ov_dt_b_product_details
    3. Serial Adapter --> https://www.amazon.com/dp/B00LODGRV8?psc=1&ref=ppx_yo2ov_dt_b_product_details
    4. Soder Set (Random one found) --> https://www.amazon.com/dp/B08R3515SF/ref=cm_sw_r_tw_dp_84MB274FP0PBHQ0C8E53
    5. Wire Strippers --> https://www.amazon.com/dp/B000JNNWQ2/ref=cm_sw_r_tw_dp_FKMH2KSDEG139P8VXCN3
    6. LED lights --> https://www.amazon.com/dp/B06XPV4CSH/ref=cm_sw_r_tw_dp_2Z13Y5QNM745NK9KX8ZM
    Arduino Board Used:
    1. Arduino Nano Every --- https://store-usa.arduino.cc/products/arduino-nano-every
      1.1. Arduino Nano will work, but the every is better.
  Pinouts:
    1. MicroSD card Breakout
      5v  --> 5v
      GND --> GND
      CLK --> D13
      DO  --> D12
      DI  --> D11
      CS  --> (chipSelect) D10
    2. LoRa Module
      VDD --> 3.3v
      GND --> GND
      TX  --> D8
    3. LED Lights
      D2 and GND for Recieving
    Reyax AT Commands:
    Done in the serial monitor (in tools) using a Serial Adapter. Change Baud to 115200 and "Both NL & CR".
      1. AT
      2. AT+RESET
      3. AT+ADDRESS=1 (Change this number to your favorite number. Do not use 1 or the same number as the transmitter)
         This is the recieving address so you have to change the code in transmitter.ino at AT+SEND= to equal this number
      4. AT+IPR=38400
      5. AT+PARAMETER=12,4,1,7
      6. AT (Change baud rate to 38400 before doing this. Should get +OK)
    Description
        This code will recieve data from a Reyax RYLR 890 module and save it to an SD card.
        LoRa reciever with selected parameter space will recieve data from a transmitter with a max of 4.5 - 8km without any objects in the way.
        This code will print the recieved data to the seial monitor along with the setup lines to verify everything is working.
        Using RX/TX pins on the Arduino Nano Every will not work so SoftwareSerial must be used for transmissions.
        LED will blink once the code starts recieving data. Each blink is a revieved data point.
        It will then parse the data into comma separated values for easier processing.
    Tips
       If any errors pop up it could be from incorrect wiring, failed boot or broken sensors. Incorrect wiring is a very common issue.
       Also make sure you do not have any soder touching a reset pin. That will cause issues.
    v1.0 - First release
    UNCG, POWR Rocket project members, arduino and relevant parties are not responsible for any action that results in harm due to this project.
    If you do recreate this project, let me know on Twitter (@tknep3) so I can follow along with your project and keep up with improvements.
    Please send me your data so I can use it for future research.
*/
/**************************************************************************/

#include <SoftwareSerial.h>
#include <SPI.h>
#include <SD.h>

SoftwareSerial Reyax(8, 9); // RX, TX

#define pin 2

int chipSelect = 10;

String inputString = ""; //string to hold incoming data
String commandString = ""; // becomes the input string through getCommand()
String id = "EverAr1"; // identifier for the arduino. Max is 8 bytes, including terminating byte.

int dataLimit = 10; //limit to counter "current"
int current = 0; //counter for loop() iterations

bool stringComplete = false;



void setup() {
  pinMode(pin, OUTPUT);
  Serial.begin(9600);
  Reyax.begin(38400);

  Serial.print("Initializing SD card...");
  // see if the card is present and can be initialized:
  if (!SD.begin(chipSelect)) {
    Serial.println("Card failed, or not present");
    // don't do anything more:
    while (1);
  }
  Serial.println("card initialized.");
}

void loop() {
  if (Reyax.available()) {
    String received = Reyax.readString();

    digitalWrite(pin, HIGH);   // turn the LED on signaling data is recieved
    delay(100);                       // wait for 1/10
    digitalWrite(pin, LOW);

    if (received.indexOf("+RCV") >= 0) {
      int parser, parser_1, parser_2, parser_3;
      parser = received.indexOf(",");
      parser_1 = received.indexOf(",", parser + 1);
      parser_2 = received.indexOf(",", parser_1 + 1);
      parser_3 = received.indexOf(",", parser_2 + 1);
      int lenMessage = received.substring(parser + 1, parser_1).toInt();

      String message = received.substring(parser_1 + 1, parser_1 + lenMessage + 1);

      Serial.print("Recived message: ");
      Serial.println(message);

      // open the file
      File dataFile = SD.open("datalog.csv", FILE_WRITE); // Open file (1 can be open at a time)

      if (dataFile) { // if the file is available, write to it:
        dataFile.println(message); // Write data to SD card
        dataFile.close(); // Close the file
      }
      // if the file isn't open, error
      else {
        Serial.println("error opening datalog.txt");
      }
    }
  }
  if (stringComplete) {
    getCommand();
    if (commandString.equals("DA") && stringComplete == true) {
      File dataFile = SD.open("datalog.csv", FILE_READ);
      // read from the file until there's nothing else in it:
      while (dataFile.available()) {
        dataFile.read();
      }
      dataFile.close();

      //ending conditions
      stringComplete = false;
      inputString = "";

    } else if (commandString.equals("ID") && stringComplete == true) {
      Serial.println(id);
      stringComplete = false;
      inputString = "";
    } else {
      // string does not equal one of the desired strings
      // or stringComplete was made false already
      stringComplete = false;
    }
  } else { // if stringComplete is false
    serialEvent();
  }
}

/* Used to extract substrings from inputString to be tested on in loop().
   If the substring is of length 0, function is left.
*/
void getCommand() {
  if (inputString.length() > 0) {
    if (inputString.equals("ID\n") || inputString.equals("DA\n")) {
      commandString = inputString.substring(0, 2);
    } else {
      Serial.println("Input is not valid.");
      stringComplete = false;
      inputString = "";
    }
  }
}

/* Detects if there are bytes available to be read.
    If no bytes, leave the function.
    Else, read the new bytes and append each to inputString.
*/
void serialEvent() {
  while (Serial.available()) {
    //get the new byte
    char inChar = (char)Serial.read();
    // add to input string
    inputString += inChar;
    // test if the last character has been read,
    // if so, string is complete
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}
