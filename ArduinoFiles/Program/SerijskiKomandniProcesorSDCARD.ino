/*
   Serijski komandni procesor
   v0.2
   18.11.2020 /// Djordje Herceg

   Program ucitava komande sa serijskog porta i tokenizuje ih.

   * SD card attached to SPI bus as follows:
   ** MOSI - pin 11
   ** MISO - pin 12
   ** CLK - pin 13
   ** CS - pin 4
*/

#include <SdFat.h>
#include "cmdProc.h"
#include "buf.h"
#include "sdCardManipulator.h"

CmdProc Commands;
Buf sbuf;

const int chipSelect = 10;


void setup() {

  Serial.begin(9600);
  while (!Serial) {}  // wait for Serial to be ready
  delay(400);  // catch Due reset problem

  
  ReturnValues::printComment(F("@sd card demo projekat"));
  if (!sd.begin(chipSelect, SPI_HALF_SPEED)) {
      ReturnValues::printErrorV2(F("unable to init communication with SdCard"), 10);
      return;
  }

  
  if (!cwdFile.open("/")) {
    ReturnValues::printComment(F("@Error: unable to open the root"));
    return;
  }
  cwdFile.close();

  Commands.Init(8);   
  Commands.Add(helpStr, cmdHelp, 1, 1);
  Commands.Add(mdStr, cmdMd, 2, 2); 
  Commands.Add(dirStr, cmdDir, 1, 2); 
  Commands.Add(cdStr, cmdCd, 1, 2); 
  Commands.Add(getStr, cmdGet, 2, 5);
  Commands.Add(delStr, cmdDel, 2, 2); 
  Commands.Add(crcStr, cmdCrc, 2, 5); 
  Commands.Add(putStr, cmdPut, 3, 104); 
  
}

int getCmdName(char* buf) {
      char* input = buf;
      int i = 0;
      int len = strlen(input);
      while (i <= len && input[i] != ' ') {
          i++;
      }

      if ( i == len )
        return -1;
      return i;
}

void loop() {
  // read serial input and put it into the buffer, until LF chr(10) is encountered. Ignore chr(13).
  if (Serial.available()) {
    int c = Serial.read();
    if (c == 10) {
      sbuf.Finish();
      int rez = Commands.Parse(sbuf.buf);     // Command processor parses and executes the command by invoking its function. Res is 0 -> OK, -n -> error
      if ( rez != 0 ) {

        int pos = getCmdName(sbuf.buf);
        (sbuf.buf)[pos] = '\0';
        
        ReturnValues::printErrorV2(sbuf.buf, rez);
      }
      sbuf.Clear();
    } else if (c != 13) {
      if (sbuf.Append((char)c) == false){
        ReturnValues::printComment(F("@Buffer overflow. We recommend to start over again and try  not  to exceed buffer storage of totally 50 bytes."));
        sbuf.Clear(); 
      }
    }
  }
}
