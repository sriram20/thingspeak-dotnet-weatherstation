#include <Wire.h>
#include <Adafruit_BMP085.h>


Adafruit_BMP085 bmp;
  
void setup() {
  Serial.begin(9600);
  bmp.begin();  
      setuplcd();
      //lcddemo();
      Rf24setup();
}
void loop() {
  Rfloop();
  measureSensiron ();
  float pascals=bmp.readPressure();
  float celcius=bmp.readTemperature();
  float altitude=bmp.readAltitude();
  float Hg = pascals * 0.000295333727+.66;
  float Fahrenheit = (celcius * 9.0)/ 5.0 + 32.0; // Convert Celcius to Fahrenheit
  float humidity=getHumidity();
  float dewpoint=getDewpoint();
  int volts=readVcc();
  

   Serial.println("=====================");
   Serial.print("celcius =    ");
  Serial.print(celcius);
  Serial.println(" *C");

  Serial.print("Fahrenheit = ");
  Serial.print(Fahrenheit);
  Serial.println(" *F");
  
  Serial.print("Pressure =   ");
  Serial.print(pascals);
  Serial.println(" Pa");
  Serial.print("Pressure =   ");
  Serial.print(Hg);
  Serial.println(" Hg");
  //Serial.print("Humidity=    ");

  //Serial.print(getHumidity());
  //Serial.println(" %");
  Serial.print("Temperature: ");
  Serial.print(getTemperature());
  Serial.print(" C, Humidity: ");
  Serial.print(humidity);
  Serial.print(" %, Dewpoint: ");
  Serial.print(dewpoint);
  Serial.println(" C");
  
  
  

  // Calculate altitude assuming 'standard' barometric
  // pressure of 1013.25 millibar = 101325 Pascal
  Serial.print("Altitude =   ");
  Serial.print(altitude);
  Serial.println(" meters");
  Serial.print("Voltage =    ");
  Serial.println(volts);
 //csv  {{P,pascals}, {C,celcius},{P,pascals},{H,Hg}}
  Serial.print(",P:");
  Serial.print(pascals);  
  Serial.print(",C:");
  Serial.print(celcius);
  Serial.print(",F:");
  Serial.print(Fahrenheit);
  Serial.print(",G:");
  Serial.print(Hg);
  Serial.print(",V:");
  Serial.print(volts);
  Serial.print(",A:");
  Serial.print(altitude);
  Serial.print(",%:");
  Serial.print(humidity);
  Serial.print(",D:");
  Serial.print(dewpoint);
  Serial.println("");
 
// you can get a more precise measurement of altitude
  // if you know the current sea level pressure which will
  // vary with weather and such. If it is 1015 millibars
  // that is equal to 101500 Pascals.
    lcdtitle();
    //lcdprint( float value,uint8_t width,uint8_t precision,uint8_t x,uint8_t y)
    //lcdPositionCursor(1,0);

    lcdPrintFloat(Fahrenheit,5,1,0,1);
    lcdPrintFloat(Hg,5,2,6,1);
    lcdPrintFloat(getHumidity(),5,1,11,1);
//    lcdprint(readVcc(),12,1);
    delay(15000);
//    lcdprint((float)bmp.readPressure());
}

