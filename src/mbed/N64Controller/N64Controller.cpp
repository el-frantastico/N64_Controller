#include "N64Controller.h"

extern "C" void my_wait_us_asm(int n);

N64Controller::N64Controller(PinName pinNumber, bool passiveMode)
    :pin(pinNumber),
    debug(USBTX, USBRX)
{    
    this->passiveMode = passiveMode;
    inputData = 0;
    
    //open drain means that the pin never actually drives the signal high.
    //the pull-up resistor drives it high, and the pin can "drain" the signal
    //to drive it low. other internet sources say this is the best way to
    //set up the n64 data pin, and that driving it high with the mbed
    //could damage the mbed or the controller
    pin.mode(OpenDrain);
    
    writeByte(RESET); //not sure if this is mandatory... but it resets the "neutral" stick position to the current position
    
    ticker.attach_us(this, &N64Controller::poll, POLLING_INTERVAL);
}

void N64Controller::poll()
{
    writeByte(GET_DATA);
    readBytes((char *)(&inputData), 4);
    
    //due to order we are reading bits in vs. order we are storing them,
    //X and Y values need their bits reversed (all other values are single bit,
    //so there is no need to reverse them)
    
    //reverse bits in X byte (bit-magic was taken from stack overflow... it simply reverses bits)
    char *xByte = ((char *)(&inputData)) + 2;
    *xByte = (*xByte & 0xF0) >> 4 | (*xByte & 0x0F) << 4;
    *xByte = (*xByte & 0xCC) >> 2 | (*xByte & 0x33) << 2;
    *xByte = (*xByte & 0xAA) >> 1 | (*xByte & 0x55) << 1;
    
    //reverse bits in Y byte
    char *yByte = ((char *)(&inputData)) + 3;
    *yByte = (*yByte & 0xF0) >> 4 | (*yByte & 0x0F) << 4;
    *yByte = (*yByte & 0xCC) >> 2 | (*yByte & 0x33) << 2;
    *yByte = (*yByte & 0xAA) >> 1 | (*yByte & 0x55) << 1;
}


bool N64Controller::a() { return ((inputData & (A_MASK)) > 0); }
bool N64Controller::b() { return (inputData & (B_MASK)) > 0; }
bool N64Controller::l() { return (inputData & (L_MASK)) > 0; }
bool N64Controller::r() { return (inputData & (R_MASK)) > 0; }
bool N64Controller::z() { return (inputData & (Z_MASK)) > 0; }
bool N64Controller::start() { return (inputData & (START_MASK)) > 0; }

bool N64Controller::dUp() { return (inputData & (D_UP_MASK)) > 0; }
bool N64Controller::dDown() { return (inputData & (D_DOWN_MASK)) > 0; }
bool N64Controller::dLeft() { return (inputData & (D_LEFT_MASK)) > 0; }
bool N64Controller::dRight() { return (inputData & (D_RIGHT_MASK)) > 0; }

bool N64Controller::cUp() { return (inputData & (C_UP_MASK)) > 0; }
bool N64Controller::cDown() { return (inputData & (C_DOWN_MASK)) > 0; }
bool N64Controller::cLeft() { return (inputData & (C_LEFT_MASK)) > 0; }
bool N64Controller::cRight() { return (inputData & (C_RIGHT_MASK)) > 0; }

int8_t N64Controller::joyX() { return (inputData >> X_R_SHIFT) & 0xFF; }
int8_t N64Controller::joyY() { return (inputData >> Y_R_SHIFT) & 0xFF; }

uint32_t N64Controller::getInputData() { return inputData; }


void N64Controller::writeByte(char byte)
{
    __disable_irq();    // Disable Interrupts
    
    for(int bitNumber = 7; bitNumber >= 0; bitNumber--)
    {
        int bit = 0;
        bit = (byte >> bitNumber) & 0x1;
        
        if(bit == 0)
        {
            pin.write(0);
            myWait_us(3);
            pin.write(1);
            myWait_us(1);
        }
        else
        {
            pin.write(0);
            myWait_us(1);
            pin.write(1);
            myWait_us(3);
        }
    }
    
    //end with signal bit
    pin.write(0);
    myWait_us(1);
    pin.write(1);
    
    __enable_irq();     // Enable Interrupts
}

void N64Controller::readBytes(char *buffer, int bytesToRead)
{
    __disable_irq();    // Disable Interrupts
    
    for(int i = 0; i < bytesToRead; i++)
    {
        char byte = 0;
        
        //read 1 byte at a time
        for(int bit = 0; bit < 8; bit++)
        {
            int prevReading = pin.read();
            
            //read until falling edge
            while(true)
            {
                int reading = pin.read();
                
                if(prevReading == 1 && reading == 0)
                {
                    //falling edge detected. Wait 2 us and read the value in
                    myWait_us(2);
                    byte |= (pin.read() << bit);
                    break;
                }
                
                prevReading = reading;
            }
        }
        
        //Since mbed is little-endian, the MSB is stored last.
        //Since we want the data laid out in the same order it arrived in
        //(i.e., we want A in bit 0), we put the LSB first
        buffer[i] = byte;
    }
        
    __enable_irq();     // Enable Interrupts
}

void N64Controller::myWait_us(int n)
{
    my_wait_us_asm(n);
}
