#include "mbed.h"
#include "N64Controller.h"

N64Controller controller(p25, false);

DigitalOut led1(LED1);
DigitalOut led2(LED2);
DigitalOut led3(LED3);
DigitalOut led4(LED4);

Serial pc (USBTX, USBRX);

//Timer timer;

int main() {
    while(1)
    {
        if(pc.getc() == '!')
        {            
            //we had issues sending the raw binary over serial (probably because
            //the serial connection was interpreting them as characters)
            //so instead we will just send the raw ASCII for the number
            //e.g., if inputData is 5267 we will send the string '0000005267'
            //(max uint32 value is 10 digits, so we pad with 0 to ensure we
            //are always sending 10 bytes
            uint32_t data = controller.getInputData();
            char payload[10];
            sprintf(payload, "%010u", data);
            
            for(int i = 0; i < 10; i++)
            {
                pc.putc(payload[i]);
            }
        }
        else
        {
            led1 = 1;
            led2 = 1;
            led3 = 1;
            led4 = 1;
        }
    }
    
    //timer.start();
//        
//    while(1)
//    {
//        if(controller.a())
//        {
//            led1 = 1;
//        }
//        else
//        {
//            led1 = 0;
//        }
//        
//        if(controller.b())
//        {
//            led2 = 1;
//        }
//        else
//        {
//            led2 = 0;
//        }
//        
//        if(controller.l())
//        {
//            led3 = 1;
//        }
//        else
//        {
//            led3 = 0;
//        }
//        
//        if(controller.r())
//        {
//            led4 = 1;
//        }
//        else
//        {
//            led4 = 0;
//        }
//        
//        //prints inputs to serial once every two seconds
//        if(timer.read_ms() > 2000)
//        {            
//            char output [1000];
//            memset(output, 0x0, 1000);
//            int outputIndex = 0;
//            
//            sprintf(&output[outputIndex], "\n\rButtons Pressed:\n\n\r");
//            outputIndex += strlen(&output[outputIndex]);
//            
//            if(controller.a())
//            {
//                sprintf(&output[outputIndex], "\tA\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.b())
//            {
//                sprintf(&output[outputIndex], "\tB\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.z())
//            {
//                sprintf(&output[outputIndex], "\tZ\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.l())
//            {
//                sprintf(&output[outputIndex], "\tL\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.r())
//            {
//                sprintf(&output[outputIndex], "\tR\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.cUp())
//            {
//                sprintf(&output[outputIndex], "\tC-Up\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.cDown())
//            {
//                sprintf(&output[outputIndex], "\tC-Down\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.cLeft())
//            {
//                sprintf(&output[outputIndex], "\tC-Left\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.cRight())
//            {
//                sprintf(&output[outputIndex], "\tC-Right\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.dUp())
//            {
//                sprintf(&output[outputIndex], "\tD-Up\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.dDown())
//            {
//                sprintf(&output[outputIndex], "\tD-Down\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.dLeft())
//            {
//                sprintf(&output[outputIndex], "\tD-Left\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            if(controller.dRight())
//            {
//                sprintf(&output[outputIndex], "\tD-Right\n\r");
//                outputIndex += strlen(&output[outputIndex]);
//            }
//            
//            sprintf(&output[outputIndex], "\nAnalog Position:\n\n\r");
//            outputIndex += strlen(&output[outputIndex]);
//            
//            sprintf(&output[outputIndex], "\tX: %d\n\r", controller.joyX());
//            outputIndex += strlen(&output[outputIndex]);
//            
//            sprintf(&output[outputIndex], "\tY: %d\n\r", controller.joyY());
//            outputIndex += strlen(&output[outputIndex]);
//            
//            sprintf(&output[outputIndex], "\n\r-----------------\n\r");
//            outputIndex += strlen(&output[outputIndex]);
//
//            pc.printf("%s", output);
//            
//            timer.reset();
//        }
//    }
}
