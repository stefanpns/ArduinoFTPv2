#ifndef __RETURN_VALUES_H__
#define __RETURN_VALUES_H__





class ReturnValues {

    
  public:
     static void printOK() {Serial.print(F("#OK"));}
     static void printNOTOK() {Serial.print(F("#ERROR"));}
     static void printBlankResultLineEndAndReady() {
      ReturnValues::printResult();
      Serial.println();
      ReturnValues::printEndAndReady();
    }
     static void printEndAndReady() {Serial.println(F("#END"));Serial.println(F("$"));}
     static void printResult() {Serial.print(F(">"));}


  public:
    static void printComment(const __FlashStringHelper* comment) {
      
        ReturnValues::printSuccess(F("1"), F("comment"));
        ReturnValues::printResult();
        Serial.println(comment);
        ReturnValues::printEndAndReady();
        
    }
    
    static void printErrorV2(const char* cmdName, int errno) {

        ReturnValues::printNOTOK();
        Serial.print(F(" "));
        Serial.print(cmdName);
        Serial.print(F(" "));
        Serial.println(errno);
        ReturnValues::printBlankResultLineEndAndReady();
    }

    static void printErrorV2(const __FlashStringHelper* arg, int errno) {

        ReturnValues::printNOTOK();
        Serial.print(F(" "));
        Serial.print(arg);
        Serial.print(F(" "));
        Serial.println(errno);
        ReturnValues::printEndAndReady();
    }

    
  static void printSuccess(const __FlashStringHelper* arg, const __FlashStringHelper* command) {

        ReturnValues::printOK();
        Serial.print(F(" "));
        Serial.print(command);
        Serial.print(F(" "));
        Serial.println(arg);
    }

    static void printSuccess(const char* arg, const char* command) {

        ReturnValues::printOK();
        Serial.print(F(" "));
        Serial.print(command);
        Serial.print(F(" "));
        Serial.println(arg);
    }

     static void printSuccess(const char* parent, const char* child, const char* command) {

        ReturnValues::printOK();
        Serial.print(F(" "));
        Serial.print(command);
        Serial.print(F(" "));
        Serial.print(parent);
        Serial.println(child);
    }
    
};

#endif
