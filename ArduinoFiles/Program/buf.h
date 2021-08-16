#ifndef __BUF_H__
#define __BUF_H__

typedef struct Buf {
  char buf[50];
  int maxlen = 50;
  int len = 0;
  int pos = 0;

  bool Append(char c) {
    if (len < maxlen) {
      
      if ( ((int)c) == 34 ) { 
          return true;
      }
      
      buf[len++] = c;
      return true;
    } else
      return false;
  }

  void Finish() {
    buf[len] = 0;
  }

  void Clear() {
    len = 0;
    pos = 0;
  }
};

#endif
