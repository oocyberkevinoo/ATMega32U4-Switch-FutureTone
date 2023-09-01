int SettingsLEDBrightnessLoad(char x){  // LEDs brightness level
  switch(x)
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

  int SettingsSensitivityLoad(char x){  // Sensitivity of sensors
  switch(x){
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
    return 8;
    }
  }

  int SettingsReleaseThresholdOffsetLoad(char x){ // Offset Release
  switch(x){
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

  int SettingsCalibrationStrength(char x){ // Calibration Strength
    switch(x){
  case 0x00:
  return 30;
  break;
  case 0x01:
  return 32;
  break;
  case 0x02:
  return 36;
  break;
  case 0x03:
  return 12;
  break;
  case 0x04:
  return 26;
  break;
  break;
  default:
  return 26;
  }
}

bool SettingsNAVShortcut(char x){ // How Navigation Mode works
  switch(x){
  case 0x00:
  return true;
  break;
  default:
  return false;
  }
}

bool SettingsHalfLedsMode(char x){ // Only half of your LEDs at the same time?
  switch(x){
      case 0x01:
      return true;
      break;
      default:
      return false;
    }
    
}
