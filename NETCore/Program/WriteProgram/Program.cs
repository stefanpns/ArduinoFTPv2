using System;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Collections.Generic;
namespace ClientWriteConsole
{
    class Program
    {
        private static ConsoleKeyInfo keyinfo;
        
        static SerialPort _serialPort;

        static void Main(string[] args)
        {  
            
            _serialPort = new SerialPort();
            _serialPort.PortName = "/dev/ttyACM0";//Set your board COM
            _serialPort.BaudRate = 9600;
            _serialPort.Open();
        


            Thread t_write = new Thread(new ThreadStart(ThreadProcWrite));
            t_write.Start();
        }
        // obratiti paznju na bafer baud 9600/8 = 1200 b/s 
        // 
        public static void ThreadProcWrite() {
            

            while (true)
            {

                Console.Write("Command to send: ");
                string command = Console.ReadLine();
                
                try
                {
                    _serialPort.WriteLine(command);
                }
                finally
                {
                }

            }
	}

      

    }
}
