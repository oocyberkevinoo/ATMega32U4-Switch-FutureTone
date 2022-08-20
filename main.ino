#include <QuickMpr121.h>
#include <FastLED.h>
#include "LUFAConfig.h"
#include <LUFA.h>
#include "Joystick.h"
#include <Bounce2.h>
#include <EEPROM.h>

#define BOUNCE_WITH_PROMPT_DETECTION
#define MILLIDEBOUNCE 1 //Debounce time in milliseconds

//Debug Timer
unsigned long previousMillis = 0;
unsigned long interval = 30;

// Buttons values
byte buttonStatus[17] = {0};
int buttonSwitchModeTimer = 1000;

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
// #define PS4_TOUCHPAD_MASK_ON 0x???

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

// Is it in PS4 mode (Not available for now, should use a converter for PS4 for now, that ensure correct analog datas... See my git ReadMe for more informations)
bool PS4 = false;

// Is it in NAV shortcut mode ?
bool NAV = false;

// Slider gameplay enabled ?
bool sliderGameplayEnabled = true;

// DEFINE Slider
#define NUM_MPRS 3                // Number of MPRs
#define PROXIMITY_ENABLE false    // Proximity check (not needed)
#define MPR_THRESHOLD_TOUCH 12    // Default Touch Sensitivity (default 15 - 10) (Optimised 10 - 9)  (perfect 9, 6)
#define MPR_THRESHOLD_RELEASE 10  // Default Release Sensitivity (2~4 behind the touch one)

// Initialise arrays for sensors...
short sensors[NUM_MPRS*12];             
short sensorsConfirmed[NUM_MPRS*12];
short sensorsConfirmed2[NUM_MPRS*12];
bool sensorsTouched[NUM_MPRS*12];
bool sensorTouched;
bool sensorsDisabled[NUM_MPRS*12];

// create the mpr121 instances
// these will have addresses set automatically
mpr121 mprs[NUM_MPRS];

// Calibration asked on boot
bool calibrated = false;
int calibratedCount = 0;
int calibratedCountNeeded = 26;

// Slider MODES list
typedef enum {
  GAMEPLAY,
  NAVIGATION,
  NAVIGATION2,
  CHUNITHM,
  ARCADE,
  MENU,
  SETTINGS,
  TRIGGER,
  CALIBRATE,
  PPD
} SliderMode;
SliderMode sliderMode = GAMEPLAY;       // Default mode is GAMEPLAY
SliderMode sliderModeChange = GAMEPLAY; // Transition mode, change it to change mode on button release correctly

// Slider Key mapping (What sensors do what)
//int sensorsSwapPS4[3] {0, 1, 2};
int sensorsChunithm[5] {0, 1, 2, 3, 4};
int sensorsArcade[5] {9, 10, 11, 12, 13};
int sensorsNav[5] {18, 19, 20, 21, 22};
int sensorsGame[5] {27, 28, 29, 30, 31};

// Slider Settings values
bool pushedSettings1 = false;
bool pushedSettings2 = false;
bool pushedSettings3 = false;
bool pushedSettings4 = false;
bool pushedSettings5 = false;
bool pushedSettings6 = false;
bool pushedSettings7 = false;
bool pushedSettings8 = false;
bool pushedSettings9 = false;
byte sliderFilter1 = 0x00;
byte sliderFilter2 = 0x00;
byte sliderOffsetRelease = 0x00;
byte gameplayLightUp = 0x00;

// Slider LightUp Effect
int lightUpTimer = 0;
int lightUpMax = 25*5;
int lightUpCurrent = 0;

int lightWave = 0;

// Navigation page
int navigationPage = 1;

short sensor32 = 0;


// LEDs
#define NUM_LEDS_PER_STRIP 32
#define LED_TYPE WS2812B
#define LED_DATA_PIN 21
#define COLOR_ORDER GRB
CRGB leds[NUM_LEDS_PER_STRIP];
//bool idleLeds = true;

// BOUNCE BUTTONS
Bounce joystickUP = Bounce();
Bounce joystickDOWN = Bounce();
Bounce joystickLEFT = Bounce();
Bounce joystickRIGHT = Bounce();
Bounce buttonA = Bounce();
Bounce buttonB = Bounce();
Bounce buttonX = Bounce();
Bounce buttonY = Bounce();
Bounce buttonLB = Bounce();
Bounce buttonRB = Bounce();
Bounce buttonLT = Bounce();
Bounce buttonRT = Bounce();
Bounce buttonSTART = Bounce();
Bounce buttonSELECT = Bounce();
Bounce buttonHOME = Bounce();

Bounce switchModePin = Bounce();
Bounce navModePin = Bounce();

typedef enum {
  DIGITAL,
} State_t;
State_t state = DIGITAL;

// Buttons pins setup
void setupPins(){
  //  You'll not have enough pins on a Pro Micro, please change the pins to the correspondig one.
  //  You can use Navgation mode of the Slider to have virtual buttons.
    joystickUP.attach(255,INPUT_PULLUP);    // Unused
    joystickDOWN.attach(255,INPUT_PULLUP);  // Unsued
    joystickLEFT.attach(1,INPUT_PULLUP);
    joystickRIGHT.attach(0,INPUT_PULLUP);
    buttonA.attach(4,INPUT_PULLUP);
    buttonB.attach(5,INPUT_PULLUP);
    buttonX.attach(7,INPUT_PULLUP);
    buttonY.attach(6,INPUT_PULLUP);
    buttonLB.attach(9,INPUT_PULLUP);
    buttonRB.attach(8,INPUT_PULLUP);
    buttonLT.attach(16,INPUT_PULLUP);
    buttonRT.attach(10,INPUT_PULLUP);
    buttonSTART.attach(15,INPUT_PULLUP);
    buttonSELECT.attach(14,INPUT_PULLUP);
    //buttonHOME.attach(18,INPUT_PULLUP);
    
    switchModePin.attach(19, INPUT_PULLUP);
    navModePin.attach(18, INPUT_PULLUP);
  
    joystickUP.interval(MILLIDEBOUNCE);
    joystickDOWN.interval(MILLIDEBOUNCE);
    joystickLEFT.interval(MILLIDEBOUNCE);
    joystickRIGHT.interval(MILLIDEBOUNCE);
    buttonA.interval(MILLIDEBOUNCE);
    buttonB.interval(MILLIDEBOUNCE);
    buttonX.interval(MILLIDEBOUNCE);
    buttonY.interval(MILLIDEBOUNCE);
    buttonLB.interval(MILLIDEBOUNCE);
    buttonRB.interval(MILLIDEBOUNCE);
    buttonLT.interval(MILLIDEBOUNCE);
    buttonRT.interval(MILLIDEBOUNCE);
    buttonSTART.interval(MILLIDEBOUNCE);
    buttonSELECT.interval(MILLIDEBOUNCE);
    //buttonHOME.interval(MILLIDEBOUNCE);
    
    switchModePin.interval(MILLIDEBOUNCE);
    navModePin.interval(MILLIDEBOUNCE);
    
}

