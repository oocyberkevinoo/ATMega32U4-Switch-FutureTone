#include <Bounce2.h>

// DEFINE buttons >> pins
#define PIN_UP 255 // Unused, change if needed.
#define PIN_DOWN 255 // Unused, change if needed.
#define PIN_LEFT 1
#define PIN_RIGHT 0
#define PIN_A 4
#define PIN_B 5
#define PIN_X 7
#define PIN_Y 6
#define PIN_LB 9
#define PIN_RB 8
#define PIN_LT 16
#define PIN_RT 10
#define PIN_START 15
#define PIN_SELECT 14
#define PIN_HOME 18 // Unused, uncomment the command "buttonHOME.attach" in the Main file to use it.

#define PIN_SWITCHMODE 19  // Button to switch the Mode of the controller. (go to SLIDER MENU)
#define PIN_NAVMODE 18     // Button to enable NAVIGATION MODE that let you have different mapping



// DEFINE buttons addresses
#define CAPTURE_MASK_ON 0x2000
#define R3_MASK_ON 0x800
#define L3_MASK_ON 0x400
#define DPAD_UP_MASK_ON 0x00
#define DPAD_UPRIGHT_MASK_ON 0x01
#define DPAD_RIGHT_MASK_ON 0x02
#define DPAD_DOWNRIGHT_MASK_ON 0x03
#define DPAD_DOWN_MASK_ON 0x04
#define DPAD_DOWNLEFT_MASK_ON 0x05
#define DPAD_LEFT_MASK_ON 0x06
#define DPAD_UPLEFT_MASK_ON 0x07
#define DPAD_NOTHING_MASK_ON 0x08
#define A_MASK_ON 0x04
#define B_MASK_ON 0x02
#define X_MASK_ON 0x08
#define Y_MASK_ON 0x01
#define LB_MASK_ON 0x10
#define RB_MASK_ON 0x20
#define ZL_MASK_ON 0x40
#define ZR_MASK_ON 0x80
#define START_MASK_ON 0x200
#define SELECT_MASK_ON 0x100
#define HOME_MASK_ON 0x1000

// DEFINE buttons index
#define BUTTONUP 0
#define BUTTONDOWN 1
#define BUTTONLEFT 2
#define BUTTONRIGHT 3
#define BUTTONA 4
#define BUTTONB 5
#define BUTTONX 7
#define BUTTONY 6
#define BUTTONLB 8
#define BUTTONRB 9
#define BUTTONLT 10
#define BUTTONRT 11
#define BUTTONSTART 12
#define BUTTONSELECT 13
#define BUTTONHOME 14
#define SWITCHMODEPIN 15
#define NAVMODEPIN 16
