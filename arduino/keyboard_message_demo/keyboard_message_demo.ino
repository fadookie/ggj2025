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

const int buttonPin = 2;         // input pin for pushbutton
int previousButtonState = HIGH;  // for checking the state of a pushButton
const char leftArmButton = 'q';

void setup() {
  // make the pushButton pin an input:
  pinMode(buttonPin, INPUT_PULLUP);
  // initialize control over the keyboard:
  Keyboard.begin();
}

void loop() {
  // read the pushbutton:
  int buttonState = digitalRead(buttonPin);
  // if the button state has changed,
  if (buttonState != previousButtonState) {
      if (buttonState == HIGH) {
        // button was pressed
        Keyboard.press(leftArmButton);
      } else {
        // button was released
        Keyboard.release(leftArmButton);
      }
  }
  // save the current button state for comparison next time:
  previousButtonState = buttonState;
}
