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
            this.button_ShowKeyboardEventForm = new System.Windows.Forms.Button();
            this.button_About = new System.Windows.Forms.Button();
            this.button_AddRepeatGroup = new System.Windows.Forms.Button();
            this.NumericUpDown_RepeatAmount = new System.Windows.Forms.NumericUpDown();
            this.repeatGroupLabel = new System.Windows.Forms.Label();
            this.button_ShowPauseEventForm = new System.Windows.Forms.Button();
            this.button_ShowMouseMoveForm = new System.Windows.Forms.Button();
            this.button_ShowMouseClickForm = new System.Windows.Forms.Button();
            this.MenuStrip_MainForm = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_ScriptEvents = new System.Windows.Forms.TabControl();
            this.tabPage_PauseEvent = new System.Windows.Forms.TabPage();
            this.tabPage2_KeyboardEvent = new System.Windows.Forms.TabPage();
            this.tabPage_MouseMoveEvent = new System.Windows.Forms.TabPage();
            this.Button_ColourPreview = new System.Windows.Forms.Button();
            this.groupBox_RelativeToScreen = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_MousePosY = new System.Windows.Forms.TextBox();
            this.TextBox_MousePosX = new System.Windows.Forms.TextBox();
            this.button_InsertMouseMoveToAreaEvent = new System.Windows.Forms.Button();
            this.button_InsertMouseMoveEvent = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBox_ActiveWindowTitle = new System.Windows.Forms.TextBox();
            this.button_InsertMouseMoveToAreaOnWindowEvent = new System.Windows.Forms.Button();
            this.button_InsertMouseMoveToPointOnWindowEvent = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBox_ActiveWindowMouseY = new System.Windows.Forms.TextBox();
            this.TextBox_ActiveWindowMouseX = new System.Windows.Forms.TextBox();
            this.groupBox_MouseSpeed = new System.Windows.Forms.GroupBox();
            this.TrackBar_MouseSpeed = new System.Windows.Forms.TrackBar();
            this.NumericUpDown_MouseSpeed = new System.Windows.Forms.NumericUpDown();
            this.tabPage_MouseClick = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RepeatAmount)).BeginInit();
            this.MenuStrip_MainForm.SuspendLayout();
            this.tabControl_ScriptEvents.SuspendLayout();
            this.tabPage_MouseMoveEvent.SuspendLayout();
            this.groupBox_RelativeToScreen.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox_MouseSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_MouseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MouseSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // button_StartScript
            // 
            this.button_StartScript.Location = new System.Drawing.Point(12, 141);
            this.button_StartScript.Name = "button_StartScript";
            this.button_StartScript.Size = new System.Drawing.Size(522, 37);
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
            this.ListBox_Events.Size = new System.Drawing.Size(522, 108);
            this.ListBox_Events.TabIndex = 1;
            this.ListBox_Events.SelectedIndexChanged += new System.EventHandler(this.ListBox_Events_SelectedIndexChanged);
            // 
            // button_MoveEventUp
            // 
            this.button_MoveEventUp.Enabled = false;
            this.button_MoveEventUp.Location = new System.Drawing.Point(540, 27);
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
            this.button_MoveEventDown.Location = new System.Drawing.Point(540, 99);
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
            this.statusLabel.Location = new System.Drawing.Point(15, 713);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(60, 13);
            this.statusLabel.TabIndex = 14;
            this.statusLabel.Text = "Status: Idle";
            // 
            // lastActionLabel
            // 
            this.lastActionLabel.AutoSize = true;
            this.lastActionLabel.Location = new System.Drawing.Point(15, 700);
            this.lastActionLabel.Name = "lastActionLabel";
            this.lastActionLabel.Size = new System.Drawing.Size(103, 13);
            this.lastActionLabel.TabIndex = 15;
            this.lastActionLabel.Text = "Last Action: Nothing";
            // 
            // button_RemoveEvent
            // 
            this.button_RemoveEvent.Enabled = false;
            this.button_RemoveEvent.Location = new System.Drawing.Point(540, 69);
            this.button_RemoveEvent.Name = "button_RemoveEvent";
            this.button_RemoveEvent.Size = new System.Drawing.Size(56, 24);
            this.button_RemoveEvent.TabIndex = 16;
            this.button_RemoveEvent.Text = "X";
            this.button_RemoveEvent.UseVisualStyleBackColor = true;
            this.button_RemoveEvent.Click += new System.EventHandler(this.removeEventButton_Click);
            // 
            // button_ShowKeyboardEventForm
            // 
            this.button_ShowKeyboardEventForm.Location = new System.Drawing.Point(93, 635);
            this.button_ShowKeyboardEventForm.Name = "button_ShowKeyboardEventForm";
            this.button_ShowKeyboardEventForm.Size = new System.Drawing.Size(75, 62);
            this.button_ShowKeyboardEventForm.TabIndex = 10;
            this.button_ShowKeyboardEventForm.Text = "Keyboard Event";
            this.button_ShowKeyboardEventForm.UseVisualStyleBackColor = true;
            this.button_ShowKeyboardEventForm.Click += new System.EventHandler(this.Button_ShowKeyPressForm_Click);
            // 
            // button_About
            // 
            this.button_About.Location = new System.Drawing.Point(540, 141);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(56, 37);
            this.button_About.TabIndex = 27;
            this.button_About.Text = "About";
            this.button_About.UseVisualStyleBackColor = true;
            this.button_About.Click += new System.EventHandler(this.Button_ShowInfoForm_Click);
            // 
            // button_AddRepeatGroup
            // 
            this.button_AddRepeatGroup.Location = new System.Drawing.Point(394, 685);
            this.button_AddRepeatGroup.Name = "button_AddRepeatGroup";
            this.button_AddRepeatGroup.Size = new System.Drawing.Size(206, 35);
            this.button_AddRepeatGroup.TabIndex = 8;
            this.button_AddRepeatGroup.Text = "Add repeat group";
            this.button_AddRepeatGroup.UseVisualStyleBackColor = true;
            this.button_AddRepeatGroup.Click += new System.EventHandler(this.repeatGroupButton_Click);
            // 
            // NumericUpDown_RepeatAmount
            // 
            this.NumericUpDown_RepeatAmount.Location = new System.Drawing.Point(394, 659);
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
            this.repeatGroupLabel.Location = new System.Drawing.Point(394, 606);
            this.repeatGroupLabel.Name = "repeatGroupLabel";
            this.repeatGroupLabel.Size = new System.Drawing.Size(202, 52);
            this.repeatGroupLabel.TabIndex = 31;
            this.repeatGroupLabel.Text = "Add selected events to a repeat group\r\nand how many times should  this group of\r\n" +
    "actions repeat before carrying on with the\r\nrest of the script..";
            // 
            // button_ShowPauseEventForm
            // 
            this.button_ShowPauseEventForm.Location = new System.Drawing.Point(12, 635);
            this.button_ShowPauseEventForm.Name = "button_ShowPauseEventForm";
            this.button_ShowPauseEventForm.Size = new System.Drawing.Size(75, 62);
            this.button_ShowPauseEventForm.TabIndex = 41;
            this.button_ShowPauseEventForm.Text = "Pause Event";
            this.button_ShowPauseEventForm.UseVisualStyleBackColor = true;
            this.button_ShowPauseEventForm.Click += new System.EventHandler(this.Button_ShowPauseForm_Click);
            // 
            // button_ShowMouseMoveForm
            // 
            this.button_ShowMouseMoveForm.Location = new System.Drawing.Point(174, 635);
            this.button_ShowMouseMoveForm.Name = "button_ShowMouseMoveForm";
            this.button_ShowMouseMoveForm.Size = new System.Drawing.Size(75, 62);
            this.button_ShowMouseMoveForm.TabIndex = 43;
            this.button_ShowMouseMoveForm.Text = "Mouse Move Event";
            this.button_ShowMouseMoveForm.UseVisualStyleBackColor = true;
            this.button_ShowMouseMoveForm.Click += new System.EventHandler(this.Button_ShowMouseMoveForm_Click);
            // 
            // button_ShowMouseClickForm
            // 
            this.button_ShowMouseClickForm.Location = new System.Drawing.Point(255, 635);
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
            this.MenuStrip_MainForm.Size = new System.Drawing.Size(651, 24);
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
            // tabControl_ScriptEvents
            // 
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage_PauseEvent);
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage2_KeyboardEvent);
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage_MouseMoveEvent);
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage_MouseClick);
            this.tabControl_ScriptEvents.Location = new System.Drawing.Point(12, 184);
            this.tabControl_ScriptEvents.Name = "tabControl_ScriptEvents";
            this.tabControl_ScriptEvents.SelectedIndex = 0;
            this.tabControl_ScriptEvents.Size = new System.Drawing.Size(588, 419);
            this.tabControl_ScriptEvents.TabIndex = 47;
            // 
            // tabPage_PauseEvent
            // 
            this.tabPage_PauseEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPage_PauseEvent.Name = "tabPage_PauseEvent";
            this.tabPage_PauseEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_PauseEvent.Size = new System.Drawing.Size(580, 393);
            this.tabPage_PauseEvent.TabIndex = 0;
            this.tabPage_PauseEvent.Text = "Pause Event";
            this.tabPage_PauseEvent.UseVisualStyleBackColor = true;
            // 
            // tabPage2_KeyboardEvent
            // 
            this.tabPage2_KeyboardEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPage2_KeyboardEvent.Name = "tabPage2_KeyboardEvent";
            this.tabPage2_KeyboardEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2_KeyboardEvent.Size = new System.Drawing.Size(580, 393);
            this.tabPage2_KeyboardEvent.TabIndex = 1;
            this.tabPage2_KeyboardEvent.Text = "Keyboard Event";
            this.tabPage2_KeyboardEvent.UseVisualStyleBackColor = true;
            // 
            // tabPage_MouseMoveEvent
            // 
            this.tabPage_MouseMoveEvent.Controls.Add(this.Button_ColourPreview);
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox_RelativeToScreen);
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox1);
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox_MouseSpeed);
            this.tabPage_MouseMoveEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPage_MouseMoveEvent.Name = "tabPage_MouseMoveEvent";
            this.tabPage_MouseMoveEvent.Size = new System.Drawing.Size(580, 393);
            this.tabPage_MouseMoveEvent.TabIndex = 2;
            this.tabPage_MouseMoveEvent.Text = "Mouse Move Event";
            this.tabPage_MouseMoveEvent.UseVisualStyleBackColor = true;
            // 
            // Button_ColourPreview
            // 
            this.Button_ColourPreview.Location = new System.Drawing.Point(467, 302);
            this.Button_ColourPreview.Name = "Button_ColourPreview";
            this.Button_ColourPreview.Size = new System.Drawing.Size(74, 76);
            this.Button_ColourPreview.TabIndex = 62;
            this.Button_ColourPreview.UseVisualStyleBackColor = true;
            // 
            // groupBox_RelativeToScreen
            // 
            this.groupBox_RelativeToScreen.Controls.Add(this.groupBox2);
            this.groupBox_RelativeToScreen.Controls.Add(this.button_InsertMouseMoveToAreaEvent);
            this.groupBox_RelativeToScreen.Controls.Add(this.button_InsertMouseMoveEvent);
            this.groupBox_RelativeToScreen.Location = new System.Drawing.Point(3, 3);
            this.groupBox_RelativeToScreen.Name = "groupBox_RelativeToScreen";
            this.groupBox_RelativeToScreen.Size = new System.Drawing.Size(409, 121);
            this.groupBox_RelativeToScreen.TabIndex = 42;
            this.groupBox_RelativeToScreen.TabStop = false;
            this.groupBox_RelativeToScreen.Text = "Move Mouse To Coordinates On Screen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TextBox_MousePosY);
            this.groupBox2.Controls.Add(this.TextBox_MousePosX);
            this.groupBox2.Location = new System.Drawing.Point(170, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 56);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cursor Location on Screen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "X:";
            // 
            // TextBox_MousePosY
            // 
            this.TextBox_MousePosY.Enabled = false;
            this.TextBox_MousePosY.Location = new System.Drawing.Point(123, 22);
            this.TextBox_MousePosY.Name = "TextBox_MousePosY";
            this.TextBox_MousePosY.ReadOnly = true;
            this.TextBox_MousePosY.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosY.TabIndex = 15;
            this.TextBox_MousePosY.TabStop = false;
            // 
            // TextBox_MousePosX
            // 
            this.TextBox_MousePosX.Enabled = false;
            this.TextBox_MousePosX.Location = new System.Drawing.Point(28, 22);
            this.TextBox_MousePosX.Name = "TextBox_MousePosX";
            this.TextBox_MousePosX.ReadOnly = true;
            this.TextBox_MousePosX.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosX.TabIndex = 14;
            this.TextBox_MousePosX.TabStop = false;
            // 
            // button_InsertMouseMoveToAreaEvent
            // 
            this.button_InsertMouseMoveToAreaEvent.Location = new System.Drawing.Point(7, 63);
            this.button_InsertMouseMoveToAreaEvent.Name = "button_InsertMouseMoveToAreaEvent";
            this.button_InsertMouseMoveToAreaEvent.Size = new System.Drawing.Size(142, 38);
            this.button_InsertMouseMoveToAreaEvent.TabIndex = 1;
            this.button_InsertMouseMoveToAreaEvent.Text = "Move To a Random Point in an Area";
            this.button_InsertMouseMoveToAreaEvent.UseVisualStyleBackColor = true;
            // 
            // button_InsertMouseMoveEvent
            // 
            this.button_InsertMouseMoveEvent.Location = new System.Drawing.Point(7, 19);
            this.button_InsertMouseMoveEvent.Name = "button_InsertMouseMoveEvent";
            this.button_InsertMouseMoveEvent.Size = new System.Drawing.Size(142, 38);
            this.button_InsertMouseMoveEvent.TabIndex = 0;
            this.button_InsertMouseMoveEvent.Text = "Move To Fixed Point";
            this.button_InsertMouseMoveEvent.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.button_InsertMouseMoveToAreaOnWindowEvent);
            this.groupBox1.Controls.Add(this.button_InsertMouseMoveToPointOnWindowEvent);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 160);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Move Mouse To Coordinates Inside a Window";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.TextBox_ActiveWindowTitle);
            this.groupBox4.Location = new System.Drawing.Point(170, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(221, 60);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Active Window Title";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "X:";
            // 
            // TextBox_ActiveWindowTitle
            // 
            this.TextBox_ActiveWindowTitle.Enabled = false;
            this.TextBox_ActiveWindowTitle.Location = new System.Drawing.Point(26, 25);
            this.TextBox_ActiveWindowTitle.Name = "TextBox_ActiveWindowTitle";
            this.TextBox_ActiveWindowTitle.ReadOnly = true;
            this.TextBox_ActiveWindowTitle.Size = new System.Drawing.Size(161, 20);
            this.TextBox_ActiveWindowTitle.TabIndex = 15;
            this.TextBox_ActiveWindowTitle.TabStop = false;
            // 
            // button_InsertMouseMoveToAreaOnWindowEvent
            // 
            this.button_InsertMouseMoveToAreaOnWindowEvent.Location = new System.Drawing.Point(6, 92);
            this.button_InsertMouseMoveToAreaOnWindowEvent.Name = "button_InsertMouseMoveToAreaOnWindowEvent";
            this.button_InsertMouseMoveToAreaOnWindowEvent.Size = new System.Drawing.Size(142, 38);
            this.button_InsertMouseMoveToAreaOnWindowEvent.TabIndex = 5;
            this.button_InsertMouseMoveToAreaOnWindowEvent.Text = "Move To Random Point in Area on Window";
            this.button_InsertMouseMoveToAreaOnWindowEvent.UseVisualStyleBackColor = true;
            // 
            // button_InsertMouseMoveToPointOnWindowEvent
            // 
            this.button_InsertMouseMoveToPointOnWindowEvent.Location = new System.Drawing.Point(6, 34);
            this.button_InsertMouseMoveToPointOnWindowEvent.Name = "button_InsertMouseMoveToPointOnWindowEvent";
            this.button_InsertMouseMoveToPointOnWindowEvent.Size = new System.Drawing.Size(142, 38);
            this.button_InsertMouseMoveToPointOnWindowEvent.TabIndex = 4;
            this.button_InsertMouseMoveToPointOnWindowEvent.Text = "Move To Fixed Point on Window";
            this.button_InsertMouseMoveToPointOnWindowEvent.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.TextBox_ActiveWindowMouseY);
            this.groupBox3.Controls.Add(this.TextBox_ActiveWindowMouseX);
            this.groupBox3.Location = new System.Drawing.Point(170, 85);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 60);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cursor Location on Active Window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "X:";
            // 
            // TextBox_ActiveWindowMouseY
            // 
            this.TextBox_ActiveWindowMouseY.Enabled = false;
            this.TextBox_ActiveWindowMouseY.Location = new System.Drawing.Point(121, 25);
            this.TextBox_ActiveWindowMouseY.Name = "TextBox_ActiveWindowMouseY";
            this.TextBox_ActiveWindowMouseY.ReadOnly = true;
            this.TextBox_ActiveWindowMouseY.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseY.TabIndex = 15;
            this.TextBox_ActiveWindowMouseY.TabStop = false;
            // 
            // TextBox_ActiveWindowMouseX
            // 
            this.TextBox_ActiveWindowMouseX.Enabled = false;
            this.TextBox_ActiveWindowMouseX.Location = new System.Drawing.Point(26, 25);
            this.TextBox_ActiveWindowMouseX.Name = "TextBox_ActiveWindowMouseX";
            this.TextBox_ActiveWindowMouseX.ReadOnly = true;
            this.TextBox_ActiveWindowMouseX.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseX.TabIndex = 14;
            this.TextBox_ActiveWindowMouseX.TabStop = false;
            // 
            // groupBox_MouseSpeed
            // 
            this.groupBox_MouseSpeed.Controls.Add(this.TrackBar_MouseSpeed);
            this.groupBox_MouseSpeed.Controls.Add(this.NumericUpDown_MouseSpeed);
            this.groupBox_MouseSpeed.Location = new System.Drawing.Point(3, 296);
            this.groupBox_MouseSpeed.Name = "groupBox_MouseSpeed";
            this.groupBox_MouseSpeed.Size = new System.Drawing.Size(458, 82);
            this.groupBox_MouseSpeed.TabIndex = 41;
            this.groupBox_MouseSpeed.TabStop = false;
            this.groupBox_MouseSpeed.Text = "Mouse Speed";
            // 
            // TrackBar_MouseSpeed
            // 
            this.TrackBar_MouseSpeed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TrackBar_MouseSpeed.Cursor = System.Windows.Forms.Cursors.Default;
            this.TrackBar_MouseSpeed.LargeChange = 500;
            this.TrackBar_MouseSpeed.Location = new System.Drawing.Point(6, 31);
            this.TrackBar_MouseSpeed.Maximum = 50;
            this.TrackBar_MouseSpeed.Name = "TrackBar_MouseSpeed";
            this.TrackBar_MouseSpeed.RightToLeftLayout = true;
            this.TrackBar_MouseSpeed.Size = new System.Drawing.Size(292, 45);
            this.TrackBar_MouseSpeed.SmallChange = 100;
            this.TrackBar_MouseSpeed.TabIndex = 39;
            this.TrackBar_MouseSpeed.TickFrequency = 5;
            this.TrackBar_MouseSpeed.Value = 15;
            this.TrackBar_MouseSpeed.Scroll += new System.EventHandler(this.TrackBar_MouseSpeed_Scroll);
            // 
            // NumericUpDown_MouseSpeed
            // 
            this.NumericUpDown_MouseSpeed.Location = new System.Drawing.Point(304, 46);
            this.NumericUpDown_MouseSpeed.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NumericUpDown_MouseSpeed.Name = "NumericUpDown_MouseSpeed";
            this.NumericUpDown_MouseSpeed.Size = new System.Drawing.Size(120, 20);
            this.NumericUpDown_MouseSpeed.TabIndex = 38;
            this.NumericUpDown_MouseSpeed.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.NumericUpDown_MouseSpeed.ValueChanged += new System.EventHandler(this.NumericUpDown_MouseSpeed_ValueChanged);
            // 
            // tabPage_MouseClick
            // 
            this.tabPage_MouseClick.Location = new System.Drawing.Point(4, 22);
            this.tabPage_MouseClick.Name = "tabPage_MouseClick";
            this.tabPage_MouseClick.Size = new System.Drawing.Size(580, 393);
            this.tabPage_MouseClick.TabIndex = 3;
            this.tabPage_MouseClick.Text = "Mouse Click Event";
            this.tabPage_MouseClick.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 732);
            this.Controls.Add(this.tabControl_ScriptEvents);
            this.Controls.Add(this.MenuStrip_MainForm);
            this.Controls.Add(this.button_ShowMouseClickForm);
            this.Controls.Add(this.button_ShowMouseMoveForm);
            this.Controls.Add(this.button_ShowPauseEventForm);
            this.Controls.Add(this.repeatGroupLabel);
            this.Controls.Add(this.NumericUpDown_RepeatAmount);
            this.Controls.Add(this.button_AddRepeatGroup);
            this.Controls.Add(this.button_About);
            this.Controls.Add(this.button_ShowKeyboardEventForm);
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
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RepeatAmount)).EndInit();
            this.MenuStrip_MainForm.ResumeLayout(false);
            this.MenuStrip_MainForm.PerformLayout();
            this.tabControl_ScriptEvents.ResumeLayout(false);
            this.tabPage_MouseMoveEvent.ResumeLayout(false);
            this.groupBox_RelativeToScreen.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox_MouseSpeed.ResumeLayout(false);
            this.groupBox_MouseSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_MouseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MouseSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_StartScript;
        private System.Windows.Forms.Button button_MoveEventUp;
        private System.Windows.Forms.Button button_MoveEventDown;
        public System.Windows.Forms.ListBox ListBox_Events;
        private System.Windows.Forms.Button button_RemoveEvent;
        private System.Windows.Forms.Button button_ShowKeyboardEventForm;
        private System.Windows.Forms.Button button_About;
        public System.Windows.Forms.Label lastActionLabel;
        public System.Windows.Forms.Label statusLabel;
        public System.Windows.Forms.Button registerClickLocationsButton;
        private System.Windows.Forms.Button button_AddRepeatGroup;
        private System.Windows.Forms.NumericUpDown NumericUpDown_RepeatAmount;
        private System.Windows.Forms.Label repeatGroupLabel;
        private System.Windows.Forms.Button button_ShowPauseEventForm;
        private System.Windows.Forms.Button button_ShowMouseMoveForm;
        private System.Windows.Forms.Button button_ShowMouseClickForm;
        private System.Windows.Forms.MenuStrip MenuStrip_MainForm;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem saveScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadScriptToolStripMenuItem1;
        private System.Windows.Forms.TabControl tabControl_ScriptEvents;
        private System.Windows.Forms.TabPage tabPage_PauseEvent;
        private System.Windows.Forms.TabPage tabPage2_KeyboardEvent;
        private System.Windows.Forms.TabPage tabPage_MouseMoveEvent;
        private System.Windows.Forms.GroupBox groupBox_MouseSpeed;
        private System.Windows.Forms.TrackBar TrackBar_MouseSpeed;
        private System.Windows.Forms.NumericUpDown NumericUpDown_MouseSpeed;
        private System.Windows.Forms.TabPage tabPage_MouseClick;
        private System.Windows.Forms.GroupBox groupBox_RelativeToScreen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_InsertMouseMoveToAreaEvent;
        private System.Windows.Forms.Button button_InsertMouseMoveEvent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_MousePosY;
        private System.Windows.Forms.TextBox TextBox_MousePosX;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowTitle;
        private System.Windows.Forms.Button button_InsertMouseMoveToAreaOnWindowEvent;
        private System.Windows.Forms.Button button_InsertMouseMoveToPointOnWindowEvent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseY;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseX;
        private System.Windows.Forms.Button Button_ColourPreview;
    }
}

