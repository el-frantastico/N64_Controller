using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using N64ControllerProject;

namespace N64ControllerProject
{
    class ControllerListener
    {
        //private Dictionary<string, PictureBox> buttons;
        private SerialPort serialPort;
        private ControllerEventHandler controllerEvents;
        private N64GUI MainForm;

        public ControllerListener(N64GUI MainForm, SerialPort serialPort)
        {
            this.MainForm = MainForm;
            this.serialPort = serialPort;
            this.controllerEvents = new ControllerEventHandler();
            controllerEvents.InitializeDefaultSettings(MainForm);
        }
        
        public void start()
        {
            listen();
        }
        /// <summary>
        /// The listen() function will continue listening through the serial
        /// port and firing off signals 
        /// </summary>
        private void listen()
        {
            while (true)
            {
                while (!stop() && !pause())
                {
                    //flush any data that might have come in after a time-out last iteration
                    serialPort.DiscardInBuffer();

                    //Serial Port Get
                    serialPort.Write("!");
                    //Serial Port Read
                    char[] signal = new char[10];

                    UInt32 data;

                    try
                    {
                        serialPort.Read(signal, 0, 10);
                        data = Convert.ToUInt32(new String(signal), 10);
                    }
                    catch (Exception)
                    {
                        //exceptions thrown on time-outs
                        data = 0;
                    }

                    CheckCButtons(data);
                    CheckAButton(data);
                    CheckBButton(data);
                    CheckLButton(data);
                    CheckRButton(data);
                    CheckStartButton(data);
                    CheckZButton(data);
                    CheckDPadButtons(data);
                    CheckAnalog(data);
                }
            }
        }