// Initialise the MPR121s on boot or calibration
void sensorsInitialization(){

    for (int i = 0; i < NUM_MPRS; i++) {
    // this special line makes `mpr` the same as typing `mprs[i]`
    mpr121 &mpr = mprs[i];

    
    // `mpr.begin()` sets up the Wire library
    // mpr121 can run in 400kHz mode; if you have issues with it or want 100kHz speed, use `mpr121.begin(100000)`
    // (or use `Wire.begin` and/or `Wire.setClock` directly instead of this)
    mpr.begin();

    // set autoconfig charge level based on 3.2V
    // without this, it will assume 1.8V (a safe default, but not always ideal)
    mpr.autoConfigUSL = 256L * (3200 - 700) / 3200;

    // enable proximity sensing if it's set to true
    if (PROXIMITY_ENABLE)
      mpr.proxEnable = MPR_ELEPROX_0_TO_11;
    else
      mpr.proxEnable = MPR_ELEPROX_DISABLED;
  
    
    // Setting up the MPR121's filters depending on EEPROM stored values...
    switch(sliderFilter1){
      default:
      mpr.FFI = MPR_FFI_10;
      mpr.SFI = MPR_SFI_6;
      break;
      case 0x01:
      mpr.FFI = MPR_FFI_18;
      mpr.SFI = MPR_SFI_10;
      break;
      case 0x02:
      break;
      }

    // Setting up the MPR121's sensitivity...
    int _sensitivity = SensitivityLoad();
    int _ReleaseThresholdOffset = ReleaseThresholdOffsetLoad();
    // Sensitivity (default 15 - 10) (Optimised 10 - 9)  (perfect 9, 6)
    mpr.setAllThresholds(_sensitivity, _sensitivity-(_ReleaseThresholdOffset), false);

    // start sensing
    mpr.start(12);

  }
}

void ledsInitialization(){
  FastLED.addLeds<LED_TYPE, LED_DATA_PIN, COLOR_ORDER>(leds, NUM_LEDS_PER_STRIP);
  FastLED.setBrightness(LEDBrightnessLoad());
  FastLED.setMaxPowerInVoltsAndMilliamps(5, 230);
  }

void settingsLoader(){ // Load settings from EEPROM that need fast load
  sliderFilter1 = EEPROM.read(2);
  sliderFilter2 = EEPROM.read(3);
  gameplayLightUp = EEPROM.read(5);
  }
  

//Arduino Setup Process...
void setup() {
  settingsLoader();             // Load settings from EEPROM that need fast load...
  sensorsInitialization();      // Initialise MPR121s...
  setupPins();                  // Setup Arduino Pins...
  ledsInitialization();         // Initialise Leds...
  SetupHardware();              // Setup Hardware...
  GlobalInterruptEnable();
}

// Arduino Loop process...
void loop() {
    buttonRead();   // Buttons state
    checkSensors(); // Sensors state

    if (switchModePin.fell() && !buttonStatus[NAVMODEPIN]){  // MENU mode button is pressed...
        if(sliderMode == MENU){ // If already in MENU mode, change to CALIBRATE mode...
          sliderModeChange = CALIBRATE;
          sliderMode = CALIBRATE;
        }else{
          sliderModeChange = MENU;
          sliderMode = MENU;
        }
    }
    
    // Change mode on release only. (avoid false positive inputs in the new selected mode)
    bool swapmode = true;
    if(sliderMode != sliderModeChange){
      for (bool sensor : sensors){
        if(sensor && swapmode)  swapmode = false;
        }
    }
        
        if(swapmode) sliderMode = sliderModeChange;
        
    switch(sliderMode) // Change the SLider Mode...
    {
      case GAMEPLAY: // Slider act like the HORI controller's Slider with Dedicated Controller Mode (For Mega39's / MegaMix / Future Tone (International version not compatible)
      sliderGameplay();
      processButtons(); // Processing controller state...
      break;
      case NAVIGATION: // Slider act like a controller with navigations buttons if you lack of buttons on your controller (customize the code below)
      sliderNavigation();
      processButtons();
      break;
      case CHUNITHM: // Slider CHUNITHM 8 KEYS
      sliderGameplay();
      processButtons();
      break;
      case ARCADE: // Slider act like a Keyboard that you can hook up to Project Diva Arcade Future Tone (TO DO)
      sliderMenu();
      break;
      case MENU: // Slider is in the menu selection mode
      sliderMenu();
      break;
      case SETTINGS: // Slider settings
      sliderSettings();
      break;
      case TRIGGER: // Disable faulty sensor
      sliderTriggerMode();
      break;
      case CALIBRATE: // Calibrate slider
      calibrateSensors();
      break;
    }

    
    
    HID_Task();
    // We also need to run the main USB management task.
    USB_USBTask();
    FastLED.show();

    
}

// CALIBRATE MODE
void calibrateSensors(){  

    // output Default Stick values to not trigger false positive in game
    long resultBits;
    int32_t sliderBits = 0;
    resultBits = sliderBits ^ 0x80808080;
    // SENDING TO CONTROLLER THE RESULTED VALUES
    ReportData.RY = (resultBits >> 24) & 0xFF;
    ReportData.RX = (resultBits >> 16) & 0xFF;
    ReportData.LY = (resultBits >> 8) & 0xFF;
    ReportData.LX = (resultBits) & 0xFF;

  
    leds[0] = CRGB::White;
    leds[1] = CRGB::White;
    leds[2] = CRGB::White;
    calibrated = false;
    calibratedCount = 0;
    for (int i = 0; i < NUM_MPRS; i++) {
      mpr121 &mpr = mprs[i];
      mpr.softReset();
    }
    sliderModeChange = GAMEPLAY;
    sensorsInitialization();
  }

/*
 * SETTINGS LOADER
 */

int LEDBrightnessLoad(){  // LEDs brightness level
  switch(EEPROM.read(0))
    {
    case 0x00:
    return 255;
    break;
    case 0x01:
    return 200;
    break;
    case 0x02:
    return 155;
    break;
    case 0x03:
    return 100;
    break;
    case 0x04:
    return 50;
    break;
    case 0x05:
    return 25;
    break;
    case 0x06:
    return 10;
    break;
    default:
    return 255;
    
    }
  }

int SensitivityLoad(){  // Sensitivity of sensors
  switch(EEPROM.read(1)){
    case 0x00:
    return 13;
    break;
    case 0x01:
    return 11;
    break;
    case 0x02:
    return 10;
    break;
    case 0x03:
    return 9;
    break;
    case 0x04:
    return 8;
    break;
    case 0x05:
    return 7;
    break;
    case 0x06:
    return 6;
    break;
    case 0x07:
    return 5;
    break;
    case 0x08:
    return 4;
    break;
    default:
    return 11;
    }
  }

  int ReleaseThresholdOffsetLoad(){ // Offset Release
    switch(EEPROM.read(4)){
      case 0x00:
      return 4;
      break;
      case 0x01:
      return 3;
      break;
      case 0x02:
      return 2;
      break;
      case 0x03:
      return 1;
      break;
      case 0x04:
      return 0;
      break;
      case 0x05:
      return 5;
      break;
      case 0x06:
      return 6;
      break;
      default:
      return 2;
      }
    }

    int CalibrationStrength(){ // Calibration Strength
    switch(EEPROM.read(6)){
      case 0x00:
      return 26;
      break;
      case 0x01:
      return 30;
      break;
      case 0x02:
      return 32;
      break;
      case 0x03:
      return 1;
      break;
      case 0x04:
      return 12;
      break;
      break;
      default:
      return 26;
      }
    }

