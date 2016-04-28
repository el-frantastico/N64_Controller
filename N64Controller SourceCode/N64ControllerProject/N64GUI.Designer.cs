namespace N64ControllerProject
{
    partial class N64GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(N64GUI));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.COMPortsToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.searchCOMPortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portSeparatorToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.testConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withOutlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noOutlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.COMPortsToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.controllerOutline = new System.Windows.Forms.ImageList(this.components);
            this.cDownBox = new System.Windows.Forms.PictureBox();
            this.n64OutlineBox = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cDownBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n64OutlineBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem,
            this.layoutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(444, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.COMPortsToolStripMenu,
            this.testConnectionToolStripMenuItem,
            this.connectionSeperator,
            this.restartToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // COMPortsToolStripMenu
            // 
            this.COMPortsToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchCOMPortsToolStripMenuItem,
            this.portSeparatorToolStripMenuItem});
            this.COMPortsToolStripMenu.Name = "COMPortsToolStripMenu";
            this.COMPortsToolStripMenu.Size = new System.Drawing.Size(160, 22);
            this.COMPortsToolStripMenu.Text = "COM Ports";
            // 
            // searchCOMPortsToolStripMenuItem
            // 
            this.searchCOMPortsToolStripMenuItem.Name = "searchCOMPortsToolStripMenuItem";
            this.searchCOMPortsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.searchCOMPortsToolStripMenuItem.Text = "Search COM Ports";
            // 
            // portSeparatorToolStripMenuItem
            // 
            this.portSeparatorToolStripMenuItem.Name = "portSeparatorToolStripMenuItem";
            this.portSeparatorToolStripMenuItem.Size = new System.Drawing.Size(167, 6);
            // 
            // testConnectionToolStripMenuItem
            // 
            this.testConnectionToolStripMenuItem.Name = "testConnectionToolStripMenuItem";
            this.testConnectionToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.testConnectionToolStripMenuItem.Text = "Test Connection";
            // 
            // connectionSeperator
            // 
            this.connectionSeperator.Name = "connectionSeperator";
            this.connectionSeperator.Size = new System.Drawing.Size(157, 6);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // layoutToolStripMenuItem
            // 
            this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controllerToolStripMenuItem,
            this.toolStripSeparator,
            this.verticalToolStripMenuItem,
            this.horizontalToolStripMenuItem});
            this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
            this.layoutToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.layoutToolStripMenuItem.Text = "Layout";
            // 
            // controllerToolStripMenuItem
            // 
            this.controllerToolStripMenuItem.Checked = true;
            this.controllerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controllerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.withOutlineToolStripMenuItem,
            this.noOutlineToolStripMenuItem});
            this.controllerToolStripMenuItem.Name = "controllerToolStripMenuItem";
            this.controllerToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.controllerToolStripMenuItem.Text = "Controller";
            // 
            // withOutlineToolStripMenuItem
            // 
            this.withOutlineToolStripMenuItem.Checked = true;
            this.withOutlineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.withOutlineToolStripMenuItem.Name = "withOutlineToolStripMenuItem";
            this.withOutlineToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.withOutlineToolStripMenuItem.Text = "With Outline";
            // 
            // noOutlineToolStripMenuItem
            // 
            this.noOutlineToolStripMenuItem.Name = "noOutlineToolStripMenuItem";
            this.noOutlineToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.noOutlineToolStripMenuItem.Text = "No Outline";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(126, 6);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            // 
            // COMPortsToolStripMenuItem
            // 
            this.COMPortsToolStripMenuItem.Name = "COMPortsToolStripMenuItem";
            this.COMPortsToolStripMenuItem.Size = new System.Drawing.Size(170, 23);
            this.COMPortsToolStripMenuItem.Text = "COM Ports";
            // 
            // controllerOutline
            // 
            this.controllerOutline.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("controllerOutline.ImageStream")));
            this.controllerOutline.TransparentColor = System.Drawing.Color.Transparent;
            this.controllerOutline.Images.SetKeyName(0, "n64outline.png");
            // 
            // cDownBox
            // 
            this.cDownBox.BackColor = System.Drawing.Color.Transparent;
            this.cDownBox.Location = new System.Drawing.Point(329, 135);
            this.cDownBox.Name = "cDownBox";
            this.cDownBox.Size = new System.Drawing.Size(25, 25);
            this.cDownBox.TabIndex = 2;
            this.cDownBox.TabStop = false;
            // 
            // n64OutlineBox
            // 
            this.n64OutlineBox.BackColor = System.Drawing.Color.Transparent;
            this.n64OutlineBox.Location = new System.Drawing.Point(12, 30);
            this.n64OutlineBox.Name = "n64OutlineBox";
            this.n64OutlineBox.Size = new System.Drawing.Size(420, 420);
            this.n64OutlineBox.TabIndex = 1;
            this.n64OutlineBox.TabStop = false;
            // 
            // N64GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(444, 461);
            this.Controls.Add(this.cDownBox);
            this.Controls.Add(this.n64OutlineBox);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "N64GUI";
            this.Text = "N64 Controller";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cDownBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n64OutlineBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SearchCOMPortsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withOutlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noOutlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox COMPortsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator connectionSeperator;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem COMPortsToolStripMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ToolStripMenuItem searchCOMPortsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator portSeparatorToolStripMenuItem;
        private System.Windows.Forms.ImageList controllerOutline;
        private System.Windows.Forms.PictureBox n64OutlineBox;
        private System.Windows.Forms.PictureBox cDownBox;
    }
}

