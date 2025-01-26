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
//#define DEBUG_LOG

const int BUTTON_PIN_START = 2; // First pin for buttons
const int NUM_BUTTONS = 6;
int previousButtonStates[NUM_BUTTONS]; // for checking the state of pushButtons
char buttonKeys[NUM_BUTTONS]; // which key to send for each button

int getButtonPin(int buttonIndex) {
  return BUTTON_PIN_START + buttonIndex;
}

void setup() {
  // initialize control over the keyboard:
  Keyboard.begin();
  for (int i = 0; i < NUM_BUTTONS; ++i) {
    previousButtonStates[i] = HIGH;
    // make the pushButton pins an input with built-in resistor
    pinMode(getButtonPin(i), INPUT_PULLUP);
  }
  buttonKeys[0] = 'q';
  buttonKeys[1] = 'w';
  buttonKeys[2] = 'e';
  buttonKeys[3] = 'r';
  buttonKeys[4] = 'o';
  buttonKeys[5] = 'p';
#ifdef DEBUG_LOG
  Serial.begin(9600);
#endif
}

void loop() {
  // read the pushbuttons
  for (int i = 0; i < NUM_BUTTONS; ++i) {
    int buttonState = digitalRead(getButtonPin(i));
    // if the button state has changed,
    if (buttonState != previousButtonStates[i]) {
        if (buttonState == LOW) {
          // button was pressed
          Keyboard.press(buttonKeys[i]);
          #ifdef DEBUG_LOG
          Serial.print("button press pin:");
          Serial.print(getButtonPin(i));
          Serial.print(" key:");
          Serial.println(buttonKeys[i]);
          #endif
        } else {
          // button was released
          int released = Keyboard.release(buttonKeys[i]);
          #ifdef DEBUG_LOG
          Serial.print("button release pin:");
          Serial.print(getButtonPin(i));
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
