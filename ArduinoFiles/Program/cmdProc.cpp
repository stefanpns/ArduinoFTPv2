#include "cmdProc.h"

bool tryParseInt(char *s, int &outResult) {
  if (!tryParseDec(s, outResult))
    return false;
  else
    return true;
}

bool tryParseDec(char *s, int &outResult, bool allowMinus = false) {
  outResult = 0;
  int n = strlen(s);
  int sign = 1;
  int a = 0;
  if ((allowMinus) && (s[0] == '-')) {
    sign = -1;
    a = 1;
  }
  for (int i = a; i < n; i++) {
    if (isDigit(s[i])) {
      outResult = 10 * outResult + sign * (s[i] - '0');
    }
    else
      return false;
  }
  return true;
}
