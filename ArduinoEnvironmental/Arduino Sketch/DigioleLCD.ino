 #include <DigoleSerialDisp.h>
 #include <Wire.h>
//DigoleSerialDisp_UART mydisp(&Serial, 9600);  //Pin 1(TX)on arduino to RX on module
DigoleSerialDisp_I2C mydisp(&Wire,'\x27');  //SDA (data line) is on analog input pin 4, and SCL (clock line) is on analog input pin 5
//DigoleSerialDisp_SPI mydisp(8,9,10);  //Pin 8: data, 9:clock, 10: SS, you can assign 255 to SS, and hard ground SS pin on module
#define LCDCol 2
#define LCDRow 16
void setuplcd()
{
  //Due to zero in a string was treated as a end string mark, so if you want send 0 to device, you need send it seperately using print method
  //Text Function
  mydisp.begin();
}
void lcdtitle(){
 // mydisp.print("STCR\x14\x04\x80\xC0\x94\xD4");  //set up Text Cols and Rows and Col addresses of LCD
  //delay(5000);
  //mydisp.print("CS0");  //disable cursor
  mydisp.print("CL");  //CLear screen
  //Text Function
  //lcdPositionCursor(0,0);
  mydisp.print("TP");  //set Text Position to
  mydisp.print((uint8_t)0); //x=0;
  mydisp.print((uint8_t)0); //y=0;
  //////////////  //1234567890123456
  mydisp.println("TT Temp Press Hum");  //display TexT: "Hello World!"
  //mydisp.print("CS0");  //enable cursor
}

void lcdPositionCursor(uint8_t coll, uint8_t row)
{
  mydisp.print("TP");  //set Text Position to
  mydisp.print((uint8_t)row); //x=0;
  mydisp.print((uint8_t)coll); //y=0;

}
void lcdprint(char* p){
  mydisp.print("TT"); //y=0;
  mydisp.println(p); // Serial.read() reads always 1 character
 
}

void lcdPrintFloat( float value,uint8_t width,uint8_t precision,uint8_t row,uint8_t coll){
  //mydisp.print("CS0");  //disable cursor
  //mydisp.print("CL");  //CLear screen
  lcdPositionCursor(coll,row);
  char ascii[(width+precision)+1];// needs to be this big to hold a type float
  //dtostrf(FLOAT,WIDTH,PRECSISION,BUFFER);
  dtostrf(value,width,precision,ascii);
  mydisp.print("TT");
  mydisp.println(ascii);  
}
void lcdprint( int value,uint8_t row,uint8_t coll){
  //mydisp.print("CS0");  //disable cursor
  //mydisp.print("CL");  //CLear screen
  lcdPositionCursor(coll,row);
  mydisp.print("TT");
  char ascii[10];// needs to be this big to hold a type float
  itoa((int)value,ascii,10);
  mydisp.println(ascii);  
}
void lcdprint( long value,int8_t row,uint8_t coll){
  //mydisp.print("CS0");  //disable cursor
  //mydisp.print("CL");  //CLear screen
  lcdPositionCursor(coll,row);
  mydisp.print("TT");
  char ascii[10];// needs to be this big to hold a type float
  itoa((long)value,ascii,10);
  mydisp.println(ascii);  
  }