/*
 * SLIDER ENGINE
 */
  

// Manage the output of the Slider sensors
void checkSensors(){
  int numElectrodes = 12; // How many electrodes/sensors by MPR121 ?
  int sensorCount = 0;    // What sensor are we actually working on ?
  int touchedSensors = 0; // How many sensors confirmed to be touched ?
  
  for (int i = 0; i < NUM_MPRS; i++) { // Checking each MPR...
    mpr121 &mpr = mprs[i];
    for (int j = 0; j < numElectrodes; j++) { // Checking each sensor...
      short touching = mpr.readTouchState(j); // Is it touched ?...

      /*if(i == 2 && j == 7)
      sensor32 = touching;*/

      
      if(sensorsDisabled[sensorCount] || (!sliderGameplayEnabled && sliderMode == GAMEPLAY)){ // Is it disabled ?
        if(sliderGameplayEnabled && (sensors[sensorCount-1] || sensors[sensorCount+1])) // Is near sensors is touched ?
          touching = 0x01;
        else
          touching = 0x00;
        }
      // CUSTOM FILTER...
      switch(sliderFilter2){  
        default:      // Filter Level 2: Double check... (Bigest latency, best stability...)
          if(touching){
          if(sensorsConfirmed[sensorCount] && sensorsConfirmed2[sensorCount])
            sensors[sensorCount] = touching;
          else if(sensorsConfirmed[sensorCount])
            sensorsConfirmed2[sensorCount] = touching;
          else
            sensorsConfirmed[sensorCount] = touching;
          }
          else{
            sensors[sensorCount] = touching;
            sensorsConfirmed[sensorCount] = touching;
            sensorsConfirmed2[sensorCount] = touching;
            }
        break;
        case 0x01:    // Filter Level 1: Single Check... (Almost no latency, some stability...)
          if(touching){
          if(sensorsConfirmed[sensorCount])
            sensors[sensorCount] = touching;
          else
            sensorsConfirmed[sensorCount] = touching;
          }
          else{
            sensors[sensorCount] = touching;
            sensorsConfirmed[sensorCount] = touching;
            }
        break;
        case 0x02:    // Filter Level 0: No Check... (No latency, stability depend only on sensitivity and MPR filters...)
          sensors[sensorCount] = touching;
        break;
          
        }

        // How many sensors touched ? (not used)
      if(touching)
        touchedSensors++;
      else if(touchedSensors > 0)
        touchedSensors--;

      // Next sensor...
      sensorCount++;
    }
    // CALIBRATION PROCESS IF ASKED OR NEEDED ON BOOT... (calibration results based on the first sensor...)
    if(!calibrated){
      /*
      short value = mpr.readElectrodeData(0); 
      byte baseline = mpr.readElectrodeBaseline(0);
      value -= ((short)baseline << 2);
      if(value > 0 && value < 5) 
        calibrated = true;*/

        calibratedCountNeeded = CalibrationStrength();
        
        for(i=0; i < NUM_MPRS * 12; i++){
          short value = mpr.readElectrodeData(i); 
          byte baseline = mpr.readElectrodeBaseline(i);
          value -= ((short)baseline << 2);
          if(value > 0 && value < 5) 
            calibratedCount++;
        }

        if(calibratedCount >= calibratedCountNeeded)
          calibrated = true;
        else
          calibratedCount = 0;

        
        
      }
      
  }

  // Check if at least one sensor is touched (not used)
  if(touchedSensors > 0){
    sensorTouched = true;
    }
}

// Is a sensor has been just touched ? (Unused)
bool sensorJustTouched(int x){
  if(sensors[x] && !sensorsTouched[x])
    {
    sensorsTouched[x] = true;  
    return true;
    }
    return false;
  }


// Slider Menu to change the mode the Slider is currently on.
void sliderMenu(){
   // output Default Stick values to not trigger false positive in game
  long resultBits;
  int32_t sliderBits = 0;
  resultBits = sliderBits ^ 0x80808080;
  // SENDING TO CONTROLLER THE RESULTED VALUES
  ReportData.RY = (resultBits >> 24) & 0xFF;
  ReportData.RX = (resultBits >> 16) & 0xFF;
  ReportData.LY = (resultBits >> 8) & 0xFF;
  ReportData.LX = (resultBits) & 0xFF;

  processButtons();
  //resetButtons();
  
   
    //Virtual Buttons of menu
  if (!buttonStatus[SWITCHMODEPIN])
  {    
    if(sensors[sensorsArcade[0]] || sensors[sensorsArcade[1]] || sensors[sensorsArcade[2]] || sensors[sensorsArcade[3]] || sensors[sensorsArcade[4]])
      sliderModeChange = SETTINGS;
    else if(sensors[sensorsNav[0]] || sensors[sensorsNav[1]] || sensors[sensorsNav[2]] || sensors[sensorsNav[3]] || sensors[sensorsNav[4]])
      sliderModeChange = NAVIGATION;
    else if(sensors[sensorsChunithm[0]] || sensors[sensorsChunithm[1]] || sensors[sensorsChunithm[2]] || sensors[sensorsChunithm[3]] || sensors[sensorsChunithm[4]])
      sliderModeChange = CHUNITHM;
    else if(sensors[sensorsGame[0]] || sensors[sensorsGame[1]] || sensors[sensorsGame[2]] || sensors[sensorsGame[3]] || sensors[sensorsGame[4]])
      sliderModeChange = GAMEPLAY;
    else if(buttonStatus[BUTTONX])
      sliderModeChange = TRIGGER;
    else if(buttonStatus[BUTTONHOME]){
      if(sliderGameplayEnabled)
        sliderGameplayEnabled = false;
      else
        sliderGameplayEnabled = true;  
      sliderModeChange = GAMEPLAY;
      }
      
    
  }
   //LEDS
for (CRGB &led : leds){
        led = CRGB::Black;
        }
      
    leds[sensorsArcade[0]] = leds[sensorsArcade[1]] = leds[sensorsArcade[2]] = leds[sensorsArcade[3]] = leds[sensorsArcade[4]] = CRGB(0, 255, 153);
    leds[sensorsChunithm[0]] = leds[sensorsChunithm[1]] = leds[sensorsChunithm[2]] = leds[sensorsChunithm[3]] = leds[sensorsChunithm[4]] = CRGB(255, 0, 242);
    leds[sensorsNav[0]] = leds[sensorsNav[1]] = leds[sensorsNav[2]] = leds[sensorsNav[3]] = leds[sensorsNav[4]] = CRGB(0, 128, 255);
    leds[sensorsGame[0]] = leds[sensorsGame[1]] = leds[sensorsGame[2]] = leds[sensorsGame[3]] = leds[sensorsGame[4]] = CRGB::Red;
}


