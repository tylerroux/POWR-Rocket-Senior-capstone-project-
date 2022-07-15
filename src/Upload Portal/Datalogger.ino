/*
  SD card datalogger

  This example shows how to log data from three analog sensors
  to an SD card using the SD library.

  The circuit:
   analog sensors on analog ins 0, 1, and 2
   SD card attached to SPI bus as follows:
 ** MOSI - pin 11
 ** MISO - pin 12
 ** CLK - pin 13
 ** CS - pin 4 (for MKRZero SD: SDCARD_SS_PIN)

  created  24 Nov 2010
  modified 9 Apr 2012
  by Tom Igoe

  This example code is in the public domain.

  More code was provided by a tutorial video found at this link:
  https://www.youtube.com/watch?v=vHeG3Gt6STE
  "Arduino Tutorial: C# to Arduino Communication. Send data and commands from Computer to an Arduino."

*/

#include <SPI.h>
#include <SD.h>

String inputString = ""; //string to hold incoming data
String commandString = ""; // becomes the input string through getCommand()
String id = "EverAr1"; // identifier for the arduino. Max is 8 bytes, including terminating byte.

int dataLimit = 10; //limit to counter "current"
int current = 0; //counter for loop() iterations

const int chipSelect =10;
bool stringComplete = false;

void setup() {
  // Open serial communications and wait for port to open:
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }


  Serial.print("Initializing SD card...");

  // see if the card is present and can be initialized:
  if (!SD.begin(chipSelect)) {
    Serial.println("Card failed, or not present");
  } else {
    Serial.println("card initialized.");
  }
}

void loop() {
  if (stringComplete) {
    getCommand();
    if (commandString.equals("DA") && stringComplete == true) {
      File dataFile = SD.open("datalog2.txt", FILE_READ);
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
