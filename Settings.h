// Slider
#define NUM_MPRS 3                // Number of MPRs
#define PROXIMITY_ENABLE false    // Proximity check (not needed)
#define MPR_THRESHOLD_TOUCH 12    // Default Touch Sensitivity (default 15 - 10) (Optimised 10 - 9)  (perfect 9, 6)
#define MPR_THRESHOLD_RELEASE 10  // Default Release Sensitivity (2~4 behind the touch one)

// LEDs
#define NUM_LEDS_PER_STRIP 32
#define LED_TYPE WS2812B
#define LED_DATA_PIN 21
#define COLOR_ORDER GRB
#define LEDS_MAXVOLTS 5           // How many volts for the LEDs
#define LEDS_MAXMILLIAMPS 230     // How many milliamps for the LEDs

// LEDs colors
int LEDS_COLOR_NOTOUCH[3] {0, 0, 0};
int LEDS_COLOR_TOUCH[3] {255, 255, 255};
int LEDS_COLOR_TRAIL[3] {65, 49, 51};

// Save EEPROM Settings addresses
#define SETTING_ADD_LEDSBRIGHTNESS 0  // How bright the LEDs
#define SETTING_ADD_SENSITIVITY 1     // How sensitive are your sensors to TOUCHING
#define SETTING_ADD_MPRFILTER 2       // What level of filtering your sensors works
#define SETTING_ADD_CUSTOMFILTER 3    // What level of filtering the custom built filter works (new filter incoming, near HORI experience)
#define SETTING_ADD_RELEASE 4         // How sensitive are your sensors to RELEASE
#define SETTING_ADD_LEDSMODE 5        // How act your LEDs in Gameplay
#define SETTING_ADD_CALIBRATION 6     // How many sensors have to be calibrated to end the calibration process
#define SETTING_ADD_NAVIGATION 7      // Does your Navigation Mode is enabled after holding your button, or pushed once
#define SETTING_ADD_HALFLEDS 8        // Do you want to light up only half of your LEDs when not touching if your power supply can't hold 32 white LEDs

int settings[] = {SETTING_ADD_LEDSBRIGHTNESS, SETTING_ADD_SENSITIVITY, SETTING_ADD_MPRFILTER, SETTING_ADD_CUSTOMFILTER, SETTING_ADD_RELEASE, SETTING_ADD_LEDSMODE, SETTING_ADD_CALIBRATION, SETTING_ADD_NAVIGATION, SETTING_ADD_HALFLEDS};

#define SETTING_ADD_USED 20            // Have you used PDAC Manager already (used to show you a first use warning message)


// Functions related to load settings (edit in C file)
int SettingsLEDBrightnessLoad(char);
int SettingsSensitivityLoad(char);
int SettingsReleaseThresholdOffsetLoad(char);
int SettingsCalibrationStrength(char);
bool SettingsNAVShortcut(char);
bool SettingsHalfLedsMode(char);