// Slider Gameplay Engine (can be used for all sort of Slider/Touchpad mechanics based games.)
void sliderGameplay(){
  long resultBits;
  long noTouchBits;
  int bit_count = 31; // Number of sensors for your slider

  int32_t sliderBits = 0;
  noTouchBits = sliderBits ^ 0x80808080;
  if(sliderMode != CHUNITHM && !buttonStatus[NAVMODEPIN]){
    for (bool sensor : sensors)
  {
    if(bit_count >= 0){
    // Check current Sensor state here
    if(sensor)
      sliderBits |= (1l << bit_count); // Add sensor state to the byte array.
    bit_count -= 1;
    }
  }
    }
  

  resultBits = sliderBits ^ 0x80808080;

  // SENDING TO CONTROLLER THE RESULTED VALUES
    ReportData.RY = (resultBits >> 24) & 0xFF;
    ReportData.RX = (resultBits >> 16) & 0xFF;
    ReportData.LY = (resultBits >> 8) & 0xFF;
    ReportData.LX = (resultBits) & 0xFF;

  if(sliderMode == GAMEPLAY){ // Slider in GAMEPLAY move (Future Tone/ Mega Mix mode)
    
    

    // LEDS
  if(!calibrated){ // When calibrating...
    
    bool lightUp = true;
    /*  Previously was just white while calibrating.
      for (CRGB &led : leds){
        if(lightUp){
          led = CRGB::White;
          lightUp = false;
          }
          else{
            led = CRGB::Black;
            lightUp = true;
            }
        }*/
        // Rainbow effect
      uint8_t thisHue = beat8(42,255); 
      fill_rainbow(leds, NUM_LEDS_PER_STRIP, thisHue, -15); 
      

  }else if(resultBits == noTouchBits && gameplayLightUp == 0x01){   // White pannel when not touching, with transition and buffer to make it smooth
    // Adjust timer and RGB values for LEDs
    if(lightUpTimer > 0) lightUpTimer--;
    if(lightUpCurrent < lightUpMax) lightUpCurrent++;
       bool lightUp = true;
       bool first = true;
        for (CRGB &led : leds){
          if(lightUp && lightUpTimer <= 0){
            if(!first)
              led = CRGB(lightUpCurrent/5, lightUpCurrent/5, lightUpCurrent/5);
            else
              led = CRGB::Black; // Except for the first one

            if(first) first = false;
            lightUp = false;
            }
          else{
            led = CRGB::Black;
            lightUp = true;
            }
        }
   
    }else if(gameplayLightUp == 0x02){
      
    // Adjust timer and RGB values for LEDs
    if(resultBits == noTouchBits){
      if(lightUpTimer > 0) lightUpTimer--;
      if(lightUpCurrent < lightUpMax) lightUpCurrent++; 
    }
    else{
      if(lightUpTimer < 25) lightUpTimer = lightUpTimer+2;
      if(lightUpCurrent > 0) lightUpCurrent = lightUpCurrent-2;

      if(lightUpTimer > 25) lightUpTimer = 25;
      if(lightUpCurrent < 0) lightUpCurrent = 0;
    }
    
       bool lightUp = true;
       bool first = true;
        for (CRGB &led : leds){
          if(lightUp){
            if(!first)
              led = CRGB(lightUpCurrent/5, lightUpCurrent/5, lightUpCurrent/5);
            else
              led = CRGB::Black; // Except for the first one

            if(first) first = false;
            lightUp = false;
            }
          else{
            led = CRGB::Black;
            lightUp = true;
            }
        }
        
        for(int i = 0; i < NUM_LEDS_PER_STRIP; i++){  // Lightup the touched sensor
          if(sensors[i]) leds[i] = CRGB::White;

          // WAVE
          /*bool wave = true;
          if(lightWave < 5000 && wave == true){
            lightWave++;
            if(lightWave >= 5000) wave = false;
          }
          else if(lightWave > 0 && wave == false){
            lightWave--;
            if(lightWave <= 0 && sensors[i]) wave = true;
          }

          for(int i2 = lightWave/1000; i2 > 0; i2--){
            if(sensors[i]){
              int left = i-i2;
              int right = i+i2;
              if(right <= NUM_LEDS_PER_STRIP-1 && !sensors[i+i2] && leds[i+i2] < leds[i]/90 && (i+i2 != 1 && i+i2 != 3 && i+i2 != 5 && i+i2 != 7 && i+i2 != 9 && i+i2 != 11 && i+i2 != 13 && i+i2 != 15 && i+i2 != 17 && i+i2 != 19 &&i+i2 != 21 && i+i2 != 23 && i+i2 != 25 && i+i2 != 27 && i+i2 != 29 && i+i2 != 31))
                leds[i+i2] = leds[i]/90;
              if(left >= 0  && !sensors[i-i2] && leds[i-i2] < leds[i]/90 && (i-i2 != 1 && i-i2 != 3 && i-i2 != 5 && i-i2 != 7 && i-i2 != 9 && i-i2 != 11 && i-i2 != 13 && i-i2 != 15 && i-i2 != 17 && i-i2 != 19 &&i-i2 != 21 && i-i2 != 23 && i-i2 != 25 && i-i2 != 27 && i-i2 != 29 && i-i2 != 31))
                leds[i-i2] = leds[i]/90;
            }
          }*/
          
          
          
          }

      
    }
    else{
      if(gameplayLightUp == 0x02){ // Arcade Lights Mode
        // Adjust timer and RGB values for LEDs
    if(lightUpTimer < 25) lightUpTimer++;
    if(lightUpCurrent > 0) lightUpCurrent--;
       bool lightUp = true;
       bool first = true;
        for (CRGB &led : leds){
          if(lightUp && lightUpTimer >= 25){
            if(!first)
              led = CRGB(lightUpCurrent/5, lightUpCurrent/5, lightUpCurrent/5);
            else
              led = CRGB::Black; // Except for the first one

            if(first) first = false;
            lightUp = false;
            }
          else{
            led = CRGB::Black;
            lightUp = true;
            }
        }
      }
      else{
        for (CRGB &led : leds){
        led = CRGB::Black;
        }
      }
    
    for(int i = 0; i < NUM_LEDS_PER_STRIP; i++){  // Lightup the touched sensor
      if(sensors[i]){
        leds[i] = CRGB::White;
        
        if(lightUpCurrent != 0 && gameplayLightUp == 0x01) lightUpCurrent = 0;
        if(lightUpTimer != 25 && gameplayLightUp == 0x01) lightUpTimer = 25;
        }
      }
    }
  }
  else if(sliderMode == CHUNITHM){ // Slider in Chunithm Mode
    // GAMEPLAY
    // KEYS CHUNITHM (Need tool to convert it to Keyboard or whatever... Made for 8 Keys inputs.)
    if(sensors[0] || sensors[1] || sensors[2] || sensors[3]) buttonStatus[BUTTONA] = true; else buttonStatus[BUTTONA] = false;
    if(sensors[4] || sensors[5] || sensors[6] || sensors[7]) buttonStatus[BUTTONB] = true; else buttonStatus[BUTTONB] = false;
    if(sensors[8] || sensors[9] || sensors[10] || sensors[11]) buttonStatus[BUTTONX] = true; else buttonStatus[BUTTONX] = false;
    if(sensors[12] || sensors[13] || sensors[14] || sensors[15]) buttonStatus[BUTTONY] = true; else buttonStatus[BUTTONY] = false;
    if(sensors[16] || sensors[17] || sensors[18] || sensors[19]) buttonStatus[BUTTONRB] = true; else buttonStatus[BUTTONRB] = false;
    if(sensors[20] || sensors[21] || sensors[22] || sensors[23]) buttonStatus[BUTTONLB] = true; else buttonStatus[BUTTONLB] = false;
    if(sensors[24] || sensors[25] || sensors[26] || sensors[27]) buttonStatus[BUTTONRT] = true; else buttonStatus[BUTTONRT] = false;
    if(sensors[28] || sensors[29] || sensors[30] || sensors[31]) buttonStatus[BUTTONLT] = true; else buttonStatus[BUTTONLT] = false;
    
    // LEDS CHUNITHM (Yellow lighted up, Purple on 3 LEDs near touching)
  if(!calibrated){
    bool lightUp = true;
      for (CRGB &led : leds){
        if(lightUp){
          led = CRGB::Yellow;
          lightUp = false;
          }
          else{
            led = CRGB::Black;
            lightUp = true;
            }
            //led = CRGB::White;
        }
    }else{
    for (CRGB &led : leds){
        led = CRGB::Yellow;
        }
    for(int i = 0; i < NUM_LEDS_PER_STRIP; i++){
      if(sensors[i]){
        leds[i] = CRGB::Purple;
        if(leds[i+1]) leds[i+1] = CRGB::Purple;
        if(leds[i-1]) leds[i-1] = CRGB::Purple;
        }
      }
    }
  } else if(sliderMode == PPD){ // Slider in PPD Mode
    // GAMEPLAY
    // KEYS PPD (Need tool to convert it to Keyboard or whatever.)

    
    
    if(sensors[0] || sensors[1] || sensors[2] || sensors[3]) buttonStatus[BUTTONA] = true; else buttonStatus[BUTTONA] = false;
    if(sensors[4] || sensors[5] || sensors[6] || sensors[7]) buttonStatus[BUTTONB] = true; else buttonStatus[BUTTONB] = false;
    if(sensors[8] || sensors[9] || sensors[10] || sensors[11]) buttonStatus[BUTTONX] = true; else buttonStatus[BUTTONX] = false;
    if(sensors[12] || sensors[13] || sensors[14] || sensors[15]) buttonStatus[BUTTONY] = true; else buttonStatus[BUTTONY] = false;
    if(sensors[16] || sensors[17] || sensors[18] || sensors[19]) buttonStatus[BUTTONRB] = true; else buttonStatus[BUTTONRB] = false;
    if(sensors[20] || sensors[21] || sensors[22] || sensors[23]) buttonStatus[BUTTONLB] = true; else buttonStatus[BUTTONLB] = false;
    if(sensors[24] || sensors[25] || sensors[26] || sensors[27]) buttonStatus[BUTTONRT] = true; else buttonStatus[BUTTONRT] = false;
    if(sensors[28] || sensors[29] || sensors[30] || sensors[31]) buttonStatus[BUTTONLT] = true; else buttonStatus[BUTTONLT] = false;
    
    // LEDS
    if(!calibrated){ // When calibrating...
    
    bool lightUp = true;
    /*  Previously was just white while calibrating.
      for (CRGB &led : leds){
        if(lightUp){
          led = CRGB::White;
          lightUp = false;
          }
          else{
            led = CRGB::Black;
            lightUp = true;
            }
        }*/
        // Rainbow effect
      uint8_t thisHue = beat8(15,255); 
      fill_rainbow(leds, NUM_LEDS_PER_STRIP, thisHue, -15); 
      

  }else if(resultBits == noTouchBits && gameplayLightUp == 0x01){   // White pannel when not touching, with transition and buffer to make it smooth
    // Adjust timer and RGB values for LEDs
    if(lightUpTimer > 0) lightUpTimer--;
    if(lightUpCurrent < lightUpMax) lightUpCurrent++;
       bool lightUp = true;
       bool first = true;
        for (CRGB &led : leds){
          if(lightUp && lightUpTimer <= 0){
            if(!first)
              led = CRGB(lightUpCurrent/5, lightUpCurrent/5, lightUpCurrent/5);
            else
              led = CRGB::Black; // Except for the first one

            if(first) first = false;
            lightUp = false;
            }
          else{
            led = CRGB::Black;
            lightUp = true;
            }
        }
   
    }else{
    for (CRGB &led : leds){
        led = CRGB::Black;
        }
    for(int i = 0; i < NUM_LEDS_PER_STRIP; i++){  // Lightup the touched sensor
      if(sensors[i]){
        leds[i] = CRGB::White;
        if(lightUpCurrent != 0) lightUpCurrent = 0;
        if(lightUpTimer != 25) lightUpTimer = 25;
        }
      }
    }
  
  }
  
  
  
  }

