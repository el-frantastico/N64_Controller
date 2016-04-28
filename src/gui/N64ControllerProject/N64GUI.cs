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

namespace N64ControllerProject
{
    public partial class N64GUI : Form
    {
        public N64GUI()
        {
            InitializeComponent();
            ListCOMPorts();
            AddOutline();
            InitializeController();
            serialPort = new SerialPort();
        }
        //// COM PORT CONNECTION ////
        private void searchCOMPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListCOMPorts();
        }
        private void ListCOMPorts()
        {
            //clear current ports or noPorts label
            for (int i = 2; i < COMPortsToolStripMenu.DropDownItems.Count; i++)
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
                if(serialPort != null && item.Text == serialPort.PortName)
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
            serialPort.ReadTimeout = 5000;
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
        private void AddOutline()
        {
            Bitmap n64OutlineBitmap 
                = new Bitmap(Properties.Resources.n64outline, new Size(420, 420));
            n64OutlineBox.Image = n64OutlineBitmap;
        }
        private void HideOutline(bool isHidden)
        {
            n64OutlineBox.Visible = !isHidden;
        }
        private void WithOutlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideOutline(false);
            noOutlineToolStripMenuItem.Checked = false;
            withOutlineToolStripMenuItem.Checked = true;
        }
        private void NoOutlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideOutline(true);
            noOutlineToolStripMenuItem.Checked = true;
            withOutlineToolStripMenuItem.Checked = false;
        }

        //// BUTTONS INITIALIZATION ////
        private void InitializeController()
        {
            Bitmap cDownBitmap
                = new Bitmap(Properties.Resources.cDown, new Size(25, 25));
            cDownBox.Image = cDownBitmap;
        }
        private void HideButton(bool isHidden, string button)
        {
            switch (button)
            {
                case "cUp":

                case "cDown":
                    cDownBox.Visible = !isHidden; break;
            }
        }
    }
}