        private void UpdateButtonsPressed(UInt32 signal)
        {
            ControllerEventHandler.buttonsPressed["a"]
                = ((0x1 << 0) & signal) > 0;
        }
        /// <summary>
        /// The following are private methods that parse the signal and then
        /// invoke ShowButton on the N64GUI form.
        /// </summary>
        /// <param name="signal"></param>
        private void CheckCButtons(UInt32 signal)
        {
            //TODO: This must be changed to see what signal is doing.
            bool cUpPressed = ((0x1 << 12) & signal) > 0;
            bool cDownPressed = ((0x1 << 13) & signal) > 0;
            bool cLeftPressed = ((0x1 << 14) & signal) > 0;
            bool cRightPressed = ((0x1 << 15) & signal) > 0;

            controllerEvents.sendKey("cUp", cUpPressed);
            controllerEvents.sendKey("cDown", cDownPressed);
            controllerEvents.sendKey("cLeft", cLeftPressed);
            controllerEvents.sendKey("cRight", cRightPressed);

            //PictureBox currentBox = MainForm.picBoxDict["cUp"];
            //if (currentBox.InvokeRequired)
            //{
                try
                {
                    MainForm.picBoxDict["cUp"].Invoke(new MethodInvoker(
                        delegate ()
                        {
                            MainForm.ShowButton("cUp", cUpPressed); 
                        }
                    ));
                    MainForm.picBoxDict["cDown"].Invoke(new MethodInvoker(
                        delegate ()
                        {
                            MainForm.ShowButton("cDown", cDownPressed);
                        }
                    ));
                    MainForm.picBoxDict["cLeft"].Invoke(new MethodInvoker(
                        delegate ()
                        {
                            MainForm.ShowButton("cLeft", cLeftPressed);
                        }
                    ));
                    MainForm.picBoxDict["cRight"].Invoke(new MethodInvoker(
                        delegate ()
                        {
                            MainForm.ShowButton("cRight", cRightPressed);
                        }
                    ));
                }
                catch (Exception)
                {
                        //Do Nothing
                }
            //}
            //else
            //{
            //    currentBox.Visible = true;
            //}
        }
        private void CheckAButton(UInt32 signal)
        {
            bool aPressed = ((0x1 << 0) & signal) > 0;

            controllerEvents.sendKey("a", aPressed);

            try
            {
                MainForm.picBoxDict["a"].Invoke(new MethodInvoker(
                    delegate ()
                    {
                        MainForm.ShowButton("a", aPressed);
                    }
                ));
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }
        private void CheckBButton(UInt32 signal)
        {
            bool bPressed = ((0x1 << 1) & signal) > 0;
            controllerEvents.sendKey("b", bPressed);

            try
            {
                MainForm.picBoxDict["b"].Invoke(new MethodInvoker(
                    delegate ()
                    {
                        MainForm.ShowButton("b", bPressed);
                    }
                ));
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }
        private void CheckLButton(UInt32 signal)
        {
            bool lPressed = ((0x1 << 10) & signal) > 0;
            controllerEvents.sendKey("l", lPressed);

            try
            {
                MainForm.picBoxDict["l"].Invoke(new MethodInvoker(
                    delegate ()
                    {
                        MainForm.ShowButton("l", lPressed);
                    }
                ));
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }
        private void CheckRButton(UInt32 signal)
        {
            bool rPressed = ((0x1 << 11) & signal) > 0;
            controllerEvents.sendKey("r", rPressed);


            try
            {
                MainForm.picBoxDict["r"].Invoke(new MethodInvoker(
                    delegate ()
                    {
                        MainForm.ShowButton("r", rPressed);
                    }
                ));
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }
        private void CheckStartButton(UInt32 signal)
        {
            bool startPressed = ((0x1 << 3) & signal) > 0;
            controllerEvents.sendKey("start", startPressed);

            try
            {
                MainForm.picBoxDict["start"].Invoke(new MethodInvoker(
                    delegate ()
                    {
                        MainForm.ShowButton("start", startPressed);
                    }
                ));
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }
        private void CheckZButton(UInt32 signal)
        {
            bool zPressed = ((0x1 << 2) & signal) > 0;
            controllerEvents.sendKey("z", zPressed);

            try
            {
                MainForm.picBoxDict["z"].Invoke(new MethodInvoker(
                    delegate ()
                    {
                        MainForm.ShowButton("z", zPressed);
                    }
                ));
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }
        /// <summary>
        /// D-Pad buttons require a little extra code and care, these will
        /// methods make them cleaner.
        /// </summary>
        /// <param name="signal"></param>
        private void CheckDPadButtons(UInt32 signal)
        {
            bool dPadUpPressed = ((0x1 << 4) & signal) > 0;
            bool dPadDownPressed = ((0x1 << 5) & signal) > 0;
            bool dPadLeftPressed = ((0x1 << 6) & signal) > 0;
            bool dPadRightPressed = ((0x1 << 7) & signal) > 0;

            try
            {
                if (dPadUpPressed && !dPadLeftPressed && !dPadRightPressed)
                {
                    DPadUp();
                    controllerEvents.sendKey("dPadUp", true);
                    controllerEvents.sendKey("dPadDown", false);
                    controllerEvents.sendKey("dPadLeft", false);
                    controllerEvents.sendKey("dPadRight", false);
                }
                else if (dPadUpPressed && dPadLeftPressed)
                {
                    DPadUpLeft();
                    controllerEvents.sendKey("dPadUp", true);
                    controllerEvents.sendKey("dPadDown", false);
                    controllerEvents.sendKey("dPadLeft", true);
                    controllerEvents.sendKey("dPadRight", false);
                }
                else if (dPadUpPressed && dPadRightPressed)
                {
                    DPadUpRight();
                    controllerEvents.sendKey("dPadUp", true);
                    controllerEvents.sendKey("dPadDown", false);
                    controllerEvents.sendKey("dPadLeft", false);
                    controllerEvents.sendKey("dPadRight", true);
                }
                else if (dPadLeftPressed && !dPadUpPressed && !dPadDownPressed)
                {
                    DPadLeft();
                    controllerEvents.sendKey("dPadUp", false);
                    controllerEvents.sendKey("dPadDown", false);
                    controllerEvents.sendKey("dPadLeft", true);
                    controllerEvents.sendKey("dPadRight", false);
                }
                else if (dPadRightPressed && !dPadUpPressed && !dPadDownPressed)
                {
                    DPadRight();
                    controllerEvents.sendKey("dPadUp", false);
                    controllerEvents.sendKey("dPadDown", false);
                    controllerEvents.sendKey("dPadLeft", false);
                    controllerEvents.sendKey("dPadRight", true);
                }
                else if (dPadLeftPressed && dPadDownPressed)
                {
                    DPadDownLeft();
                    controllerEvents.sendKey("dPadUp", false);
                    controllerEvents.sendKey("dPadDown", true);
                    controllerEvents.sendKey("dPadLeft", true);
                    controllerEvents.sendKey("dPadRight", false);
                }
                else if (dPadRightPressed && dPadDownPressed)
                {
                    DPadDownRight();
                    controllerEvents.sendKey("dPadUp", false);
                    controllerEvents.sendKey("dPadDown", true);
                    controllerEvents.sendKey("dPadLeft", false);
                    controllerEvents.sendKey("dPadRight", true);
                }
                else if (dPadDownPressed && !dPadLeftPressed && !dPadRightPressed)
                {
                    DPadDown();
                    controllerEvents.sendKey("dPadUp", false);
                    controllerEvents.sendKey("dPadDown", true);
                    controllerEvents.sendKey("dPadLeft", false);
                    controllerEvents.sendKey("dPadRight", false);
                }
                else
                {
                    HideDPad();
                    controllerEvents.sendKey("dPadUp", false);
                    controllerEvents.sendKey("dPadDown", false);
                    controllerEvents.sendKey("dPadLeft", false);
                    controllerEvents.sendKey("dPadRight", false);
                }
            }
            catch (Exception) { /*Do Nothing*/ }
        }
        private void HideDPad()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        private void DPadUp()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", true);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        private void DPadUpLeft()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", true);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        private void DPadUpRight()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", true);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        private void DPadLeft()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", true);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        private void DPadRight()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", true);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        private void DPadDownLeft()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", true);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        private void DPadDownRight()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", false);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", true);
                }
            ));
        }
        private void DPadDown()
        {
            MainForm.picBoxDict["dPadUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUp", false);
                }
            ));
            MainForm.picBoxDict["dPadUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadUpRight", false);
                }
            ));
            MainForm.picBoxDict["dPadLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadRight", false);
                }
            ));
            MainForm.picBoxDict["dPadDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDown", true);
                }
            ));
            MainForm.picBoxDict["dPadDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownLeft", false);
                }
            ));
            MainForm.picBoxDict["dPadDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("dPadDownRight", false);
                }
            ));
        }
        /// <summary>
        /// Analog stick requires a little extra code and care, thes will make
        /// the methods cleaner.
        /// </summary>
        /// <param name="signal"></param>
        private void CheckAnalog(UInt32 signal)
        {
            SByte rawX = (SByte)((signal >> 16) & 0xFF);
            SByte rawY = (SByte)((signal >> 24) & 0xFF);

            try
            {
                if (   rawY >= ControllerEventHandler.yThreshold 
                    && Math.Abs(rawX) < ControllerEventHandler.xThreshold)
                {
                    AnalogUp();
                    controllerEvents.sendKey("analogUp", true);
                    controllerEvents.sendKey("analogDown", false);
                    controllerEvents.sendKey("analogLeft", false);
                    controllerEvents.sendKey("analogRight", false);
                }
                else if (   rawY >= ControllerEventHandler.yThreshold
                         && rawX < -ControllerEventHandler.xThreshold)
                {
                    AnalogUpLeft();
                    controllerEvents.sendKey("analogUp", true);
                    controllerEvents.sendKey("analogDown", false);
                    controllerEvents.sendKey("analogLeft", true);
                    controllerEvents.sendKey("analogRight", false);
                }
                else if (   rawY >= ControllerEventHandler.yThreshold
                         && rawX >= ControllerEventHandler.xThreshold)
                {
                    AnalogUpRight();
                    controllerEvents.sendKey("analogUp", true);
                    controllerEvents.sendKey("analogDown", false);
                    controllerEvents.sendKey("analogLeft", false);
                    controllerEvents.sendKey("analogRight", true);
                }
                else if (   Math.Abs(rawY) < ControllerEventHandler.yThreshold
                         && rawX < -ControllerEventHandler.xThreshold)
                {
                    AnalogLeft();
                    controllerEvents.sendKey("analogUp", false);
                    controllerEvents.sendKey("analogDown", false);
                    controllerEvents.sendKey("analogLeft", true);
                    controllerEvents.sendKey("analogRight", false);
                }
                else if (   Math.Abs(rawY) < ControllerEventHandler.yThreshold
                         && rawX >= ControllerEventHandler.xThreshold)
                {
                    AnalogRight();
                    controllerEvents.sendKey("analogUp", false);
                    controllerEvents.sendKey("analogDown", false);
                    controllerEvents.sendKey("analogLeft", false);
                    controllerEvents.sendKey("analogRight", true);
                }
                else if (   rawY < -ControllerEventHandler.yThreshold
                         && rawX < -ControllerEventHandler.xThreshold)
                {
                    AnalogDownLeft();
                    controllerEvents.sendKey("analogUp", false);
                    controllerEvents.sendKey("analogDown", true);
                    controllerEvents.sendKey("analogLeft", true);
                    controllerEvents.sendKey("analogRight", false);
                }
                else if (   rawY < -ControllerEventHandler.yThreshold
                         && rawX >= ControllerEventHandler.xThreshold)
                {
                    AnalogDownRight();
                    controllerEvents.sendKey("analogUp", false);
                    controllerEvents.sendKey("analogDown", true);
                    controllerEvents.sendKey("analogLeft", false);
                    controllerEvents.sendKey("analogRight", true);
                }
                else if (rawY < -ControllerEventHandler.yThreshold
                         && Math.Abs(rawX) < ControllerEventHandler.xThreshold)
                {
                    AnalogDown();
                    controllerEvents.sendKey("analogUp", false);
                    controllerEvents.sendKey("analogDown", true);
                    controllerEvents.sendKey("analogLeft", false);
                    controllerEvents.sendKey("analogRight", false);
                }
                else
                {
                    AnalogDefault();
                    controllerEvents.sendKey("analogUp", false);
                    controllerEvents.sendKey("analogDown", false);
                    controllerEvents.sendKey("analogLeft", false);
                    controllerEvents.sendKey("analogRight", false);
                }
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }
        private void AnalogDefault()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);
                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", true);
                }
            ));
        }
        private void AnalogUp()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", true);
                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        private void AnalogUpLeft()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);
                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", true);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        private void AnalogUpRight()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);

                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", true);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        private void AnalogLeft()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);

                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", true);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        private void AnalogRight()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);

                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", true);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        private void AnalogDownLeft()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);

                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", true);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        private void AnalogDownRight()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);

                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", false);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", true);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        private void AnalogDown()
        {
            MainForm.picBoxDict["analogUp"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUp", false);

                }
            ));
            MainForm.picBoxDict["analogUpLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpLeft", false);
                }
            ));
            MainForm.picBoxDict["analogUpRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogUpRight", false);
                }
            ));
            MainForm.picBoxDict["analogLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogLeft", false);
                }
            ));
            MainForm.picBoxDict["analogRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogRight", false);
                }
            ));
            MainForm.picBoxDict["analogDown"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDown", true);
                }
            ));
            MainForm.picBoxDict["analogDownLeft"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownLeft", false);
                }
            ));
            MainForm.picBoxDict["analogDownRight"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogDownRight", false);
                }
            ));
            MainForm.picBoxDict["analogCenter"].Invoke(new MethodInvoker(
                delegate ()
                {
                    MainForm.ShowButton("analogCenter", false);
                }
            ));
        }
        /// <summary>
        /// The pause() will always return false, its puerpose is to 
        /// momentarily wait to see if all parameters are ready or have changed
        /// before continuing listening.
        /// </summary>
        /// <returns> bool always true </returns>
        private bool pause()
        {
            while (!serialPort.IsOpen) {/*if closed wait until open*/}
            return false;
        }
        /// <summary>
        /// The stop() button will stop the listening process if needed.
        /// </summary>
        /// <returns> bool if the whole process should stop</returns>
        private bool stop()
        {
            return false;
        }
    }
}
