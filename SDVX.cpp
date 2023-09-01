bool SDVX_POTS[4] = {false, false, false, false}; // 2 potentiometers directions
int SDVX_SENSED[2][3] = {{0, 0, 0},   //  Array of last touched sensors
                          {0, 0, 0}}; 
bool SDVX_Touched[] = {false, false}; // Did we touched any of the 2 half of the Slider

int SDVX_sensorCount = 0;     // How many sensors checked
int SDVX_touchedSensors[2] = {0, 0};  // How many sensors did we touched on each half


void SDVX_Gameplay(bool sensor, bool firstHalf){
  int i = 0;      // Sensors index
  int i_pots = 0; // Potentiometer index
  if(!firstHalf){ // If we're working on the second half, we shift index's values to aim the correct element in arrays.
    i = 1;
    i_pots = 2;
  }
  
  if(sensor && SDVX_touchedSensors[i] < 2){   // If sensor touched and we didn't already touched 2 sensors on this cycle...
              SDVX_touchedSensors[i]++;
              SDVX_Touched[i] = true;
              if(SDVX_sensorCount != SDVX_SENSED[i][0] && SDVX_sensorCount != SDVX_SENSED[i][1] && SDVX_sensorCount != SDVX_SENSED[i][2]){  // Avoid double values...
                // Update the array...
                SDVX_SENSED[i][2] = SDVX_SENSED[i][1];
                SDVX_SENSED[i][1] = SDVX_SENSED[i][0];
                SDVX_SENSED[i][0] = SDVX_sensorCount;
              }

              // Calculate the direction and apply to the potetiometers...
              if(SDVX_SENSED[i][0] == SDVX_SENSED[i][2] || SDVX_SENSED[i][2] < 0){
                SDVX_POTS[i_pots] = false;
                SDVX_POTS[i_pots+1] = false;
              } else if(SDVX_SENSED[i][2] - SDVX_SENSED[i][0] > 0){
                SDVX_POTS[i_pots] = true;
                SDVX_POTS[i_pots+1] = false;
              } else if(SDVX_SENSED[i][2] - SDVX_SENSED[i][0] < 0){
                SDVX_POTS[i_pots] = false;
                SDVX_POTS[i_pots+1] = true;
              }
            }
}

void SDVX_CheckTouchedSlide(){
int i_pots = 0;
  for(int i = 0; i <= 1; i++){
    if(!SDVX_Touched[i]){
      // Nullify the potetiometers if no sensors have been confirmed to be touched...
      SDVX_SENSED[i][0] = SDVX_SENSED[i][1] = SDVX_SENSED[i][2] = -1;
      if(i > 0) i_pots = 2; 
      SDVX_POTS[i_pots] = false;
      SDVX_POTS[i_pots+1] = false;
    }
  }
  
}
