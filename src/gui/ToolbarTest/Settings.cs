using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N64ControllerProject
{
    public partial class Settings : Form
    {
        public Settings()
        {
            //Initialize form
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            CheckEmulatorMode();
            FillTextBoxes();
        }
        
        private void CheckEmulatorMode()
        {
            emulatorModeCheckBox.Checked = ControllerEventHandler.emulatorMode;
        }
        private void FillTextBoxes()
        {
            aTextBox.Text = ControllerEventHandler.buttonKeyDict["a"].ToString();
            bTextBox.Text = ControllerEventHandler.buttonKeyDict["b"].ToString();
            lTextBox.Text = ControllerEventHandler.buttonKeyDict["l"].ToString();
            rTextBox.Text = ControllerEventHandler.buttonKeyDict["r"].ToString();
            zTextBox.Text = ControllerEventHandler.buttonKeyDict["z"].ToString();
            startTextBox.Text = ControllerEventHandler.buttonKeyDict["start"].ToString();

            cUpTextBox.Text = ControllerEventHandler.buttonKeyDict["cUp"].ToString();
            cDownTextBox.Text = ControllerEventHandler.buttonKeyDict["cDown"].ToString();
            cLeftTextBox.Text = ControllerEventHandler.buttonKeyDict["cLeft"].ToString();
            cRightTextBox.Text = ControllerEventHandler.buttonKeyDict["cRight"].ToString();

            dPadUpTextBox.Text = ControllerEventHandler.buttonKeyDict["dPadUp"].ToString();
            dPadDownTextBox.Text = ControllerEventHandler.buttonKeyDict["dPadDown"].ToString();
            dPadLeftTextBox.Text = ControllerEventHandler.buttonKeyDict["dPadLeft"].ToString();
            dPadRightTextBox.Text = ControllerEventHandler.buttonKeyDict["dPadRight"].ToString();

            analogUpTextBox.Text = ControllerEventHandler.buttonKeyDict["analogUp"].ToString();
            analogDownTextBox.Text = ControllerEventHandler.buttonKeyDict["analogDown"].ToString();
            analogLeftTextBox.Text = ControllerEventHandler.buttonKeyDict["analogLeft"].ToString();
            analogRightTextBox.Text = ControllerEventHandler.buttonKeyDict["analogRight"].ToString();
        }
        //Save and Cancel Buttons
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            ControllerEventHandler.emulatorMode = emulatorModeCheckBox.Checked;

            ControllerEventHandler.buttonKeyDict["a"] = aTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["b"] = bTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["l"] = lTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["r"] = rTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["z"] = zTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["start"] = startTextBox.Text.ToCharArray()[0];

            ControllerEventHandler.buttonKeyDict["cUp"] = cUpTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["cDown"] = cDownTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["cLeft"] = cLeftTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["cRight"] = cRightTextBox.Text.ToCharArray()[0];

            ControllerEventHandler.buttonKeyDict["dPadUp"] = dPadUpTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["dPadDown"] = dPadDownTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["dPadLeft"] = dPadLeftTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["dPadRight"] = dPadRightTextBox.Text.ToCharArray()[0];

            ControllerEventHandler.buttonKeyDict["analogUp"] = analogUpTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["analogDown"] = analogDownTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["analogLeft"] = analogLeftTextBox.Text.ToCharArray()[0];
            ControllerEventHandler.buttonKeyDict["analogRight"] = analogRightTextBox.Text.ToCharArray()[0];

            this.Close();
        }
    }
}