// Disable specific sensors, and enable the one around them to trigger it. Use if faulty sensors. (can be used to diagnose if some sensors trigger without touching)
void sliderTriggerMode(){

  
  for (CRGB &led : leds){
          led = CRGB::Green;
          }
  for(int i = 0; i < NUM_LEDS_PER_STRIP; i++){
    if(sensors[i]){
      sensorsDisabled[i] = true;
      }
    if(sensorsDisabled[i]){
      leds[i] = CRGB::Red;
      }
    }

    if(buttonStatus[BUTTONB]){
      for (bool &sensorDisabled : sensorsDisabled){
        sensorDisabled = false;
        }
      }
  
  }

// Act as controllers buttons using virtual buttons on the Slider.
void sliderNavigation(){

  // output Default Stick values to not trigger false positive in game
  long resultBits;
  int32_t sliderBits = 0;
  resultBits = sliderBits ^ 0x80808080;
  // SENDING TO CONTROLLER THE RESULTED VALUES
  ReportData.RY = (resultBits >> 24) & 0xFF;
  ReportData.RX = (resultBits >> 16) & 0xFF;
  ReportData.LY = (resultBits >> 8) & 0xFF;
  ReportData.LX = (resultBits) & 0xFF;

  // Define LED colors...
  CRGB colorPushed = CRGB::White;
  CRGB colorL1Btn = CRGB::DodgerBlue;
  CRGB colorR1Btn = CRGB::DodgerBlue;
  CRGB colorL2Btn = CRGB::DarkBlue;
  CRGB colorR2Btn = CRGB::DarkBlue;
  CRGB colorUpBtn = CRGB(0, 255, 153);
  CRGB colorDownBtn = CRGB(0, 128, 255);
  CRGB colorLeftBtn = CRGB(255, 0, 242);
  CRGB colorRightBtn = CRGB::Red;
  CRGB colorShareBtn = CRGB::Cyan;
  /* 
   * sensors 0-2 & 29-31 : L1 / R1 | Blue
   * sensors 3-5 & 26-28 : L2 / R2 | Darker blue
   * sensors 6 & 25 : L3 / R3 | darkest blue
   * sensors 9-11 : Up | Green
   * sensors 13-15 : Down | Blue
   * sensors 17-19 : Left | Pink
   * sensors 21-23 : Right | Red
   */

/*switch(page){
  case 0:*/
    // Bumpers/Triggers
    if(sensors[0] || sensors[1] || sensors[2]) {buttonStatus[BUTTONLB] = true;}
    else {buttonStatus[BUTTONLB] = false;}
    if(sensors[29] || sensors[30] || sensors[31]) {buttonStatus[BUTTONRB] = true;}
    else {buttonStatus[BUTTONRB] = false;}
    if(sensors[3] || sensors[4] || sensors[5]) {buttonStatus[BUTTONLT] = true;}
    else {buttonStatus[BUTTONLT] = false;}
    if(sensors[26] || sensors[27] || sensors[28]) {buttonStatus[BUTTONRT] = true;}
    else {buttonStatus[BUTTONRT] = false;}
    //if(sensors[6]) {buttonStatus[BUTTONL3] = true;}
    //if(sensors[25]) {buttonStatus[BUTTONR3] = true;}
  
    // DPAD
    if (sensors[7] || sensors[8] || sensors[9]) {buttonStatus[BUTTONUP] = true;}
    else {buttonStatus[BUTTONUP] = false;}
    if (sensors[11] || sensors[12] || sensors[13]) {buttonStatus[BUTTONDOWN] = true;}
    else {buttonStatus[BUTTONDOWN] = false;}
    if (sensors[17] || sensors[18] || sensors[19]) {buttonStatus[BUTTONLEFT] = true;}
    else {buttonStatus[BUTTONLEFT] = false;}
    if (sensors[21] || sensors[22] || sensors[23]) {buttonStatus[BUTTONRIGHT] = true;}
    else {buttonStatus[BUTTONRIGHT] = false;}
  
    // Apply LEDS colors...
    if(buttonStatus[BUTTONLB]) colorL1Btn = colorPushed;
    if(buttonStatus[BUTTONRB]) colorR1Btn = colorPushed;
    if(buttonStatus[BUTTONLT]) colorL2Btn = colorPushed;
    if(buttonStatus[BUTTONRT]) colorR2Btn = colorPushed;
    if(buttonStatus[BUTTONUP]) colorUpBtn = colorPushed;
    if(buttonStatus[BUTTONDOWN]) colorDownBtn = colorPushed;
    if(buttonStatus[BUTTONLEFT]) colorLeftBtn = colorPushed;
    if(buttonStatus[BUTTONRIGHT]) colorRightBtn = colorPushed;
    
    for (CRGB &led : leds){
          led = CRGB::Black;
          }
    leds[0] = leds[1] = leds[2] = colorL1Btn;
    leds[29] = leds[30] = leds[31] = colorR1Btn;
    leds[3] = leds[4] = leds[5] = colorL2Btn;
    leds[26] = leds[27] = leds[28] = colorR2Btn;
    leds[8] = leds[9] = leds[10] = colorUpBtn;
    leds[12] = leds[13] = leds[14] = colorDownBtn;
    leds[17] = leds[18] = leds[19] = colorLeftBtn;
    leds[21] = leds[22] = leds[23] = colorRightBtn;
  /*break;
  
  case 1: // Page 2 not implemented
  
    // DPAD
    if (sensors[9] || sensors[10] || sensors[11]) {buttonStatus[BUTTONSELECT] = true;}
    else {buttonStatus[BUTTONSELECT] = false;}
  
    // LEDS
    if(buttonStatus[BUTTONSELECT]) colorShareBtn = colorPushed;

    
    for (CRGB &led : leds){
          led = CRGB::Black;
          }
   
    leds[8] = leds[9] = leds[10] = colorShareBtn;

  break;
  }*/
}


