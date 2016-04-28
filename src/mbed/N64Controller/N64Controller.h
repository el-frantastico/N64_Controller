#ifndef __N64Controller__
#define __N64Controller__

#include "mbed.h"

class N64Controller
{
    public:
        /*
         * Passive Mode makes it so the mbed does not request data from the n64 controller. This allows the controller
         * to be plugged into a real n64 and have the signal split to the mbed as well. The mbed will still read the
         * incoming inputs in passive mode, but all of the data requests will come from the n64 itself.
         */
        N64Controller(PinName pin, bool passiveMode);
        
        bool a();
        bool b();
        bool l();
        bool r();
        bool z();
        bool start();
        
        bool dUp();
        bool dDown();
        bool dLeft();
        bool dRight();
        
        bool cUp();
        bool cDown();
        bool cLeft();
        bool cRight();
        
        int8_t joyX();
        int8_t joyY();
        
        void setPassiveMode(bool);
        
        uint32_t getInputData();
        
    private:
        void poll();
        void writeByte(char data);
        void readBytes(char* buffer, int numBytes);
        void myWait_us(int numMicroseconds);
        
        DigitalInOut pin;
        Ticker ticker;
        uint32_t inputData;
        bool passiveMode;
        Serial debug;
        
        #define RESET 0xFF
        #define GET_DATA 0x01
        
        #define POLLING_INTERVAL 10000 //0.01 seconds

                #define A_MASK       (0x1 << 0)
        #define B_MASK       (0x1 << 1)
        #define Z_MASK       (0x1 << 2)
        #define START_MASK   (0x1 << 3)
        #define D_UP_MASK    (0x1 << 4)
        #define D_DOWN_MASK  (0x1 << 5)
        #define D_LEFT_MASK  (0x1 << 6)
        #define D_RIGHT_MASK (0x1 << 7)
        
        //bits 8, 9 unused
        
        #define L_MASK       (0x1 << 10)
        #define R_MASK       (0x1 << 11)
        #define C_UP_MASK    (0x1 << 12)
        #define C_DOWN_MASK  (0x1 << 13)
        #define C_LEFT_MASK  (0x1 << 14)
        #define C_RIGHT_MASK (0x1 << 15)
        
        #define X_R_SHIFT    16
        #define Y_R_SHIFT    24
};
 
#endif   