#include <Sensirion.h>


#define sensirionDataPin  2
#define sensirionClockPin 3 

float temperature;
float humidity;
float dewpoint;

Sensirion tempSensor = Sensirion(sensirionDataPin, sensirionClockPin);



void measureSensiron()
{
  tempSensor.measure(&temperature, &humidity, &dewpoint);

  /*Serial.print("Temperature: ");
  Serial.print(temperature);
  Serial.print(" C, Humidity: ");
  Serial.print(humidity);
  Serial.print(" %, Dewpoint: ");
  Serial.print(dewpoint);
  Serial.println(" C");
  */
  
 
}
float getHumidity(){
  return humidity;
}
float getTemperature(){
  return temperature;
}
float getDewpoint(){
  return dewpoint;
}