// Edit and save settings for the Slider behaviour, using virtual buttons.
void sliderSettings(){

  // output Default Stick values to not trigger false positive in game
  long resultBits;
  int32_t sliderBits = 0;
  resultBits = sliderBits ^ 0x80808080;
  // SENDING TO CONTROLLER THE RESULTED VALUES
  ReportData.RY = (resultBits >> 24) & 0xFF;
  ReportData.RX = (resultBits >> 16) & 0xFF;
  ReportData.LY = (resultBits >> 8) & 0xFF;
  ReportData.LX = (resultBits) & 0xFF;

  processButtons();
  bool testingLeds = false;
  // Virtual Buttons :
  // LED's Brightness
  if(sensors[0] || sensors[1] || sensors[2]) { 
    if(!pushedSettings1){
      updateSettings(0, 0x06);
      FastLED.setBrightness(LEDBrightnessLoad());
      pushedSettings1 = true;
    }else
      testingLeds = true;
    }
  else if(pushedSettings1){
    pushedSettings1 = false;
    testingLeds = false;
    } 
  
  // MPR121's sensitivity (How sensitive to touch it is ?)
  if(sensors[4] || sensors[5] || sensors[6]) { 
    if(!pushedSettings2)
      pushedSettings2 = true;
    }
  else if(pushedSettings2){
    updateSettings(1, 0x08);
    pushedSettings2 = false;
    }
  // Offset Release (How sensitive to release it is compared to touch sensitive ?)
  if(sensors[7] || sensors[8]) {
  if(!pushedSettings5)
    pushedSettings5 = true;
  }
else if(pushedSettings5){
  updateSettings(4, 0x06);
  pushedSettings5 = false;
  } 

    // Slider Filter 1 (How much data needed to confirm the touch through MPR121s filter.)
  if(sensors[13] || sensors[14] || sensors[15]) { 
    if(!pushedSettings3)
      pushedSettings3 = true;
    }
  else if(pushedSettings3){
    updateSettings(2, 0x02);
    settingsLoader();
    pushedSettings3 = false;
    } 

    // Slider Filter 2 (How much cycles sensors mut be touched to confirm the touch through Custom Filter.)
  if(sensors[16] || sensors[17] || sensors[18]) { 
    if(!pushedSettings4)
      pushedSettings4 = true;
    }
  else if(pushedSettings4){
    updateSettings(3, 0x02);
    settingsLoader();
    pushedSettings4 = false;
    } 

    // Lighten Up when not touching on GAMEPLAY ?
  if(sensors[22] || sensors[23]) { 
    if(!pushedSettings6)
      pushedSettings6 = true;
    }
  else if(pushedSettings6){
    updateSettings(5, 0x02);
    settingsLoader();
    pushedSettings6 = false;
    } 

    // Calibrating Level (s 30-31) setting 6
    if(sensors[30] || sensors[31]) { 
    if(!pushedSettings7)
      pushedSettings7 = true;
    }
  else if(pushedSettings7){
    updateSettings(6, 0x04);
    settingsLoader();
    pushedSettings7 = false;
    } 

    // Saving Settings
    if(buttonStatus[NAVMODEPIN]) { 
    if(!pushedSettings8)
      pushedSettings8 = true;
    }
  else if(pushedSettings8){
    EEPROM.write(10, EEPROM.read(0));
    EEPROM.write(11, EEPROM.read(1));
    EEPROM.write(12, EEPROM.read(2));
    EEPROM.write(13, EEPROM.read(3));
    EEPROM.write(14, EEPROM.read(4));
    EEPROM.write(15, EEPROM.read(5));
    EEPROM.write(16, EEPROM.read(6));
    settingsLoader();
    pushedSettings8 = false;
    } 
    
    // Loading Settings
    if(buttonStatus[BUTTONSTART]) { 
    if(!pushedSettings9)
      pushedSettings9 = true;
    }
  else if(pushedSettings9){
    EEPROM.write(0, EEPROM.read(10));
    EEPROM.write(1, EEPROM.read(11));
    EEPROM.write(2, EEPROM.read(12));
    EEPROM.write(3, EEPROM.read(13));
    EEPROM.write(4, EEPROM.read(14));
    EEPROM.write(5, EEPROM.read(15));
    EEPROM.write(6, EEPROM.read(16));
    settingsLoader();
    FastLED.setBrightness(LEDBrightnessLoad());
    pushedSettings9 = false;
    } 

  
  // LEDS output
for (CRGB &led : leds){
        led = CRGB::Black;
        }
        
  leds[0] = leds[1] = leds[2] = CRGB::Red;

  if(testingLeds){
    leds[13] = leds[15] = leds[17] = leds[19] = CRGB::White;
    return;
    } 
  
  switch(EEPROM.read(1)){ // MPR121's sensitivity
    case 0x00:
    leds[4] = leds[5] = leds[6] = CRGB::Red;
    break;
    case 0x01:
    leds[4] = leds[5] = leds[6] = CRGB::Orange;
    break;
    case 0x02:
    leds[4] = leds[5] = leds[6] = CRGB::Yellow;
    break;
    case 0x03:
    leds[4] = leds[5] = leds[6] = CRGB::Green;
    break;
    case 0x04:
    leds[4] = leds[5] = leds[6] = CRGB::Cyan;
    break;
    case 0x05:
    leds[4] = leds[5] = leds[6] = CRGB::Blue;
    break;
    case 0x06:
    leds[4] = leds[5] = leds[6] = CRGB::Purple;
    break;
    case 0x07:
    leds[4] = leds[5] = leds[6] = CRGB::Pink;
    break;
    case 0x08:
    leds[4] = leds[5] = leds[6] = CRGB::White;
    break;
    default: leds[4] = leds[5] = leds[6] = CRGB::Green;
  }
  switch(EEPROM.read(4)){ // Offset Release
    case 0x00:
    leds[7] = leds[8] = CRGB::Green;
    break;
    case 0x01:
    leds[7] = leds[8] = CRGB::Blue;
    break;
    case 0x02:
    leds[7] = leds[8] = CRGB::Cyan;
    break;
    case 0x03:
    leds[7] = leds[8] = CRGB::Yellow;
    break;
    case 0x04:
    leds[7] = leds[8] = CRGB::Red;
    break;
    case 0x05:
    leds[7] = leds[8] = CRGB::Purple;
    break;
    case 0x06:
    leds[7] = leds[8] = CRGB::Pink;
    break;
    default: leds[4] = leds[5] = leds[6] = CRGB::White;
  }
  switch(EEPROM.read(2)){ // MPR121's Filter
    case 0x00:
    leds[13] = leds[14] = leds[15] = CRGB::Yellow;
    break;
    case 0x01:
    leds[13] = leds[14] = leds[15] = CRGB::Red;
    break;
    case 0x02:
    leds[13] = leds[14] = leds[15] = CRGB::Green;
    break;
    default: leds[13] = leds[14] = leds[15] = CRGB::Yellow;
  }
  switch(EEPROM.read(3)){ // Custom Filter
    case 0x00:
    leds[16] = leds[17] = leds[18] = CRGB::Red;
    break;
    case 0x01:
    leds[16] = leds[17] = leds[18] = CRGB::Yellow;
    break;
    case 0x02:
    leds[16] = leds[17] = leds[18] = CRGB::Green;
    break;
    default: leds[16] = leds[17] = leds[18] = CRGB::Red;
  }
  switch(EEPROM.read(5)){ // GAMEPLAY light mode
    case 0x00:
    leds[22] = leds[23] = CRGB::Red;
    break;
    case 0x01:
    leds[22] = leds[23] = CRGB::Green;
    break;
    case 0x02:
    leds[22] = leds[23] = CRGB::White;
    break;
    default: leds[22] = leds[23] = CRGB::Red;
  }
  switch(EEPROM.read(6)){ // Calibration Strength
    case 0x00:
    leds[30] = leds[31] = CRGB::Blue;
    break;
    case 0x01:
    leds[30] = leds[31] = CRGB::Cyan;
    break;
    case 0x02:
    leds[30] = leds[31] = CRGB::Red;
    break;
    case 0x03:
    leds[30] = leds[31] = CRGB::Yellow;
    break;
    case 0x04:
    leds[30] = leds[31] = CRGB::Green;
    break;
    default: leds[30] = leds[31] = CRGB::Blue;
  }

}

