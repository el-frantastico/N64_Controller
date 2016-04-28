using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace N64ControllerProject
{
    public partial class N64GUI : Form
    {
        public Dictionary<string, PictureBox> picBoxDict = new Dictionary<string, PictureBox>();
        private Bitmap n64OutlineBitmap;
        private System.Threading.Thread ListenerThread;

        public N64GUI()
        {
            InitializeComponent();
            //Contoller outline
            InitializeOutline();
            InitializeControllerButtons();
            //Serial ports
            InitializeCOMPorts();
            serialPort = new SerialPort();
            serialPort.ReadTimeout = 30;
            //Start controller signals thread
            ListenerThread = new System.Threading.Thread(LoadControllerListener);
            ListenerThread.Start();
            //Settings
            InitializeSettings();
            //Quit & Restart
            //this.restartToolStripMenuItem.Click += restartToolStripMenuItem_Click;
            this.quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
        }
        //// LISTEN THREAD ////
        void LoadControllerListener()
        {
            ControllerListener contollerListener 
                = new ControllerListener(this, serialPort);
            contollerListener.start();
        }
        //// COM PORT CONNECTION ////
        private void InitializeCOMPorts()
        {
            this.searchCOMPortsToolStripMenuItem.Click += searchCOMPortsToolStripMenuItem_Click;
            ListCOMPorts();
        }
        private void searchCOMPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListCOMPorts();
        }
        private void ListCOMPorts()
        {
            //clear current ports or noPorts label
            for (int i = COMPortsToolStripMenu.DropDownItems.Count - 1; i > 1; i--)
            {
                COMPortsToolStripMenu.DropDownItems.RemoveAt(i);
            }

            //search for ports
            string[] ports = SerialPort.GetPortNames();

            //if no ports found, write noPorts label
            if (ports.Length == 0)
            {
                ToolStripMenuItem noPorts = new ToolStripMenuItem();
                noPorts.Text = "No COM Ports Detected";
                noPorts.Enabled = false;

                COMPortsToolStripMenu.DropDownItems.Add(noPorts);
                return;
            }

            //else add all ports found
            foreach (string port in ports)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(port);
                item.Text = port;
                //add event handler when this com port is selected
                item.Click += new EventHandler(selectCOMPort);
                //if this com port is already open, check it
                if (serialPort != null && item.Text == serialPort.PortName)
                {
                    item.Checked = true;
                }
                
                //add to list
                COMPortsToolStripMenu.DropDownItems.Add(item);
            }
        }
        private void selectCOMPort(object sender, EventArgs e)
        {
            //uncheck all other COM ports since you can't have more than one
            for (int i = 2; i < COMPortsToolStripMenu.DropDownItems.Count; i++)
            {
                ToolStripMenuItem item
                    = COMPortsToolStripMenu.DropDownItems[i] as ToolStripMenuItem;

                if (item.Checked == true)
                {
                    serialPort.Close();
                    item.Checked = false;
                }

            }

            ToolStripMenuItem comPortItem = sender as ToolStripMenuItem;
            comPortItem.Checked = true;
            serialPort.PortName = comPortItem.Text;
            serialPort.Open();
            Console.WriteLine(serialPort.PortName);
        }
        //// TEST CONNECTION ////
        private void testConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(serialPort == null)
            {
                Console.WriteLine("Test Connection: FAILED\nSerial port not instantiated.");
                return;
            }

            if(!serialPort.IsOpen)
            {
                Console.WriteLine("Test Connection: FAILED\nSerial port is closed.");
                return;
            }
            
            if(serialPort.PortName == null)
            {
                Console.WriteLine("Test Connection: FAILED\nNo COM port selected.");
                return;
            }

            serialPort.Write("test");
            serialPort.ReadTimeout = 30;
            string echo = serialPort.ReadLine();
            //serialPort.ReadTimeout = XXXX;
            if(echo == "success")
            {
                Console.WriteLine("Test Connection: SUCCESS");
            }
            else
            {
                Console.WriteLine("Test Connection: FAILED\nNo mBED echo received.");
            }
        }
        //// QUIT & RESTART ////
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        //// CONTROLLER OUTLINE ////
        private void InitializeOutline()
        {
            n64OutlineBitmap 
                = new Bitmap(Properties.Resources.n64outline, new Size(420, 420));
            n64OutlineBox.Image = n64OutlineBitmap;
            this.noOutlineToolStripMenuItem.Click += noOutlineToolStripMenuItem_Click;
            this.withOutlineToolStripMenuItem.Click += withOutlineToolStripMenuItem_Click;
        }
        private void HideOutline(bool isHidden)
        {
            if(isHidden)
            {
                n64OutlineBox.Image = null;
            }
            else
            {
                n64OutlineBox.Image = n64OutlineBitmap;
            }
        }
        private void withOutlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideOutline(false);
            noOutlineToolStripMenuItem.Checked = false;
            withOutlineToolStripMenuItem.Checked = true;
            //SetParentForButtons(n64OutlineBox);
        }
        private void noOutlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideOutline(true);
            noOutlineToolStripMenuItem.Checked = true;
            withOutlineToolStripMenuItem.Checked = false;
            //SetParentForButtons(this);
        }
        //// BUTTONS ////
        private void InitializeControllerButtons()
        {
            Bitmap cDownBitmap
                = new Bitmap(Properties.Resources.cDown, new Size(26, 26));
            cDownBox.Image = cDownBitmap;
            cDownBox.Parent = n64OutlineBox;
            picBoxDict.Add("cDown", cDownBox);

            Bitmap cUpBitmap
                = new Bitmap(Properties.Resources.cUp, new Size(26, 26));
            cUpBox.Image = cUpBitmap;
            cUpBox.Parent = n64OutlineBox;
            picBoxDict.Add("cUp", cUpBox);

            Bitmap cLeftBitmap
                = new Bitmap(Properties.Resources.cLeft, new Size(26, 26));
            cLeftBox.Image = cLeftBitmap;
            cLeftBox.Parent = n64OutlineBox;
            picBoxDict.Add("cLeft", cLeftBox);

            Bitmap cRightBitmap
                = new Bitmap(Properties.Resources.cRight, new Size(26, 26));
            cRightBox.Image = cRightBitmap;
            cRightBox.Parent = n64OutlineBox;
            picBoxDict.Add("cRight", cRightBox);

            Bitmap aBitmap
                = new Bitmap(Properties.Resources.a, new Size(37, 37));
            aBox.Image = aBitmap;
            aBox.Parent = n64OutlineBox;
            picBoxDict.Add("a", aBox);

            Bitmap bBitmap
                = new Bitmap(Properties.Resources.b, new Size(37, 37));
            bBox.Image = bBitmap;
            bBox.Parent = n64OutlineBox;
            picBoxDict.Add("b", bBox);

            Bitmap startBitmap
                = new Bitmap(Properties.Resources.start, new Size(43, 43));
            startBox.Image = startBitmap;
            startBox.Parent = n64OutlineBox;
            picBoxDict.Add("start", startBox);

            Bitmap rBitmap
                = new Bitmap(Properties.Resources.r, new Size(70, 45));
            rBox.Image = rBitmap;
            rBox.Location = new Point(304, 4);
            rBox.Parent = n64OutlineBox;
            picBoxDict.Add("r", rBox);

            Bitmap lBitmap
                = new Bitmap(Properties.Resources.l, new Size(70, 45));
            lBox.Image = lBitmap;
            lBox.Location = new Point(45, 4);
            lBox.Parent = n64OutlineBox;
            picBoxDict.Add("l", lBox);

            Bitmap dPadUp
                = new Bitmap(Properties.Resources.dPadUp, new Size(100, 100));
            dPadUpBox.Image = dPadUp;
            dPadUpBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadUp", dPadUpBox);

            Bitmap dPadUpLeft
                = new Bitmap(Properties.Resources.dPadUpLeft, new Size(100, 100));
            dPadUpLeftBox.Image = dPadUpLeft;
            dPadUpLeftBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadUpLeft", dPadUpLeftBox);

            Bitmap dPadUpRight
                = new Bitmap(Properties.Resources.dPadUpRight, new Size(100, 100));
            dPadUpRightBox.Image = dPadUpRight;
            dPadUpRightBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadUpRight", dPadUpRightBox);

            Bitmap dPadLeft
                = new Bitmap(Properties.Resources.dPadLeft, new Size(100, 100));
            dPadLeftBox.Image = dPadLeft;
            dPadLeftBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadLeft", dPadLeftBox);

            Bitmap dPadRight
                = new Bitmap(Properties.Resources.dPadRight, new Size(100, 100));
            dPadRightBox.Image = dPadRight;
            dPadRightBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadRight", dPadRightBox);

            Bitmap dPadDownLeft
                = new Bitmap(Properties.Resources.dPadDownLeft, new Size(100, 100));
            dPadDownLeftBox.Image = dPadDownLeft;
            dPadDownLeftBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadDownLeft", dPadDownLeftBox);

            Bitmap dPadDownRight
                = new Bitmap(Properties.Resources.dPadDownRight, new Size(100, 100));
            dPadDownRightBox.Image = dPadDownRight;
            dPadDownRightBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadDownRight", dPadDownRightBox);

            Bitmap dPadDown
                = new Bitmap(Properties.Resources.dPadDown, new Size(100, 100));
            dPadDownBox.Image = dPadDown;
            dPadDownBox.Parent = n64OutlineBox;
            picBoxDict.Add("dPadDown", dPadDownBox);


            Bitmap analogCenterBitmap
                = new Bitmap(Properties.Resources.analogCenter, new Size(100, 100));
            analogCenterBox.Image = analogCenterBitmap;
            analogCenterBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogCenter", analogCenterBox);

            Bitmap analogUpBitmap
                = new Bitmap(Properties.Resources.analogUp, new Size(100, 100));
            analogUpBox.Image = analogUpBitmap;
            analogUpBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogUp", analogUpBox);

            Bitmap analogUpRightBitmap
                = new Bitmap(Properties.Resources.analogUpRight, new Size(100, 100));
            analogUpRightBox.Image = analogUpRightBitmap;
            analogUpRightBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogUpRight", analogUpRightBox);

            Bitmap analogRightBitmap
                = new Bitmap(Properties.Resources.analogRight, new Size(100, 100));
            analogRightBox.Image = analogRightBitmap;
            analogRightBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogRight", analogRightBox);

            Bitmap analogDownRightBitmap
                = new Bitmap(Properties.Resources.analogDownRight, new Size(100, 100));
            analogDownRightBox.Image = analogDownRightBitmap;
            analogDownRightBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogDownRight", analogDownRightBox);

            Bitmap analogDownBitmap
                = new Bitmap(Properties.Resources.analogDown, new Size(100, 100));
            analogDownBox.Image = analogDownBitmap;
            analogDownBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogDown", analogDownBox);

            Bitmap analogDownLeftBitmap
                = new Bitmap(Properties.Resources.analogDownLeft, new Size(100, 100));
            analogDownLeftBox.Image = analogDownLeftBitmap;
            analogDownLeftBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogDownLeft", analogDownLeftBox);

            Bitmap analogLeftBitmap
                = new Bitmap(Properties.Resources.analogLeft, new Size(100, 100));
            analogLeftBox.Image = analogLeftBitmap;
            analogLeftBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogLeft", analogLeftBox);

            Bitmap analogUpLeftBitmap
                = new Bitmap(Properties.Resources.analogUpLeft, new Size(100, 100));
            analogUpLeftBox.Image = analogUpLeftBitmap;
            analogUpLeftBox.Parent = n64OutlineBox;
            picBoxDict.Add("analogUpLeft", analogUpLeftBox);

            Bitmap zBitmap
                = new Bitmap(Properties.Resources.z, new Size(60, 60));
            zBox.Image = zBitmap;
            zBox.Parent = n64OutlineBox;
            picBoxDict.Add("z", zBox);

            //analogCenterBox.Visible = false;
            analogUpBox.Visible = false;
            analogUpRightBox.Visible = false;
            analogRightBox.Visible = false;
            analogDownRightBox.Visible = false;
            analogDownBox.Visible = false;
            analogDownLeftBox.Visible = false;
            analogLeftBox.Visible = false;
            analogUpLeftBox.Visible = false;
        }
        internal void ShowButton(string button, bool isVisible)
        {
            picBoxDict[button].Visible = isVisible;
        }
        //// SETTINGS ////
        private void InitializeSettings()
        {
            this.keyBindingsToolStripMenuItem.Click += keyBindingsToolStripMenuItem_Click;

            this.blackToolStripMenuItem.Click += blackToolStripMenuItem_Click;
            this.redToolStripMenuItem.Click += redToolStripMenuItem_Click;
            this.greenToolStripMenuItem.Click += greenToolStripMenuItem_Click;
            this.blueToolStripMenuItem.Click += blueToolStripMenuItem_Click;
        }
        private void keyBindingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Visible = true;
        }
        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            blackToolStripMenuItem.Checked = true;
            redToolStripMenuItem.Checked   = false;
            blueToolStripMenuItem.Checked  = false;
            greenToolStripMenuItem.Checked = false;
        }
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
            blackToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked   = true;
            blueToolStripMenuItem.Checked  = false;
            greenToolStripMenuItem.Checked = false;
        }
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;
            blackToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked   = false;
            blueToolStripMenuItem.Checked  = true;
            greenToolStripMenuItem.Checked = false;
        }
        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
            blackToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked   = false;
            blueToolStripMenuItem.Checked  = false;
            greenToolStripMenuItem.Checked = true;
        }
    }
}
