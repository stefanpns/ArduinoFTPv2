using System;
using System.IO.Ports;
using System.Threading;


namespace coreapp
{

    class TalkBuffer {


        //  SerialBuf v1.0.002 
        //  17.12.2020 
        //  Djordje Herceg 


        const int maxbuf = 1000;                          // velicina bafera
        char[] buffer = new char[maxbuf];
        bool overflow;
        int mils;
        int stopInterval = 50;             // interval nakon zadnjeg primljenog karaktera kada se poruka racuna kao zavrsena
        bool finished;
        int length;                          // duzina sadrzaja u baferu
        int position;                        // trenutna pozicija u baferu
        PortTalk sp;


        public TalkBuffer(PortTalk sp) { 
            this.sp = sp;
            init(); 
        } 

        private void init(){ 
            clear(); 
        } 


        public void clear() { 

            overflow = false; 
            finished = false; 
            length = 0; 
            position = 0; 
            buffer[0] = '\0'; // null-terminate 
            mils = DateTime.Now.Millisecond; 

        } 


          

        public void loop(){ 

            if (finished) { 
                return; 
            } 

            if ((length > 0) && (DateTime.Now.Millisecond - mils > stopInterval)) { 

                finished = true; 
                return; 

            } 

          

            while (sp.bytesToRead() > 0) { 
                
                mils = DateTime.Now.Millisecond; 
                int r = sp.readChar(); 

                if (length + 1 < maxbuf) { 

                    if (r > -1) { 

                        buffer[length++] = (char)r; 
                        buffer[length] = '\0'; // null-terminate 

                    } 

                } else { 
                    overflow = true; 
                    break;
                } 

            } 

        } 

      

          

          

        public bool isnext() { 

            return (position + 1) < length; 

        } 

          

        public int read() { 

            if (!isnext()) 
                return -1; 

          

            return buffer[position++]; 

        } 
        public bool isoverflow() { 

            return overflow; 

        } 
        public bool isavailable() { 

            return finished; 

        } 


    }


}