// Saving Settings in EEPROM
void updateSettings(int index, byte _size){
  byte temp = EEPROM.read(index);
    if(temp < _size)
      temp++;
    else
      temp = 0x00;
    EEPROM.write(index, temp);
  }

void buttonRead(){
    if (joystickUP.update()) {buttonStatus[BUTTONUP] = joystickUP.fell();}
    if (joystickDOWN.update()) {buttonStatus[BUTTONDOWN] = joystickDOWN.fell();}
    if (joystickLEFT.update()) {buttonStatus[BUTTONLEFT] = joystickLEFT.fell();}
    if (joystickRIGHT.update()) {buttonStatus[BUTTONRIGHT] = joystickRIGHT.fell();}
    if (buttonA.update()) {buttonStatus[BUTTONA] = buttonA.fell();}
    if (buttonB.update()) {buttonStatus[BUTTONB] = buttonB.fell();}
    if (buttonX.update()) {buttonStatus[BUTTONX] = buttonX.fell();}
    if (buttonY.update()) {buttonStatus[BUTTONY] = buttonY.fell();}
    if (buttonLB.update()) {buttonStatus[BUTTONLB] = buttonLB.fell();}
    if (buttonRB.update()) {buttonStatus[BUTTONRB] = buttonRB.fell();}
    if (buttonLT.update()) {buttonStatus[BUTTONLT] = buttonLT.fell();}
    if (buttonRT.update()) {buttonStatus[BUTTONRT] = buttonRT.fell();}
    if (buttonSTART.update()) {buttonStatus[BUTTONSTART] = buttonSTART.fell();}
    if (buttonSELECT.update()) {buttonStatus[BUTTONSELECT] = buttonSELECT.fell();}
    //if (buttonHOME.update()) {buttonStatus[BUTTONHOME] = buttonHOME.fell();}
    if (switchModePin.update()) {buttonStatus[SWITCHMODEPIN] = switchModePin.fell();}
    if (navModePin.update()) {buttonStatus[NAVMODEPIN] = navModePin.fell();}
}


