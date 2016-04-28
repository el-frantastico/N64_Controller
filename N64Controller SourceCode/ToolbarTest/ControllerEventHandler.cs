using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace N64ControllerProject
{
    class ControllerEventHandler
    {
        public static Dictionary<string, char> buttonKeyDict = new Dictionary<string, char>();
        public static bool emulatorMode = true;
        public static int xThreshold = 40;
        public static int yThreshold = 40;

        public static Dictionary<string, bool> buttonsPressed;
        private Dictionary<string, bool> buttonsPressedBefore;
        private N64GUI MainForm;
        //[DllImport("User32.Dll", EntryPoint = "PostMessage")]
        //static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public void InitializeDefaultSettings(N64GUI mainForm)
        {
            MainForm = mainForm;

            buttonKeyDict.Add("a", 'a');
            buttonKeyDict.Add("b", 'b');

            buttonKeyDict.Add("l", 'l');
            buttonKeyDict.Add("r", 'r');

            buttonKeyDict.Add("z", 'z');

            buttonKeyDict.Add("start", 's');

            buttonKeyDict.Add("cUp", 'e');
            buttonKeyDict.Add("cDown", 'q');
            buttonKeyDict.Add("cLeft", 't');
            buttonKeyDict.Add("cRight", 'y');

            buttonKeyDict.Add("dPadUp", 'h');
            buttonKeyDict.Add("dPadDown", 'j');
            buttonKeyDict.Add("dPadLeft", 'k');
            buttonKeyDict.Add("dPadRight", 'g');

            buttonKeyDict.Add("analogUp", 'v');
            buttonKeyDict.Add("analogDown", 'c');
            buttonKeyDict.Add("analogLeft", 'n');
            buttonKeyDict.Add("analogRight", 'm');

            buttonsPressed = new Dictionary<string, bool>();
            buttonsPressedBefore = new Dictionary<string, bool>();
            foreach (string key in buttonKeyDict.Keys)
            {
                buttonsPressed.Add(key, false);
                buttonsPressedBefore.Add(key, false);
            }
        }

        public void sendKey(string button)
        {
            if(emulatorMode)
            {
                Keyboard.PressKey((Keys)Enum.Parse(typeof(Keys), buttonKeyDict[button].ToString(), true));
            }
        }
        public void sendKey(string button, bool isPressed)
        {
            if (emulatorMode)
            {
                if (isPressed && !buttonsPressedBefore[button])
                {
                    Keyboard.HoldKey((Keys)Enum.Parse(typeof(Keys), buttonKeyDict[button].ToString(), true));
                    buttonsPressedBefore[button] = true;
                }
                else if (isPressed && buttonsPressedBefore[button])
                {
                    Keyboard.HoldKey((Keys)Enum.Parse(typeof(Keys), buttonKeyDict[button].ToString(), true));
                }
                else
                {
                    Keyboard.ReleaseKey((Keys)Enum.Parse(typeof(Keys), buttonKeyDict[button].ToString(), true));
                    buttonsPressedBefore[button] = false;
                }
            }
        }
    }

    public class Keyboard
    {
        public Keyboard()
        {
        }

        [StructLayout(LayoutKind.Explicit, Size = 28)]
        public struct Input
        {
            [FieldOffset(0)]
            public uint type;
            [FieldOffset(4)]
            public KeyboardInput ki;
        }

        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public long time;
            public uint dwExtraInfo;
        }

        const int KEYEVENTF_KEYUP = 0x0002;
        const int INPUT_KEYBOARD = 1;

        [DllImport("user32.dll")]
        public static extern int SendInput(uint cInputs, ref Input inputs, int cbSize);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        static extern ushort MapVirtualKey(int wCode, int wMapType);

        public static bool IsKeyDown(Keys key)
        {
            return (GetKeyState((int)key) & -128) == -128;
        }

        public static void HoldKey(Keys vk)
        {
            ushort nScan = MapVirtualKey((ushort)vk, 0);

            Input input = new Input();
            input.type = INPUT_KEYBOARD;
            input.ki.wVk = (ushort)vk;
            input.ki.wScan = nScan;
            input.ki.dwFlags = 0;
            input.ki.time = 0;
            input.ki.dwExtraInfo = 0;
            SendInput(1, ref input, Marshal.SizeOf(input)).ToString();
        }

        public static void ReleaseKey(Keys vk)
        {
            ushort nScan = MapVirtualKey((ushort)vk, 0);

            Input input = new Input();
            input.type = INPUT_KEYBOARD;
            input.ki.wVk = (ushort)vk;
            input.ki.wScan = nScan;
            input.ki.dwFlags = KEYEVENTF_KEYUP;
            input.ki.time = 0;
            input.ki.dwExtraInfo = 0;
            SendInput(1, ref input, Marshal.SizeOf(input));
        }

        public static void PressKey(Keys vk)
        {
            HoldKey(vk);
            ReleaseKey(vk);
        }
    }
}
