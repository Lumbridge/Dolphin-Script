namespace DolphinScript
{
    partial class MainForm
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
            this.button_StartScript = new System.Windows.Forms.Button();
            this.ListBox_Events = new System.Windows.Forms.ListBox();
            this.button_MoveEventUp = new System.Windows.Forms.Button();
            this.button_MoveEventDown = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.lastActionLabel = new System.Windows.Forms.Label();
            this.button_RemoveEvent = new System.Windows.Forms.Button();
            this.NumericUpDown_MouseSpeed = new System.Windows.Forms.NumericUpDown();
            this.button_ShowKeyboardEventForm = new System.Windows.Forms.Button();
            this.button_About = new System.Windows.Forms.Button();
            this.button_AddRepeatGroup = new System.Windows.Forms.Button();
            this.NumericUpDown_RepeatAmount = new System.Windows.Forms.NumericUpDown();
            this.repeatGroupLabel = new System.Windows.Forms.Label();
            this.TrackBar_MouseSpeed = new System.Windows.Forms.TrackBar();
            this.mouseSpeedLabel = new System.Windows.Forms.Label();
            this.button_ShowPauseEventForm = new System.Windows.Forms.Button();
            this.button_ShowMouseMoveForm = new System.Windows.Forms.Button();
            this.button_ShowMouseClickForm = new System.Windows.Forms.Button();
            this.MenuStrip_MainForm = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MouseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RepeatAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_MouseSpeed)).BeginInit();
            this.MenuStrip_MainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_StartScript
            // 
            this.button_StartScript.Location = new System.Drawing.Point(12, 141);
            this.button_StartScript.Name = "button_StartScript";
            this.button_StartScript.Size = new System.Drawing.Size(417, 37);
            this.button_StartScript.TabIndex = 0;
            this.button_StartScript.Text = "Start Script";
            this.button_StartScript.UseVisualStyleBackColor = true;
            this.button_StartScript.Click += new System.EventHandler(this.startButton_Click);
            // 
            // ListBox_Events
            // 
            this.ListBox_Events.FormattingEnabled = true;
            this.ListBox_Events.HorizontalScrollbar = true;
            this.ListBox_Events.Location = new System.Drawing.Point(12, 27);
            this.ListBox_Events.Name = "ListBox_Events";
            this.ListBox_Events.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListBox_Events.Size = new System.Drawing.Size(417, 108);
            this.ListBox_Events.TabIndex = 1;
            this.ListBox_Events.SelectedIndexChanged += new System.EventHandler(this.ListBox_Events_SelectedIndexChanged);
            // 
            // button_MoveEventUp
            // 
            this.button_MoveEventUp.Enabled = false;
            this.button_MoveEventUp.Location = new System.Drawing.Point(435, 27);
            this.button_MoveEventUp.Name = "button_MoveEventUp";
            this.button_MoveEventUp.Size = new System.Drawing.Size(56, 36);
            this.button_MoveEventUp.TabIndex = 3;
            this.button_MoveEventUp.Text = "↑";
            this.button_MoveEventUp.UseVisualStyleBackColor = true;
            this.button_MoveEventUp.Click += new System.EventHandler(this.moveElementUpButton_Click);
            // 
            // button_MoveEventDown
            // 
            this.button_MoveEventDown.Enabled = false;
            this.button_MoveEventDown.Location = new System.Drawing.Point(435, 99);
            this.button_MoveEventDown.Name = "button_MoveEventDown";
            this.button_MoveEventDown.Size = new System.Drawing.Size(56, 36);
            this.button_MoveEventDown.TabIndex = 4;
            this.button_MoveEventDown.Text = "↓";
            this.button_MoveEventDown.UseVisualStyleBackColor = true;
            this.button_MoveEventDown.Click += new System.EventHandler(this.moveElementDownButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(9, 458);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(60, 13);
            this.statusLabel.TabIndex = 14;
            this.statusLabel.Text = "Status: Idle";
            // 
            // lastActionLabel
            // 
            this.lastActionLabel.AutoSize = true;
            this.lastActionLabel.Location = new System.Drawing.Point(9, 445);
            this.lastActionLabel.Name = "lastActionLabel";
            this.lastActionLabel.Size = new System.Drawing.Size(103, 13);
            this.lastActionLabel.TabIndex = 15;
            this.lastActionLabel.Text = "Last Action: Nothing";
            // 
            // button_RemoveEvent
            // 
            this.button_RemoveEvent.Enabled = false;
            this.button_RemoveEvent.Location = new System.Drawing.Point(435, 69);
            this.button_RemoveEvent.Name = "button_RemoveEvent";
            this.button_RemoveEvent.Size = new System.Drawing.Size(56, 24);
            this.button_RemoveEvent.TabIndex = 16;
            this.button_RemoveEvent.Text = "X";
            this.button_RemoveEvent.UseVisualStyleBackColor = true;
            this.button_RemoveEvent.Click += new System.EventHandler(this.removeEventButton_Click);
            // 
            // NumericUpDown_MouseSpeed
            // 
            this.NumericUpDown_MouseSpeed.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.NumericUpDown_MouseSpeed.Location = new System.Drawing.Point(88, 363);
            this.NumericUpDown_MouseSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NumericUpDown_MouseSpeed.Name = "NumericUpDown_MouseSpeed";
            this.NumericUpDown_MouseSpeed.Size = new System.Drawing.Size(120, 20);
            this.NumericUpDown_MouseSpeed.TabIndex = 6;
            this.NumericUpDown_MouseSpeed.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.NumericUpDown_MouseSpeed.ValueChanged += new System.EventHandler(this.mouseSpeedNumberBox_ValueChanged);
            // 
            // button_ShowKeyboardEventForm
            // 
            this.button_ShowKeyboardEventForm.Location = new System.Drawing.Point(93, 184);
            this.button_ShowKeyboardEventForm.Name = "button_ShowKeyboardEventForm";
            this.button_ShowKeyboardEventForm.Size = new System.Drawing.Size(75, 62);
            this.button_ShowKeyboardEventForm.TabIndex = 10;
            this.button_ShowKeyboardEventForm.Text = "Keyboard Event";
            this.button_ShowKeyboardEventForm.UseVisualStyleBackColor = true;
            this.button_ShowKeyboardEventForm.Click += new System.EventHandler(this.Button_ShowKeyPressForm_Click);
            // 
            // button_About
            // 
            this.button_About.Location = new System.Drawing.Point(435, 141);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(56, 37);
            this.button_About.TabIndex = 27;
            this.button_About.Text = "About";
            this.button_About.UseVisualStyleBackColor = true;
            this.button_About.Click += new System.EventHandler(this.Button_ShowInfoForm_Click);
            // 
            // button_AddRepeatGroup
            // 
            this.button_AddRepeatGroup.Location = new System.Drawing.Point(267, 339);
            this.button_AddRepeatGroup.Name = "button_AddRepeatGroup";
            this.button_AddRepeatGroup.Size = new System.Drawing.Size(206, 35);
            this.button_AddRepeatGroup.TabIndex = 8;
            this.button_AddRepeatGroup.Text = "Add repeat group";
            this.button_AddRepeatGroup.UseVisualStyleBackColor = true;
            this.button_AddRepeatGroup.Click += new System.EventHandler(this.repeatGroupButton_Click);
            // 
            // NumericUpDown_RepeatAmount
            // 
            this.NumericUpDown_RepeatAmount.Location = new System.Drawing.Point(267, 313);
            this.NumericUpDown_RepeatAmount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.NumericUpDown_RepeatAmount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NumericUpDown_RepeatAmount.Name = "NumericUpDown_RepeatAmount";
            this.NumericUpDown_RepeatAmount.Size = new System.Drawing.Size(206, 20);
            this.NumericUpDown_RepeatAmount.TabIndex = 7;
            this.NumericUpDown_RepeatAmount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // repeatGroupLabel
            // 
            this.repeatGroupLabel.AutoSize = true;
            this.repeatGroupLabel.Location = new System.Drawing.Point(267, 260);
            this.repeatGroupLabel.Name = "repeatGroupLabel";
            this.repeatGroupLabel.Size = new System.Drawing.Size(202, 52);
            this.repeatGroupLabel.TabIndex = 31;
            this.repeatGroupLabel.Text = "Add selected events to a repeat group\r\nand how many times should  this group of\r\n" +
    "actions repeat before carrying on with the\r\nrest of the script..";
            // 
            // TrackBar_MouseSpeed
            // 
            this.TrackBar_MouseSpeed.Cursor = System.Windows.Forms.Cursors.Default;
            this.TrackBar_MouseSpeed.LargeChange = 500;
            this.TrackBar_MouseSpeed.Location = new System.Drawing.Point(12, 389);
            this.TrackBar_MouseSpeed.Maximum = 50;
            this.TrackBar_MouseSpeed.Name = "TrackBar_MouseSpeed";
            this.TrackBar_MouseSpeed.RightToLeftLayout = true;
            this.TrackBar_MouseSpeed.Size = new System.Drawing.Size(292, 45);
            this.TrackBar_MouseSpeed.SmallChange = 100;
            this.TrackBar_MouseSpeed.TabIndex = 36;
            this.TrackBar_MouseSpeed.TickFrequency = 5;
            this.TrackBar_MouseSpeed.Value = 15;
            this.TrackBar_MouseSpeed.Scroll += new System.EventHandler(this.mouseSpeedTrackBar_Scroll);
            // 
            // mouseSpeedLabel
            // 
            this.mouseSpeedLabel.AutoSize = true;
            this.mouseSpeedLabel.Location = new System.Drawing.Point(9, 367);
            this.mouseSpeedLabel.Name = "mouseSpeedLabel";
            this.mouseSpeedLabel.Size = new System.Drawing.Size(73, 13);
            this.mouseSpeedLabel.TabIndex = 37;
            this.mouseSpeedLabel.Text = "Mouse Speed";
            // 
            // button_ShowPauseEventForm
            // 
            this.button_ShowPauseEventForm.Location = new System.Drawing.Point(12, 184);
            this.button_ShowPauseEventForm.Name = "button_ShowPauseEventForm";
            this.button_ShowPauseEventForm.Size = new System.Drawing.Size(75, 62);
            this.button_ShowPauseEventForm.TabIndex = 41;
            this.button_ShowPauseEventForm.Text = "Pause Event";
            this.button_ShowPauseEventForm.UseVisualStyleBackColor = true;
            this.button_ShowPauseEventForm.Click += new System.EventHandler(this.Button_ShowPauseForm_Click);
            // 
            // button_ShowMouseMoveForm
            // 
            this.button_ShowMouseMoveForm.Location = new System.Drawing.Point(174, 184);
            this.button_ShowMouseMoveForm.Name = "button_ShowMouseMoveForm";
            this.button_ShowMouseMoveForm.Size = new System.Drawing.Size(75, 62);
            this.button_ShowMouseMoveForm.TabIndex = 43;
            this.button_ShowMouseMoveForm.Text = "Mouse Move Event";
            this.button_ShowMouseMoveForm.UseVisualStyleBackColor = true;
            this.button_ShowMouseMoveForm.Click += new System.EventHandler(this.Button_ShowMouseMoveForm_Click);
            // 
            // button_ShowMouseClickForm
            // 
            this.button_ShowMouseClickForm.Location = new System.Drawing.Point(255, 184);
            this.button_ShowMouseClickForm.Name = "button_ShowMouseClickForm";
            this.button_ShowMouseClickForm.Size = new System.Drawing.Size(75, 62);
            this.button_ShowMouseClickForm.TabIndex = 44;
            this.button_ShowMouseClickForm.Text = "Mouse Click Event";
            this.button_ShowMouseClickForm.UseVisualStyleBackColor = true;
            this.button_ShowMouseClickForm.Click += new System.EventHandler(this.Button_ShowMouseClickForm_Click);
            // 
            // MenuStrip_MainForm
            // 
            this.MenuStrip_MainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File});
            this.MenuStrip_MainForm.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_MainForm.Name = "MenuStrip_MainForm";
            this.MenuStrip_MainForm.Size = new System.Drawing.Size(505, 24);
            this.MenuStrip_MainForm.TabIndex = 46;
            this.MenuStrip_MainForm.Text = "File";
            // 
            // ToolStripMenuItem_File
            // 
            this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveScriptToolStripMenuItem,
            this.loadScriptToolStripMenuItem1});
            this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
            this.ToolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.ToolStripMenuItem_File.Text = "File";
            // 
            // saveScriptToolStripMenuItem
            // 
            this.saveScriptToolStripMenuItem.Name = "saveScriptToolStripMenuItem";
            this.saveScriptToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveScriptToolStripMenuItem.Text = "Save Script";
            this.saveScriptToolStripMenuItem.Click += new System.EventHandler(this.saveScriptToolStripMenuItem_Click);
            // 
            // loadScriptToolStripMenuItem1
            // 
            this.loadScriptToolStripMenuItem1.Name = "loadScriptToolStripMenuItem1";
            this.loadScriptToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.loadScriptToolStripMenuItem1.Text = "Load Script";
            this.loadScriptToolStripMenuItem1.Click += new System.EventHandler(this.loadScriptToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 480);
            this.Controls.Add(this.MenuStrip_MainForm);
            this.Controls.Add(this.button_ShowMouseClickForm);
            this.Controls.Add(this.button_ShowMouseMoveForm);
            this.Controls.Add(this.button_ShowPauseEventForm);
            this.Controls.Add(this.mouseSpeedLabel);
            this.Controls.Add(this.TrackBar_MouseSpeed);
            this.Controls.Add(this.repeatGroupLabel);
            this.Controls.Add(this.NumericUpDown_RepeatAmount);
            this.Controls.Add(this.button_AddRepeatGroup);
            this.Controls.Add(this.button_About);
            this.Controls.Add(this.button_ShowKeyboardEventForm);
            this.Controls.Add(this.NumericUpDown_MouseSpeed);
            this.Controls.Add(this.button_RemoveEvent);
            this.Controls.Add(this.lastActionLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.button_MoveEventDown);
            this.Controls.Add(this.button_MoveEventUp);
            this.Controls.Add(this.ListBox_Events);
            this.Controls.Add(this.button_StartScript);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStrip_MainForm;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Dolphin Script";
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MouseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RepeatAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_MouseSpeed)).EndInit();
            this.MenuStrip_MainForm.ResumeLayout(false);
            this.MenuStrip_MainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_StartScript;
        private System.Windows.Forms.Button button_MoveEventUp;
        private System.Windows.Forms.Button button_MoveEventDown;
        public System.Windows.Forms.ListBox ListBox_Events;
        private System.Windows.Forms.Button button_RemoveEvent;
        private System.Windows.Forms.NumericUpDown NumericUpDown_MouseSpeed;
        private System.Windows.Forms.Button button_ShowKeyboardEventForm;
        private System.Windows.Forms.Button button_About;
        public System.Windows.Forms.Label lastActionLabel;
        public System.Windows.Forms.Label statusLabel;
        public System.Windows.Forms.Button registerClickLocationsButton;
        private System.Windows.Forms.Button button_AddRepeatGroup;
        private System.Windows.Forms.NumericUpDown NumericUpDown_RepeatAmount;
        private System.Windows.Forms.Label repeatGroupLabel;
        private System.Windows.Forms.TrackBar TrackBar_MouseSpeed;
        private System.Windows.Forms.Label mouseSpeedLabel;
        private System.Windows.Forms.Button button_ShowPauseEventForm;
        private System.Windows.Forms.Button button_ShowMouseMoveForm;
        private System.Windows.Forms.Button button_ShowMouseClickForm;
        private System.Windows.Forms.MenuStrip MenuStrip_MainForm;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem saveScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadScriptToolStripMenuItem1;
    }
}