/*
 * Processing Buttons & Dpad
 */
void processButtons(){
  switch (state) // Can create other states to act differently...
  {
    case DIGITAL:
        processDPAD();
        buttonProcessing();
    break;
  }
}

void processDPAD(){
  if (!buttonStatus[NAVMODEPIN]){
    if ((buttonStatus[BUTTONUP]) && (buttonStatus[BUTTONRIGHT])){ReportData.HAT = DPAD_UPRIGHT_MASK_ON;}
    else if ((buttonStatus[BUTTONDOWN]) && (buttonStatus[BUTTONRIGHT])) {ReportData.HAT = DPAD_DOWNRIGHT_MASK_ON;} 
    else if ((buttonStatus[BUTTONDOWN]) && (buttonStatus[BUTTONLEFT])) {ReportData.HAT = DPAD_DOWNLEFT_MASK_ON;}
    else if ((buttonStatus[BUTTONUP]) && (buttonStatus[BUTTONLEFT])){ReportData.HAT = DPAD_UPLEFT_MASK_ON;}
    else if (buttonStatus[BUTTONUP]) {ReportData.HAT = DPAD_UP_MASK_ON;}
    else if (buttonStatus[BUTTONDOWN]) {ReportData.HAT = DPAD_DOWN_MASK_ON;}
    else if (buttonStatus[BUTTONLEFT]) {ReportData.HAT = DPAD_LEFT_MASK_ON;}
    else if (buttonStatus[BUTTONRIGHT]) {ReportData.HAT = DPAD_RIGHT_MASK_ON;}
    else{ReportData.HAT = DPAD_NOTHING_MASK_ON;}
  }else{
    if ((buttonStatus[BUTTONUP]) && (buttonStatus[BUTTONRIGHT])){ReportData.HAT = DPAD_UPRIGHT_MASK_ON;}
    else if ((buttonStatus[BUTTONDOWN]) && (buttonStatus[BUTTONRIGHT])) {ReportData.HAT = DPAD_DOWNRIGHT_MASK_ON;} 
    else if ((buttonStatus[BUTTONDOWN]) && (buttonStatus[BUTTONLEFT])) {ReportData.HAT = DPAD_DOWNLEFT_MASK_ON;}
    else if ((buttonStatus[BUTTONUP]) && (buttonStatus[BUTTONLEFT])){ReportData.HAT = DPAD_UPLEFT_MASK_ON;}
    else if (buttonStatus[BUTTONUP]) {ReportData.HAT = DPAD_LEFT_MASK_ON;}
    else if (buttonStatus[BUTTONDOWN]) {ReportData.HAT = DPAD_RIGHT_MASK_ON;}
    else if (buttonStatus[BUTTONLEFT]) {ReportData.HAT = DPAD_UP_MASK_ON;}
    else if (buttonStatus[BUTTONRIGHT]) {ReportData.HAT = DPAD_DOWN_MASK_ON;}
    else{ReportData.HAT = DPAD_NOTHING_MASK_ON;}
  }
    
}


void buttonProcessing(){

  if (!buttonStatus[NAVMODEPIN]){
    if (buttonStatus[BUTTONA]) {ReportData.Button |= A_MASK_ON;}
    if (buttonStatus[BUTTONB]) {ReportData.Button |= B_MASK_ON;}
    if (buttonStatus[BUTTONX]) {ReportData.Button |= X_MASK_ON;}
    if (buttonStatus[BUTTONY]) {ReportData.Button |= Y_MASK_ON;}
    if (buttonStatus[BUTTONLB]) {ReportData.Button |= LB_MASK_ON;}
    if (buttonStatus[BUTTONRB]) {ReportData.Button |= RB_MASK_ON;}
    if (buttonStatus[BUTTONLT]) {ReportData.Button |= ZL_MASK_ON;}
    if (buttonStatus[BUTTONRT]) {ReportData.Button |= ZR_MASK_ON;}
    if (buttonStatus[BUTTONSTART]){ReportData.Button |= START_MASK_ON;}
    if (buttonStatus[BUTTONSELECT]){ReportData.Button |= SELECT_MASK_ON;}
    if (buttonStatus[BUTTONHOME]){ReportData.Button |= HOME_MASK_ON;}
    // Need more buttons...
  }else{
    if (buttonStatus[BUTTONA]) {ReportData.Button |= RB_MASK_ON;}
    if (buttonStatus[BUTTONB]) {ReportData.Button |= ZR_MASK_ON;}
    if (buttonStatus[BUTTONX]) {ReportData.Button |= LB_MASK_ON;}
    if (buttonStatus[BUTTONY]) {ReportData.Button |= ZL_MASK_ON;}
    if (buttonStatus[BUTTONLB]) {ReportData.Button |= X_MASK_ON;}
    if (buttonStatus[BUTTONRB]) {ReportData.Button |= A_MASK_ON;}
    if (buttonStatus[BUTTONLT]) {ReportData.Button |= Y_MASK_ON;}
    if (buttonStatus[BUTTONRT]) {ReportData.Button |= B_MASK_ON;}
    if (buttonStatus[BUTTONSTART]){ReportData.Button |= CAPTURE_MASK_ON;}
    if (buttonStatus[BUTTONSELECT]){ReportData.Button |= START_MASK_ON;}
    if (buttonStatus[SWITCHMODEPIN]){ReportData.Button |= HOME_MASK_ON;}
    // Need more buttons...

    if (sensors[0] || sensors[1] || sensors[2]){ReportData.Button |= L3_MASK_ON;}
    if (sensors[31] || sensors[30] || sensors[29]){ReportData.Button |= R3_MASK_ON;}
    if (sensors[12] || sensors[13] || sensors[16] || sensors[17] || sensors[18] || sensors[19] || sensors[20]){ReportData.Button |= SELECT_MASK_ON;}
  }
  
  
}

void resetButtons(){
  buttonStatus[BUTTONA] = false;
  buttonStatus[BUTTONB] = false;
  buttonStatus[BUTTONX] = false;
  buttonStatus[BUTTONY] = false;
  buttonStatus[BUTTONLB] = false;
  buttonStatus[BUTTONRB] = false;
  buttonStatus[BUTTONLT] = false;
  buttonStatus[BUTTONRT] = false;
  buttonStatus[BUTTONSTART] = false;
  buttonStatus[BUTTONSELECT] = false;
  buttonStatus[BUTTONHOME] = false;

  buttonStatus[BUTTONUP] = false;
  buttonStatus[BUTTONDOWN] = false;
  buttonStatus[BUTTONLEFT] = false;
  buttonStatus[BUTTONRIGHT] = false;
}
