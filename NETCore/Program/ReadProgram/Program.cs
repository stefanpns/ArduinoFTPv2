using System;
using System.IO.Ports;
using System.Threading;
using System.Text;

namespace coreapp
{

    
    


    class Program
    {


        static void Main(string[] args)
        {  
            Thread t_read = new Thread(new ThreadStart(ThreadProcRead));
            t_read.Start();
        }

         public static void ThreadProcRead() {

            
             SerialTalk _serialTalk = new SerialTalk();
            _serialTalk.setup();


            ArduinoIW arduinoIW = new ArduinoIW((PortTalk) _serialTalk);
            TalkBuffer sb = new TalkBuffer((PortTalk)_serialTalk);


            bool waitingForTheChain = false;


            while (true)
            {

                try
                {
                    
                    CommandObject res = null;

                    sb.loop();

                    if(sb.isavailable()) {

                        while(sb.isnext() ) {

                            res = arduinoIW.interpret(sb);
                            
                            if ( res == null ) 
                                continue;

                                   
                            if ( res.isValid() ) {

                                if ( res.isComplete()) {

                                    if ( waitingForTheChain ) {

                                        
                                        if ( arduinoIW.getPreviousCommandObject() != null ) {

                                            CommandObject previousCmdObject = arduinoIW.getPreviousCommandObject();

                                            
                                             if ( previousCmdObject.checkChainCommandName(res.getName()) ) {

                                                if ( previousCmdObject.checkChainCmdResult(res) == true) {
                                                    previousCmdObject.print();
                                                    Console.WriteLine(previousCmdObject.getChainSuccessMessage());
                                                } else {
                                                    Console.WriteLine(previousCmdObject.getChainUnsuccessMessage());
                                                }
                                                    Console.WriteLine();


                                            }else {
                                                Console.WriteLine("The new command result is not compatible with the previous command result.");
                                                Console.WriteLine("The chain cannot be continued.");
                                                Console.WriteLine("The new command result is:");
                                                res.print();
                                                Console.WriteLine();
                                            }
                                        }else {
                                            Console.WriteLine("Program.cs: Unhandled case 1");
                                        }

                                        waitingForTheChain = false;

                                    } else if ( res.isWaitingForTheChain() ) {

                                        waitingForTheChain = true;
                                        arduinoIW.WriteLine(res.getChainCommand());

                                    } else {
                                        res.print();
                                        Console.WriteLine();
                                    }



                                } 

                            } else  {

                                Console.WriteLine("Program.cs: We have recieved a command result that is not valid.");
                                Console.WriteLine("The command was ["+res.getName()+"].");
                                
                                Program.RewindBuffer(sb);
                                
                            }
                             
                            arduinoIW.setPreviousCommandObject(res);

                        }

                        sb.clear();
                    }

                    

                } finally {
                }

               
            }
        }


        private static void RewindBuffer(TalkBuffer sb) {

            while( sb.isnext() && sb.read() != '$' );
            sb.read();

        }


    }
}
