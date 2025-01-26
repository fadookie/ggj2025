/*
  Keyboard Message test

  For the Arduino Leonardo and Micro.

  Sends a text string when a button is pressed.

  The circuit:
  - pushbutton attached from pin 4 to +5V
  - 10 kilohm resistor attached from pin 4 to ground

  created 24 Oct 2011
  modified 27 Mar 2012
  by Tom Igoe
  modified 11 Nov 2013
  by Scott Fitzgerald

  This example code is in the public domain.

  https://www.arduino.cc/en/Tutorial/BuiltInExamples/KeyboardMessage
*/

#include "Keyboard.h"
#define DEBUG_LOG

const int BUTTON_PIN_START = 2; // First pin for buttons
const int NUM_BUTTONS = 16;
int previousButtonStates[NUM_BUTTONS]; // for checking the state of pushButtons
char buttonKeys[NUM_BUTTONS]; // which key to send for each button
int buttonPins[NUM_BUTTONS];

void setup() {
  buttonPins[0] = 2;
  buttonPins[1] = 3;
  buttonPins[2] = 4;
  buttonPins[3] = 5;
  buttonPins[4] = 6;
  buttonPins[5] = 7;
  buttonPins[6] = 8;
  buttonPins[7] = 9;
  buttonPins[8] = 10;
  buttonPins[9] = 14;
  buttonPins[10] = 15;
  buttonPins[11] = 16;
  buttonPins[12] = A0;
  buttonPins[13] = A1;
  buttonPins[14] = A2;
  buttonPins[15] = A3;

  for (int i = 0; i < NUM_BUTTONS; ++i) {
    previousButtonStates[i] = HIGH;
    // make the pushButton pins an input with built-in resistor
    pinMode(buttonPins[i], INPUT_PULLUP);
  }

  buttonKeys[0] = 'q';
  buttonKeys[1] = 'w';
  buttonKeys[2] = 'e';
  buttonKeys[3] = 'r';
  buttonKeys[4] = 'u';
  buttonKeys[5] = 'i';
  buttonKeys[6] = 'o';
  buttonKeys[7] = 'p';
  buttonKeys[8] = 'a';
  buttonKeys[9] = 's';
  buttonKeys[10] = 'l';
  buttonKeys[11] = 'k';
  buttonKeys[12] = 'f';
  buttonKeys[13] = 'b';
  buttonKeys[14] = 'x';
  buttonKeys[15] = 'h';

  // initialize control over the keyboard:
  Keyboard.begin();

#ifdef DEBUG_LOG
  Serial.begin(9600);
#endif
}

void loop() {
  // read the pushbuttons
  for (int i = 0; i < NUM_BUTTONS; ++i) {
    int buttonState = digitalRead(buttonPins[i]);
    // if the button state has changed,
    if (buttonState != previousButtonStates[i]) {
        if (buttonState == LOW) {
          // button was pressed
          Keyboard.press(buttonKeys[i]);
          #ifdef DEBUG_LOG
          Serial.print("button press pin:");
          Serial.print(buttonPins[i]);
          Serial.print(" key:");
          Serial.println(buttonKeys[i]);
          #endif
        } else {
          // button was released
          int released = Keyboard.release(buttonKeys[i]);
          #ifdef DEBUG_LOG
          Serial.print("button release pin:");
          Serial.print(buttonPins[i]);
          Serial.print(" key:");
          Serial.print(buttonKeys[i]);
          Serial.print(" released:");
          Serial.println(released);
          #endif
        }
        // save the current button state for comparison next time:
        previousButtonStates[i] = buttonState;
    }
  }
}
