using System;
using System.IO.Ports;
using System.Threading;

using System.Text;

namespace coreapp
{

    abstract class PortTalk {

        public abstract void setup();
        public abstract void WriteLine(string line);
        public abstract int bytesToRead();
        public abstract int readChar();


    }


    class SerialTalk : PortTalk{

        SerialPort _serialPort = null;

        public override void setup() {

            _serialPort = new SerialPort();
            _serialPort.PortName = "/dev/ttyACM0";//Set your board COM
            _serialPort.BaudRate = 9600;
            _serialPort.Open();


        }

        public override void WriteLine(string line) {
            _serialPort.WriteLine(line);
        }

        
        public override int bytesToRead(){
            return _serialPort.BytesToRead;
        }
        public override int readChar() {
            return _serialPort.ReadChar();
        }

    }


}
