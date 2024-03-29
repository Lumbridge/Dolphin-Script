﻿namespace DolphinScript.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button_StartScript = new System.Windows.Forms.Button();
            this.button_MoveEventUp = new System.Windows.Forms.Button();
            this.button_MoveEventDown = new System.Windows.Forms.Button();
            this.button_RemoveEvent = new System.Windows.Forms.Button();
            this.button_AddRepeatGroup = new System.Windows.Forms.Button();
            this.NumericUpDown_RepeatAmount = new System.Windows.Forms.NumericUpDown();
            this.MenuStrip_MainForm = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_ScriptEvents = new System.Windows.Forms.TabControl();
            this.tabPage_PauseEvent = new System.Windows.Forms.TabPage();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.Button_ColourPreview1 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow = new System.Windows.Forms.Button();
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.Button_InsertPauseWhileColourDoesntExistInArea = new System.Windows.Forms.Button();
            this.Button_InsertPauseWhileColourExistsInArea = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Button_AddFixedPause = new System.Windows.Forms.Button();
            this.fixedDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Button_AddRandomPause = new System.Windows.Forms.Button();
            this.upperRandomDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.lowerRandomDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.tabPage2_KeyboardEvent = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.Button_AddSpecialButton = new System.Windows.Forms.Button();
            this.RichTextBox_KeyboardEvent = new System.Windows.Forms.RichTextBox();
            this.ComboBox_SpecialKeys = new System.Windows.Forms.ComboBox();
            this.Button_AddKeypressEvent = new System.Windows.Forms.Button();
            this.tabPage_MouseMoveEvent = new System.Windows.Forms.TabPage();
            this.groupBox_mouseMovementMode = new System.Windows.Forms.GroupBox();
            this.ComboBox_MouseMovementMode = new System.Windows.Forms.ComboBox();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.Button_ColourPreview2 = new System.Windows.Forms.Button();
            this.groupBox_RelativeToScreen = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_MousePosY_1 = new System.Windows.Forms.TextBox();
            this.TextBox_MousePosX_1 = new System.Windows.Forms.TextBox();
            this.Button_InsertMouseMoveToAreaEvent = new System.Windows.Forms.Button();
            this.Button_InsertMouseMoveEvent = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBox_ActiveWindowTitle_1 = new System.Windows.Forms.TextBox();
            this.Button_InsertMouseMoveToAreaOnWindowEvent = new System.Windows.Forms.Button();
            this.Button_InsertMouseMoveToPointOnWindowEvent = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBox_ActiveWindowMouseY_1 = new System.Windows.Forms.TextBox();
            this.TextBox_ActiveWindowMouseX_1 = new System.Windows.Forms.TextBox();
            this.groupBox_MouseSpeed = new System.Windows.Forms.GroupBox();
            this.label_MaxMouseSpeed = new System.Windows.Forms.Label();
            this.label_MinMouseSpeed = new System.Windows.Forms.Label();
            this.NumericUpDown_MaxMouseSpeed = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown_MinMouseSpeed = new System.Windows.Forms.NumericUpDown();
            this.tabPage_MouseMoveToColour = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.Button_ColourPreview3 = new System.Windows.Forms.Button();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.Picturebox_ColourSelectionArea = new System.Windows.Forms.PictureBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.Button_InsertColourSearchAreaWindowEvent = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TextBox_ActiveWindowMouseY_2 = new System.Windows.Forms.TextBox();
            this.TextBox_ActiveWindowMouseX_2 = new System.Windows.Forms.TextBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TextBox_ActiveWindowTitle_2 = new System.Windows.Forms.TextBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TextBox_MousePosY_2 = new System.Windows.Forms.TextBox();
            this.TextBox_MousePosX_2 = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.Button_InsertColourSearchAreaEvent = new System.Windows.Forms.Button();
            this.tabPage_MouseClick = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.Button_InsertRMBUp = new System.Windows.Forms.Button();
            this.Button_InsertMMBUp = new System.Windows.Forms.Button();
            this.Button_InsertLMBUp = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.Button_InsertRMBDown = new System.Windows.Forms.Button();
            this.Button_InsertLMBDown = new System.Windows.Forms.Button();
            this.Button_InsertMMBDown = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.Button_InsertLeftClickEvent = new System.Windows.Forms.Button();
            this.Button_InsertRightClickEvent = new System.Windows.Forms.Button();
            this.Buton_InsertMiddleMouseClickEvent = new System.Windows.Forms.Button();
            this.button_RemoveRepeatGroup = new System.Windows.Forms.Button();
            this.MainDataGrid = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RepeatCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WindowTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WindowHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RepeatAmount)).BeginInit();
            this.MenuStrip_MainForm.SuspendLayout();
            this.tabControl_ScriptEvents.SuspendLayout();
            this.tabPage_PauseEvent.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fixedDelayNumberBox)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upperRandomDelayNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerRandomDelayNumberBox)).BeginInit();
            this.tabPage2_KeyboardEvent.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage_MouseMoveEvent.SuspendLayout();
            this.groupBox_mouseMovementMode.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox_RelativeToScreen.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox_MouseSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MaxMouseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MinMouseSpeed)).BeginInit();
            this.tabPage_MouseMoveToColour.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_ColourSelectionArea)).BeginInit();
            this.groupBox22.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tabPage_MouseClick.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // button_StartScript
            // 
            this.button_StartScript.Location = new System.Drawing.Point(1297, 388);
            this.button_StartScript.Name = "button_StartScript";
            this.button_StartScript.Size = new System.Drawing.Size(101, 35);
            this.button_StartScript.TabIndex = 0;
            this.button_StartScript.Text = "Start";
            this.button_StartScript.UseVisualStyleBackColor = true;
            this.button_StartScript.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // button_MoveEventUp
            // 
            this.button_MoveEventUp.Enabled = false;
            this.button_MoveEventUp.Location = new System.Drawing.Point(1297, 245);
            this.button_MoveEventUp.Name = "button_MoveEventUp";
            this.button_MoveEventUp.Size = new System.Drawing.Size(37, 36);
            this.button_MoveEventUp.TabIndex = 3;
            this.button_MoveEventUp.Text = "↑";
            this.button_MoveEventUp.UseVisualStyleBackColor = true;
            this.button_MoveEventUp.Click += new System.EventHandler(this.MoveElementUpButton_Click);
            // 
            // button_MoveEventDown
            // 
            this.button_MoveEventDown.Enabled = false;
            this.button_MoveEventDown.Location = new System.Drawing.Point(1297, 317);
            this.button_MoveEventDown.Name = "button_MoveEventDown";
            this.button_MoveEventDown.Size = new System.Drawing.Size(37, 36);
            this.button_MoveEventDown.TabIndex = 4;
            this.button_MoveEventDown.Text = "↓";
            this.button_MoveEventDown.UseVisualStyleBackColor = true;
            this.button_MoveEventDown.Click += new System.EventHandler(this.MoveElementDownButton_Click);
            // 
            // button_RemoveEvent
            // 
            this.button_RemoveEvent.Enabled = false;
            this.button_RemoveEvent.Location = new System.Drawing.Point(1297, 287);
            this.button_RemoveEvent.Name = "button_RemoveEvent";
            this.button_RemoveEvent.Size = new System.Drawing.Size(37, 24);
            this.button_RemoveEvent.TabIndex = 16;
            this.button_RemoveEvent.Text = "X";
            this.button_RemoveEvent.UseVisualStyleBackColor = true;
            this.button_RemoveEvent.Click += new System.EventHandler(this.RemoveEventButton_Click);
            // 
            // button_AddRepeatGroup
            // 
            this.button_AddRepeatGroup.Location = new System.Drawing.Point(1340, 271);
            this.button_AddRepeatGroup.Name = "button_AddRepeatGroup";
            this.button_AddRepeatGroup.Size = new System.Drawing.Size(58, 56);
            this.button_AddRepeatGroup.TabIndex = 8;
            this.button_AddRepeatGroup.Text = "Add repeat group";
            this.button_AddRepeatGroup.UseVisualStyleBackColor = true;
            this.button_AddRepeatGroup.Click += new System.EventHandler(this.RepeatGroupButton_Click);
            // 
            // NumericUpDown_RepeatAmount
            // 
            this.NumericUpDown_RepeatAmount.Location = new System.Drawing.Point(1340, 245);
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
            this.NumericUpDown_RepeatAmount.Size = new System.Drawing.Size(58, 20);
            this.NumericUpDown_RepeatAmount.TabIndex = 7;
            this.NumericUpDown_RepeatAmount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // MenuStrip_MainForm
            // 
            this.MenuStrip_MainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File,
            this.helpToolStripMenuItem});
            this.MenuStrip_MainForm.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_MainForm.Name = "MenuStrip_MainForm";
            this.MenuStrip_MainForm.Size = new System.Drawing.Size(1410, 24);
            this.MenuStrip_MainForm.TabIndex = 46;
            this.MenuStrip_MainForm.Text = "File";
            // 
            // ToolStripMenuItem_File
            // 
            this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveScriptToolStripMenuItem,
            this.loadScriptToolStripMenuItem});
            this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
            this.ToolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.ToolStripMenuItem_File.Text = "File";
            // 
            // saveScriptToolStripMenuItem
            // 
            this.saveScriptToolStripMenuItem.Name = "saveScriptToolStripMenuItem";
            this.saveScriptToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveScriptToolStripMenuItem.Text = "Save Script";
            this.saveScriptToolStripMenuItem.Click += new System.EventHandler(this.SaveScriptToolStripMenuItem_Click);
            // 
            // loadScriptToolStripMenuItem
            // 
            this.loadScriptToolStripMenuItem.Name = "loadScriptToolStripMenuItem";
            this.loadScriptToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.loadScriptToolStripMenuItem.Text = "Load Script";
            this.loadScriptToolStripMenuItem.Click += new System.EventHandler(this.LoadScriptToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wikiToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.refreshFormToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // wikiToolStripMenuItem
            // 
            this.wikiToolStripMenuItem.Name = "wikiToolStripMenuItem";
            this.wikiToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.wikiToolStripMenuItem.Text = "Wiki";
            this.wikiToolStripMenuItem.Click += new System.EventHandler(this.WikiToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // refreshFormToolStripMenuItem
            // 
            this.refreshFormToolStripMenuItem.Name = "refreshFormToolStripMenuItem";
            this.refreshFormToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.refreshFormToolStripMenuItem.Text = "Refresh Form";
            this.refreshFormToolStripMenuItem.Click += new System.EventHandler(this.RefreshFormToolStripMenuItem_Click);
            // 
            // tabControl_ScriptEvents
            // 
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage_PauseEvent);
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage2_KeyboardEvent);
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage_MouseMoveEvent);
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage_MouseMoveToColour);
            this.tabControl_ScriptEvents.Controls.Add(this.tabPage_MouseClick);
            this.tabControl_ScriptEvents.Location = new System.Drawing.Point(12, 245);
            this.tabControl_ScriptEvents.Name = "tabControl_ScriptEvents";
            this.tabControl_ScriptEvents.SelectedIndex = 0;
            this.tabControl_ScriptEvents.Size = new System.Drawing.Size(1279, 289);
            this.tabControl_ScriptEvents.TabIndex = 47;
            // 
            // tabPage_PauseEvent
            // 
            this.tabPage_PauseEvent.Controls.Add(this.groupBox25);
            this.tabPage_PauseEvent.Controls.Add(this.groupBox12);
            this.tabPage_PauseEvent.Controls.Add(this.groupBox11);
            this.tabPage_PauseEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPage_PauseEvent.Name = "tabPage_PauseEvent";
            this.tabPage_PauseEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_PauseEvent.Size = new System.Drawing.Size(1271, 263);
            this.tabPage_PauseEvent.TabIndex = 0;
            this.tabPage_PauseEvent.Text = "Pause";
            this.tabPage_PauseEvent.UseVisualStyleBackColor = true;
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.Button_ColourPreview1);
            this.groupBox25.Location = new System.Drawing.Point(618, 6);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(177, 172);
            this.groupBox25.TabIndex = 72;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Pixel colour under mouse";
            // 
            // Button_ColourPreview1
            // 
            this.Button_ColourPreview1.Location = new System.Drawing.Point(6, 19);
            this.Button_ColourPreview1.Name = "Button_ColourPreview1";
            this.Button_ColourPreview1.Size = new System.Drawing.Size(165, 147);
            this.Button_ColourPreview1.TabIndex = 61;
            this.Button_ColourPreview1.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.groupBox14);
            this.groupBox12.Controls.Add(this.groupBox13);
            this.groupBox12.Location = new System.Drawing.Point(308, 6);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(304, 211);
            this.groupBox12.TabIndex = 67;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Colour pauses";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow);
            this.groupBox14.Controls.Add(this.Button_InsertPauseWhileColourExistsInAreaOnWindow);
            this.groupBox14.Location = new System.Drawing.Point(152, 19);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(144, 186);
            this.groupBox14.TabIndex = 1;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Specific window check";
            // 
            // Button_InsertPauseWhileColourDoesntExistInAreaOnWindow
            // 
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Location = new System.Drawing.Point(17, 108);
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Name = "Button_InsertPauseWhileColourDoesntExistInAreaOnWindow";
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Size = new System.Drawing.Size(102, 66);
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.TabIndex = 62;
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = "Pause while colour doesn\'t exist in an area on a window";
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Click += new System.EventHandler(this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow_Click);
            // 
            // Button_InsertPauseWhileColourExistsInAreaOnWindow
            // 
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Location = new System.Drawing.Point(17, 26);
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Name = "Button_InsertPauseWhileColourExistsInAreaOnWindow";
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Size = new System.Drawing.Size(102, 66);
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.TabIndex = 63;
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = "Pause while colour exists in an area on a window";
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Click += new System.EventHandler(this.Button_InsertPauseWhileColourExistsInAreaOnWindow_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.Button_InsertPauseWhileColourDoesntExistInArea);
            this.groupBox13.Controls.Add(this.Button_InsertPauseWhileColourExistsInArea);
            this.groupBox13.Location = new System.Drawing.Point(6, 19);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(140, 186);
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Entire screen check";
            // 
            // Button_InsertPauseWhileColourDoesntExistInArea
            // 
            this.Button_InsertPauseWhileColourDoesntExistInArea.Location = new System.Drawing.Point(13, 98);
            this.Button_InsertPauseWhileColourDoesntExistInArea.Name = "Button_InsertPauseWhileColourDoesntExistInArea";
            this.Button_InsertPauseWhileColourDoesntExistInArea.Size = new System.Drawing.Size(102, 66);
            this.Button_InsertPauseWhileColourDoesntExistInArea.TabIndex = 64;
            this.Button_InsertPauseWhileColourDoesntExistInArea.Text = "Pause while colour doesn\'t exist in an area";
            this.Button_InsertPauseWhileColourDoesntExistInArea.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourDoesntExistInArea.Click += new System.EventHandler(this.Button_InsertPauseWhileColourDoesntExistInArea_Click);
            // 
            // Button_InsertPauseWhileColourExistsInArea
            // 
            this.Button_InsertPauseWhileColourExistsInArea.Location = new System.Drawing.Point(13, 26);
            this.Button_InsertPauseWhileColourExistsInArea.Name = "Button_InsertPauseWhileColourExistsInArea";
            this.Button_InsertPauseWhileColourExistsInArea.Size = new System.Drawing.Size(102, 66);
            this.Button_InsertPauseWhileColourExistsInArea.TabIndex = 65;
            this.Button_InsertPauseWhileColourExistsInArea.Text = "Pause while colour exists in an area";
            this.Button_InsertPauseWhileColourExistsInArea.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourExistsInArea.Click += new System.EventHandler(this.Button_InsertPauseWhileColourExistsInArea_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.groupBox5);
            this.groupBox11.Controls.Add(this.groupBox6);
            this.groupBox11.Location = new System.Drawing.Point(6, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(296, 132);
            this.groupBox11.TabIndex = 66;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Time pauses";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Button_AddFixedPause);
            this.groupBox5.Controls.Add(this.fixedDelayNumberBox);
            this.groupBox5.Location = new System.Drawing.Point(6, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(96, 96);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Fixed pause";
            // 
            // Button_AddFixedPause
            // 
            this.Button_AddFixedPause.Location = new System.Drawing.Point(6, 47);
            this.Button_AddFixedPause.Name = "Button_AddFixedPause";
            this.Button_AddFixedPause.Size = new System.Drawing.Size(79, 35);
            this.Button_AddFixedPause.TabIndex = 52;
            this.Button_AddFixedPause.Text = "Add fixed pause";
            this.Button_AddFixedPause.UseVisualStyleBackColor = true;
            this.Button_AddFixedPause.Click += new System.EventHandler(this.Button_AddFixedPause_Click);
            // 
            // fixedDelayNumberBox
            // 
            this.fixedDelayNumberBox.DecimalPlaces = 1;
            this.fixedDelayNumberBox.Location = new System.Drawing.Point(6, 21);
            this.fixedDelayNumberBox.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.fixedDelayNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.fixedDelayNumberBox.Name = "fixedDelayNumberBox";
            this.fixedDelayNumberBox.Size = new System.Drawing.Size(79, 20);
            this.fixedDelayNumberBox.TabIndex = 51;
            this.fixedDelayNumberBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Button_AddRandomPause);
            this.groupBox6.Controls.Add(this.upperRandomDelayNumberBox);
            this.groupBox6.Controls.Add(this.lowerRandomDelayNumberBox);
            this.groupBox6.Location = new System.Drawing.Point(108, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(177, 96);
            this.groupBox6.TabIndex = 53;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Random pause within range";
            // 
            // Button_AddRandomPause
            // 
            this.Button_AddRandomPause.Location = new System.Drawing.Point(6, 47);
            this.Button_AddRandomPause.Name = "Button_AddRandomPause";
            this.Button_AddRandomPause.Size = new System.Drawing.Size(157, 39);
            this.Button_AddRandomPause.TabIndex = 56;
            this.Button_AddRandomPause.Text = "Add random pause between min and max";
            this.Button_AddRandomPause.UseVisualStyleBackColor = true;
            this.Button_AddRandomPause.Click += new System.EventHandler(this.Button_AddRandomPause_Click);
            // 
            // upperRandomDelayNumberBox
            // 
            this.upperRandomDelayNumberBox.DecimalPlaces = 1;
            this.upperRandomDelayNumberBox.Location = new System.Drawing.Point(87, 21);
            this.upperRandomDelayNumberBox.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.upperRandomDelayNumberBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.upperRandomDelayNumberBox.Name = "upperRandomDelayNumberBox";
            this.upperRandomDelayNumberBox.Size = new System.Drawing.Size(76, 20);
            this.upperRandomDelayNumberBox.TabIndex = 55;
            this.upperRandomDelayNumberBox.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.upperRandomDelayNumberBox.ValueChanged += new System.EventHandler(this.UpperRandomDelayNumberBox_ValueChanged);
            // 
            // lowerRandomDelayNumberBox
            // 
            this.lowerRandomDelayNumberBox.DecimalPlaces = 1;
            this.lowerRandomDelayNumberBox.Location = new System.Drawing.Point(6, 21);
            this.lowerRandomDelayNumberBox.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.lowerRandomDelayNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lowerRandomDelayNumberBox.Name = "lowerRandomDelayNumberBox";
            this.lowerRandomDelayNumberBox.Size = new System.Drawing.Size(75, 20);
            this.lowerRandomDelayNumberBox.TabIndex = 54;
            this.lowerRandomDelayNumberBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.lowerRandomDelayNumberBox.ValueChanged += new System.EventHandler(this.LowerRandomDelayNumberBox_ValueChanged);
            // 
            // tabPage2_KeyboardEvent
            // 
            this.tabPage2_KeyboardEvent.Controls.Add(this.groupBox7);
            this.tabPage2_KeyboardEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPage2_KeyboardEvent.Name = "tabPage2_KeyboardEvent";
            this.tabPage2_KeyboardEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2_KeyboardEvent.Size = new System.Drawing.Size(1271, 263);
            this.tabPage2_KeyboardEvent.TabIndex = 1;
            this.tabPage2_KeyboardEvent.Text = "Keyboard";
            this.tabPage2_KeyboardEvent.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.Button_AddSpecialButton);
            this.groupBox7.Controls.Add(this.RichTextBox_KeyboardEvent);
            this.groupBox7.Controls.Add(this.ComboBox_SpecialKeys);
            this.groupBox7.Controls.Add(this.Button_AddKeypressEvent);
            this.groupBox7.Location = new System.Drawing.Point(6, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(343, 252);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "The keyboard event will repeat everything in the box below";
            // 
            // Button_AddSpecialButton
            // 
            this.Button_AddSpecialButton.Location = new System.Drawing.Point(26, 148);
            this.Button_AddSpecialButton.Name = "Button_AddSpecialButton";
            this.Button_AddSpecialButton.Size = new System.Drawing.Size(282, 30);
            this.Button_AddSpecialButton.TabIndex = 32;
            this.Button_AddSpecialButton.Text = "Add special button";
            this.Button_AddSpecialButton.UseVisualStyleBackColor = true;
            this.Button_AddSpecialButton.Click += new System.EventHandler(this.Button_AddSpecialButton_Click);
            // 
            // RichTextBox_KeyboardEvent
            // 
            this.RichTextBox_KeyboardEvent.Location = new System.Drawing.Point(26, 19);
            this.RichTextBox_KeyboardEvent.Multiline = false;
            this.RichTextBox_KeyboardEvent.Name = "RichTextBox_KeyboardEvent";
            this.RichTextBox_KeyboardEvent.Size = new System.Drawing.Size(282, 96);
            this.RichTextBox_KeyboardEvent.TabIndex = 31;
            this.RichTextBox_KeyboardEvent.Text = "";
            // 
            // ComboBox_SpecialKeys
            // 
            this.ComboBox_SpecialKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_SpecialKeys.FormattingEnabled = true;
            this.ComboBox_SpecialKeys.Location = new System.Drawing.Point(26, 121);
            this.ComboBox_SpecialKeys.Name = "ComboBox_SpecialKeys";
            this.ComboBox_SpecialKeys.Size = new System.Drawing.Size(282, 21);
            this.ComboBox_SpecialKeys.TabIndex = 29;
            // 
            // Button_AddKeypressEvent
            // 
            this.Button_AddKeypressEvent.Location = new System.Drawing.Point(26, 184);
            this.Button_AddKeypressEvent.Name = "Button_AddKeypressEvent";
            this.Button_AddKeypressEvent.Size = new System.Drawing.Size(282, 48);
            this.Button_AddKeypressEvent.TabIndex = 30;
            this.Button_AddKeypressEvent.Text = "Add keypess event to event list";
            this.Button_AddKeypressEvent.UseVisualStyleBackColor = true;
            this.Button_AddKeypressEvent.Click += new System.EventHandler(this.Button_AddKeypressEvent_Click);
            // 
            // tabPage_MouseMoveEvent
            // 
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox_mouseMovementMode);
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox20);
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox_RelativeToScreen);
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox1);
            this.tabPage_MouseMoveEvent.Controls.Add(this.groupBox_MouseSpeed);
            this.tabPage_MouseMoveEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPage_MouseMoveEvent.Name = "tabPage_MouseMoveEvent";
            this.tabPage_MouseMoveEvent.Size = new System.Drawing.Size(1271, 263);
            this.tabPage_MouseMoveEvent.TabIndex = 2;
            this.tabPage_MouseMoveEvent.Text = "Mouse move";
            this.tabPage_MouseMoveEvent.UseVisualStyleBackColor = true;
            // 
            // groupBox_mouseMovementMode
            // 
            this.groupBox_mouseMovementMode.Controls.Add(this.ComboBox_MouseMovementMode);
            this.groupBox_mouseMovementMode.Location = new System.Drawing.Point(3, 130);
            this.groupBox_mouseMovementMode.Name = "groupBox_mouseMovementMode";
            this.groupBox_mouseMovementMode.Size = new System.Drawing.Size(227, 65);
            this.groupBox_mouseMovementMode.TabIndex = 64;
            this.groupBox_mouseMovementMode.TabStop = false;
            this.groupBox_mouseMovementMode.Text = "Mouse movement mode";
            // 
            // ComboBox_MouseMovementMode
            // 
            this.ComboBox_MouseMovementMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_MouseMovementMode.FormattingEnabled = true;
            this.ComboBox_MouseMovementMode.Location = new System.Drawing.Point(14, 24);
            this.ComboBox_MouseMovementMode.Name = "ComboBox_MouseMovementMode";
            this.ComboBox_MouseMovementMode.Size = new System.Drawing.Size(195, 21);
            this.ComboBox_MouseMovementMode.TabIndex = 0;
            this.ComboBox_MouseMovementMode.SelectedIndexChanged += new System.EventHandler(this.ComboBox_MouseMovementMode_SelectedIndexChanged);
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.Button_ColourPreview2);
            this.groupBox20.Location = new System.Drawing.Point(813, 3);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(160, 163);
            this.groupBox20.TabIndex = 63;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Pixel colour under mouse";
            // 
            // Button_ColourPreview2
            // 
            this.Button_ColourPreview2.Location = new System.Drawing.Point(6, 19);
            this.Button_ColourPreview2.Name = "Button_ColourPreview2";
            this.Button_ColourPreview2.Size = new System.Drawing.Size(148, 138);
            this.Button_ColourPreview2.TabIndex = 62;
            this.Button_ColourPreview2.UseVisualStyleBackColor = true;
            // 
            // groupBox_RelativeToScreen
            // 
            this.groupBox_RelativeToScreen.Controls.Add(this.groupBox2);
            this.groupBox_RelativeToScreen.Controls.Add(this.Button_InsertMouseMoveToAreaEvent);
            this.groupBox_RelativeToScreen.Controls.Add(this.Button_InsertMouseMoveEvent);
            this.groupBox_RelativeToScreen.Location = new System.Drawing.Point(3, 3);
            this.groupBox_RelativeToScreen.Name = "groupBox_RelativeToScreen";
            this.groupBox_RelativeToScreen.Size = new System.Drawing.Size(409, 121);
            this.groupBox_RelativeToScreen.TabIndex = 42;
            this.groupBox_RelativeToScreen.TabStop = false;
            this.groupBox_RelativeToScreen.Text = "Move mouse cursor to coordinates on screen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TextBox_MousePosY_1);
            this.groupBox2.Controls.Add(this.TextBox_MousePosX_1);
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
            // TextBox_MousePosY_1
            // 
            this.TextBox_MousePosY_1.Enabled = false;
            this.TextBox_MousePosY_1.Location = new System.Drawing.Point(123, 22);
            this.TextBox_MousePosY_1.Name = "TextBox_MousePosY_1";
            this.TextBox_MousePosY_1.ReadOnly = true;
            this.TextBox_MousePosY_1.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosY_1.TabIndex = 15;
            this.TextBox_MousePosY_1.TabStop = false;
            // 
            // TextBox_MousePosX_1
            // 
            this.TextBox_MousePosX_1.Enabled = false;
            this.TextBox_MousePosX_1.Location = new System.Drawing.Point(28, 22);
            this.TextBox_MousePosX_1.Name = "TextBox_MousePosX_1";
            this.TextBox_MousePosX_1.ReadOnly = true;
            this.TextBox_MousePosX_1.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosX_1.TabIndex = 14;
            this.TextBox_MousePosX_1.TabStop = false;
            // 
            // Button_InsertMouseMoveToAreaEvent
            // 
            this.Button_InsertMouseMoveToAreaEvent.Location = new System.Drawing.Point(7, 63);
            this.Button_InsertMouseMoveToAreaEvent.Name = "Button_InsertMouseMoveToAreaEvent";
            this.Button_InsertMouseMoveToAreaEvent.Size = new System.Drawing.Size(142, 38);
            this.Button_InsertMouseMoveToAreaEvent.TabIndex = 1;
            this.Button_InsertMouseMoveToAreaEvent.Text = "Move to a random point in an area";
            this.Button_InsertMouseMoveToAreaEvent.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveToAreaEvent.Click += new System.EventHandler(this.Button_InsertMouseMoveToAreaEvent_Click);
            // 
            // Button_InsertMouseMoveEvent
            // 
            this.Button_InsertMouseMoveEvent.Location = new System.Drawing.Point(7, 19);
            this.Button_InsertMouseMoveEvent.Name = "Button_InsertMouseMoveEvent";
            this.Button_InsertMouseMoveEvent.Size = new System.Drawing.Size(142, 38);
            this.Button_InsertMouseMoveEvent.TabIndex = 0;
            this.Button_InsertMouseMoveEvent.Text = "Move to fixed point";
            this.Button_InsertMouseMoveEvent.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveEvent.Click += new System.EventHandler(this.Button_InsertMouseMoveEvent_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.Button_InsertMouseMoveToAreaOnWindowEvent);
            this.groupBox1.Controls.Add(this.Button_InsertMouseMoveToPointOnWindowEvent);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(418, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 160);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Move mouse cursor to coordinates inside a window";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.TextBox_ActiveWindowTitle_1);
            this.groupBox4.Location = new System.Drawing.Point(170, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(204, 60);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Active window title";
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
            // TextBox_ActiveWindowTitle_1
            // 
            this.TextBox_ActiveWindowTitle_1.Enabled = false;
            this.TextBox_ActiveWindowTitle_1.Location = new System.Drawing.Point(26, 25);
            this.TextBox_ActiveWindowTitle_1.Name = "TextBox_ActiveWindowTitle_1";
            this.TextBox_ActiveWindowTitle_1.ReadOnly = true;
            this.TextBox_ActiveWindowTitle_1.Size = new System.Drawing.Size(161, 20);
            this.TextBox_ActiveWindowTitle_1.TabIndex = 15;
            this.TextBox_ActiveWindowTitle_1.TabStop = false;
            // 
            // Button_InsertMouseMoveToAreaOnWindowEvent
            // 
            this.Button_InsertMouseMoveToAreaOnWindowEvent.Location = new System.Drawing.Point(6, 92);
            this.Button_InsertMouseMoveToAreaOnWindowEvent.Name = "Button_InsertMouseMoveToAreaOnWindowEvent";
            this.Button_InsertMouseMoveToAreaOnWindowEvent.Size = new System.Drawing.Size(142, 38);
            this.Button_InsertMouseMoveToAreaOnWindowEvent.TabIndex = 5;
            this.Button_InsertMouseMoveToAreaOnWindowEvent.Text = "Move to random point in area on window";
            this.Button_InsertMouseMoveToAreaOnWindowEvent.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveToAreaOnWindowEvent.Click += new System.EventHandler(this.Button_InsertMouseMoveToAreaOnWindowEvent_Click);
            // 
            // Button_InsertMouseMoveToPointOnWindowEvent
            // 
            this.Button_InsertMouseMoveToPointOnWindowEvent.Location = new System.Drawing.Point(6, 34);
            this.Button_InsertMouseMoveToPointOnWindowEvent.Name = "Button_InsertMouseMoveToPointOnWindowEvent";
            this.Button_InsertMouseMoveToPointOnWindowEvent.Size = new System.Drawing.Size(142, 38);
            this.Button_InsertMouseMoveToPointOnWindowEvent.TabIndex = 4;
            this.Button_InsertMouseMoveToPointOnWindowEvent.Text = "Move to fixed point on window";
            this.Button_InsertMouseMoveToPointOnWindowEvent.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveToPointOnWindowEvent.Click += new System.EventHandler(this.Button_InsertMouseMoveToPointOnWindowEvent_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.TextBox_ActiveWindowMouseY_1);
            this.groupBox3.Controls.Add(this.TextBox_ActiveWindowMouseX_1);
            this.groupBox3.Location = new System.Drawing.Point(170, 85);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 60);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cursor location on active window";
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
            // TextBox_ActiveWindowMouseY_1
            // 
            this.TextBox_ActiveWindowMouseY_1.Enabled = false;
            this.TextBox_ActiveWindowMouseY_1.Location = new System.Drawing.Point(121, 25);
            this.TextBox_ActiveWindowMouseY_1.Name = "TextBox_ActiveWindowMouseY_1";
            this.TextBox_ActiveWindowMouseY_1.ReadOnly = true;
            this.TextBox_ActiveWindowMouseY_1.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseY_1.TabIndex = 15;
            this.TextBox_ActiveWindowMouseY_1.TabStop = false;
            // 
            // TextBox_ActiveWindowMouseX_1
            // 
            this.TextBox_ActiveWindowMouseX_1.Enabled = false;
            this.TextBox_ActiveWindowMouseX_1.Location = new System.Drawing.Point(26, 25);
            this.TextBox_ActiveWindowMouseX_1.Name = "TextBox_ActiveWindowMouseX_1";
            this.TextBox_ActiveWindowMouseX_1.ReadOnly = true;
            this.TextBox_ActiveWindowMouseX_1.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseX_1.TabIndex = 14;
            this.TextBox_ActiveWindowMouseX_1.TabStop = false;
            // 
            // groupBox_MouseSpeed
            // 
            this.groupBox_MouseSpeed.Controls.Add(this.label_MaxMouseSpeed);
            this.groupBox_MouseSpeed.Controls.Add(this.label_MinMouseSpeed);
            this.groupBox_MouseSpeed.Controls.Add(this.NumericUpDown_MaxMouseSpeed);
            this.groupBox_MouseSpeed.Controls.Add(this.NumericUpDown_MinMouseSpeed);
            this.groupBox_MouseSpeed.Location = new System.Drawing.Point(236, 130);
            this.groupBox_MouseSpeed.Name = "groupBox_MouseSpeed";
            this.groupBox_MouseSpeed.Size = new System.Drawing.Size(176, 82);
            this.groupBox_MouseSpeed.TabIndex = 41;
            this.groupBox_MouseSpeed.TabStop = false;
            this.groupBox_MouseSpeed.Text = "Mouse speed";
            // 
            // label_MaxMouseSpeed
            // 
            this.label_MaxMouseSpeed.AutoSize = true;
            this.label_MaxMouseSpeed.Location = new System.Drawing.Point(6, 53);
            this.label_MaxMouseSpeed.Name = "label_MaxMouseSpeed";
            this.label_MaxMouseSpeed.Size = new System.Drawing.Size(61, 13);
            this.label_MaxMouseSpeed.TabIndex = 42;
            this.label_MaxMouseSpeed.Text = "Max Speed";
            // 
            // label_MinMouseSpeed
            // 
            this.label_MinMouseSpeed.AutoSize = true;
            this.label_MinMouseSpeed.Location = new System.Drawing.Point(6, 26);
            this.label_MinMouseSpeed.Name = "label_MinMouseSpeed";
            this.label_MinMouseSpeed.Size = new System.Drawing.Size(58, 13);
            this.label_MinMouseSpeed.TabIndex = 41;
            this.label_MinMouseSpeed.Text = "Min Speed";
            // 
            // NumericUpDown_MaxMouseSpeed
            // 
            this.NumericUpDown_MaxMouseSpeed.Location = new System.Drawing.Point(73, 51);
            this.NumericUpDown_MaxMouseSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NumericUpDown_MaxMouseSpeed.Name = "NumericUpDown_MaxMouseSpeed";
            this.NumericUpDown_MaxMouseSpeed.Size = new System.Drawing.Size(87, 20);
            this.NumericUpDown_MaxMouseSpeed.TabIndex = 40;
            this.NumericUpDown_MaxMouseSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.NumericUpDown_MaxMouseSpeed.ValueChanged += new System.EventHandler(this.NumericUpDown_MaxMouseSpeed_ValueChanged);
            // 
            // NumericUpDown_MinMouseSpeed
            // 
            this.NumericUpDown_MinMouseSpeed.Location = new System.Drawing.Point(73, 26);
            this.NumericUpDown_MinMouseSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NumericUpDown_MinMouseSpeed.Name = "NumericUpDown_MinMouseSpeed";
            this.NumericUpDown_MinMouseSpeed.Size = new System.Drawing.Size(87, 20);
            this.NumericUpDown_MinMouseSpeed.TabIndex = 38;
            this.NumericUpDown_MinMouseSpeed.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.NumericUpDown_MinMouseSpeed.ValueChanged += new System.EventHandler(this.NumericUpDown_MinMouseSpeed_ValueChanged);
            // 
            // tabPage_MouseMoveToColour
            // 
            this.tabPage_MouseMoveToColour.Controls.Add(this.groupBox21);
            this.tabPage_MouseMoveToColour.Controls.Add(this.groupBox24);
            this.tabPage_MouseMoveToColour.Controls.Add(this.groupBox22);
            this.tabPage_MouseMoveToColour.Controls.Add(this.groupBox16);
            this.tabPage_MouseMoveToColour.Controls.Add(this.groupBox15);
            this.tabPage_MouseMoveToColour.Location = new System.Drawing.Point(4, 22);
            this.tabPage_MouseMoveToColour.Name = "tabPage_MouseMoveToColour";
            this.tabPage_MouseMoveToColour.Size = new System.Drawing.Size(1271, 263);
            this.tabPage_MouseMoveToColour.TabIndex = 4;
            this.tabPage_MouseMoveToColour.Text = "Mouse move to colour";
            this.tabPage_MouseMoveToColour.UseVisualStyleBackColor = true;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.Button_ColourPreview3);
            this.groupBox21.Location = new System.Drawing.Point(796, 3);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(177, 172);
            this.groupBox21.TabIndex = 71;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Pixel colour under mouse";
            // 
            // Button_ColourPreview3
            // 
            this.Button_ColourPreview3.Location = new System.Drawing.Point(6, 18);
            this.Button_ColourPreview3.Name = "Button_ColourPreview3";
            this.Button_ColourPreview3.Size = new System.Drawing.Size(165, 148);
            this.Button_ColourPreview3.TabIndex = 62;
            this.Button_ColourPreview3.UseVisualStyleBackColor = true;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.Picturebox_ColourSelectionArea);
            this.groupBox24.Location = new System.Drawing.Point(483, 3);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(307, 172);
            this.groupBox24.TabIndex = 72;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Multi-colour search area";
            // 
            // Picturebox_ColourSelectionArea
            // 
            this.Picturebox_ColourSelectionArea.Location = new System.Drawing.Point(6, 19);
            this.Picturebox_ColourSelectionArea.Name = "Picturebox_ColourSelectionArea";
            this.Picturebox_ColourSelectionArea.Size = new System.Drawing.Size(295, 147);
            this.Picturebox_ColourSelectionArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picturebox_ColourSelectionArea.TabIndex = 50;
            this.Picturebox_ColourSelectionArea.TabStop = false;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.Button_InsertColourSearchAreaWindowEvent);
            this.groupBox22.Location = new System.Drawing.Point(3, 70);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(256, 63);
            this.groupBox22.TabIndex = 70;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Move cursor to colours in an area on a window";
            // 
            // Button_InsertColourSearchAreaWindowEvent
            // 
            this.Button_InsertColourSearchAreaWindowEvent.Location = new System.Drawing.Point(6, 19);
            this.Button_InsertColourSearchAreaWindowEvent.Name = "Button_InsertColourSearchAreaWindowEvent";
            this.Button_InsertColourSearchAreaWindowEvent.Size = new System.Drawing.Size(242, 34);
            this.Button_InsertColourSearchAreaWindowEvent.TabIndex = 66;
            this.Button_InsertColourSearchAreaWindowEvent.Text = "Colour search area on specific window";
            this.Button_InsertColourSearchAreaWindowEvent.UseVisualStyleBackColor = true;
            this.Button_InsertColourSearchAreaWindowEvent.Click += new System.EventHandler(this.Button_InsertColourSearchAreaWindowEvent_Click);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.groupBox19);
            this.groupBox16.Controls.Add(this.groupBox18);
            this.groupBox16.Controls.Add(this.groupBox17);
            this.groupBox16.Location = new System.Drawing.Point(265, 3);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(212, 214);
            this.groupBox16.TabIndex = 70;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Cursor and window information";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.label9);
            this.groupBox19.Controls.Add(this.label10);
            this.groupBox19.Controls.Add(this.TextBox_ActiveWindowMouseY_2);
            this.groupBox19.Controls.Add(this.TextBox_ActiveWindowMouseX_2);
            this.groupBox19.Location = new System.Drawing.Point(6, 147);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(201, 60);
            this.groupBox19.TabIndex = 20;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Cursor location on active window";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(98, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Y:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "X:";
            // 
            // TextBox_ActiveWindowMouseY_2
            // 
            this.TextBox_ActiveWindowMouseY_2.Enabled = false;
            this.TextBox_ActiveWindowMouseY_2.Location = new System.Drawing.Point(121, 25);
            this.TextBox_ActiveWindowMouseY_2.Name = "TextBox_ActiveWindowMouseY_2";
            this.TextBox_ActiveWindowMouseY_2.ReadOnly = true;
            this.TextBox_ActiveWindowMouseY_2.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseY_2.TabIndex = 15;
            this.TextBox_ActiveWindowMouseY_2.TabStop = false;
            // 
            // TextBox_ActiveWindowMouseX_2
            // 
            this.TextBox_ActiveWindowMouseX_2.Enabled = false;
            this.TextBox_ActiveWindowMouseX_2.Location = new System.Drawing.Point(26, 25);
            this.TextBox_ActiveWindowMouseX_2.Name = "TextBox_ActiveWindowMouseX_2";
            this.TextBox_ActiveWindowMouseX_2.ReadOnly = true;
            this.TextBox_ActiveWindowMouseX_2.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseX_2.TabIndex = 14;
            this.TextBox_ActiveWindowMouseX_2.TabStop = false;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label8);
            this.groupBox18.Controls.Add(this.TextBox_ActiveWindowTitle_2);
            this.groupBox18.Location = new System.Drawing.Point(6, 81);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(201, 60);
            this.groupBox18.TabIndex = 19;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Active window title";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "X:";
            // 
            // TextBox_ActiveWindowTitle_2
            // 
            this.TextBox_ActiveWindowTitle_2.Enabled = false;
            this.TextBox_ActiveWindowTitle_2.Location = new System.Drawing.Point(26, 25);
            this.TextBox_ActiveWindowTitle_2.Name = "TextBox_ActiveWindowTitle_2";
            this.TextBox_ActiveWindowTitle_2.ReadOnly = true;
            this.TextBox_ActiveWindowTitle_2.Size = new System.Drawing.Size(161, 20);
            this.TextBox_ActiveWindowTitle_2.TabIndex = 15;
            this.TextBox_ActiveWindowTitle_2.TabStop = false;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.label5);
            this.groupBox17.Controls.Add(this.label7);
            this.groupBox17.Controls.Add(this.TextBox_MousePosY_2);
            this.groupBox17.Controls.Add(this.TextBox_MousePosX_2);
            this.groupBox17.Location = new System.Drawing.Point(6, 19);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(201, 56);
            this.groupBox17.TabIndex = 3;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Cursor location on screen";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Y:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "X:";
            // 
            // TextBox_MousePosY_2
            // 
            this.TextBox_MousePosY_2.Enabled = false;
            this.TextBox_MousePosY_2.Location = new System.Drawing.Point(123, 22);
            this.TextBox_MousePosY_2.Name = "TextBox_MousePosY_2";
            this.TextBox_MousePosY_2.ReadOnly = true;
            this.TextBox_MousePosY_2.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosY_2.TabIndex = 15;
            this.TextBox_MousePosY_2.TabStop = false;
            // 
            // TextBox_MousePosX_2
            // 
            this.TextBox_MousePosX_2.Enabled = false;
            this.TextBox_MousePosX_2.Location = new System.Drawing.Point(28, 22);
            this.TextBox_MousePosX_2.Name = "TextBox_MousePosX_2";
            this.TextBox_MousePosX_2.ReadOnly = true;
            this.TextBox_MousePosX_2.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosX_2.TabIndex = 14;
            this.TextBox_MousePosX_2.TabStop = false;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.Button_InsertColourSearchAreaEvent);
            this.groupBox15.Location = new System.Drawing.Point(3, 3);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(256, 61);
            this.groupBox15.TabIndex = 69;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Move cursor to colours in an area on screen";
            // 
            // Button_InsertColourSearchAreaEvent
            // 
            this.Button_InsertColourSearchAreaEvent.Location = new System.Drawing.Point(6, 19);
            this.Button_InsertColourSearchAreaEvent.Name = "Button_InsertColourSearchAreaEvent";
            this.Button_InsertColourSearchAreaEvent.Size = new System.Drawing.Size(242, 34);
            this.Button_InsertColourSearchAreaEvent.TabIndex = 67;
            this.Button_InsertColourSearchAreaEvent.Text = "Register a screen area colour search event";
            this.Button_InsertColourSearchAreaEvent.UseVisualStyleBackColor = true;
            this.Button_InsertColourSearchAreaEvent.Click += new System.EventHandler(this.Button_InsertColourSearchAreaEvent_Click);
            // 
            // tabPage_MouseClick
            // 
            this.tabPage_MouseClick.Controls.Add(this.groupBox10);
            this.tabPage_MouseClick.Controls.Add(this.groupBox9);
            this.tabPage_MouseClick.Controls.Add(this.groupBox8);
            this.tabPage_MouseClick.Location = new System.Drawing.Point(4, 22);
            this.tabPage_MouseClick.Name = "tabPage_MouseClick";
            this.tabPage_MouseClick.Size = new System.Drawing.Size(1271, 263);
            this.tabPage_MouseClick.TabIndex = 3;
            this.tabPage_MouseClick.Text = "Mouse click";
            this.tabPage_MouseClick.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.Button_InsertRMBUp);
            this.groupBox10.Controls.Add(this.Button_InsertMMBUp);
            this.groupBox10.Controls.Add(this.Button_InsertLMBUp);
            this.groupBox10.Location = new System.Drawing.Point(521, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(251, 91);
            this.groupBox10.TabIndex = 67;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Release mouse buttons";
            // 
            // Button_InsertRMBUp
            // 
            this.Button_InsertRMBUp.Location = new System.Drawing.Point(168, 19);
            this.Button_InsertRMBUp.Name = "Button_InsertRMBUp";
            this.Button_InsertRMBUp.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertRMBUp.TabIndex = 73;
            this.Button_InsertRMBUp.Text = "Release right mouse button";
            this.Button_InsertRMBUp.UseVisualStyleBackColor = true;
            this.Button_InsertRMBUp.Click += new System.EventHandler(this.Button_InsertRMBUp_Click);
            // 
            // Button_InsertMMBUp
            // 
            this.Button_InsertMMBUp.Location = new System.Drawing.Point(87, 19);
            this.Button_InsertMMBUp.Name = "Button_InsertMMBUp";
            this.Button_InsertMMBUp.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertMMBUp.TabIndex = 72;
            this.Button_InsertMMBUp.Text = "Release middle mouse button";
            this.Button_InsertMMBUp.UseVisualStyleBackColor = true;
            this.Button_InsertMMBUp.Click += new System.EventHandler(this.Button_InsertMMBUp_Click);
            // 
            // Button_InsertLMBUp
            // 
            this.Button_InsertLMBUp.Location = new System.Drawing.Point(6, 19);
            this.Button_InsertLMBUp.Name = "Button_InsertLMBUp";
            this.Button_InsertLMBUp.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertLMBUp.TabIndex = 71;
            this.Button_InsertLMBUp.Text = "Release left mouse button";
            this.Button_InsertLMBUp.UseVisualStyleBackColor = true;
            this.Button_InsertLMBUp.Click += new System.EventHandler(this.Button_InsertLMBUp_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.Button_InsertRMBDown);
            this.groupBox9.Controls.Add(this.Button_InsertLMBDown);
            this.groupBox9.Controls.Add(this.Button_InsertMMBDown);
            this.groupBox9.Location = new System.Drawing.Point(265, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(250, 91);
            this.groupBox9.TabIndex = 66;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Hold mouse buttons";
            // 
            // Button_InsertRMBDown
            // 
            this.Button_InsertRMBDown.Location = new System.Drawing.Point(168, 19);
            this.Button_InsertRMBDown.Name = "Button_InsertRMBDown";
            this.Button_InsertRMBDown.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertRMBDown.TabIndex = 69;
            this.Button_InsertRMBDown.Text = "Hold right mouse button down";
            this.Button_InsertRMBDown.UseVisualStyleBackColor = true;
            this.Button_InsertRMBDown.Click += new System.EventHandler(this.Button_InsertRMBDown_Click);
            // 
            // Button_InsertLMBDown
            // 
            this.Button_InsertLMBDown.Location = new System.Drawing.Point(6, 19);
            this.Button_InsertLMBDown.Name = "Button_InsertLMBDown";
            this.Button_InsertLMBDown.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertLMBDown.TabIndex = 65;
            this.Button_InsertLMBDown.Text = "Hold left mouse button down";
            this.Button_InsertLMBDown.UseVisualStyleBackColor = true;
            this.Button_InsertLMBDown.Click += new System.EventHandler(this.Button_InsertLMBDown_Click);
            // 
            // Button_InsertMMBDown
            // 
            this.Button_InsertMMBDown.Location = new System.Drawing.Point(87, 19);
            this.Button_InsertMMBDown.Name = "Button_InsertMMBDown";
            this.Button_InsertMMBDown.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertMMBDown.TabIndex = 67;
            this.Button_InsertMMBDown.Text = "Hold middle mouse button down";
            this.Button_InsertMMBDown.UseVisualStyleBackColor = true;
            this.Button_InsertMMBDown.Click += new System.EventHandler(this.Button_InsertMMBDown_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.Button_InsertLeftClickEvent);
            this.groupBox8.Controls.Add(this.Button_InsertRightClickEvent);
            this.groupBox8.Controls.Add(this.Buton_InsertMiddleMouseClickEvent);
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(256, 91);
            this.groupBox8.TabIndex = 65;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Mouse clicks";
            // 
            // Button_InsertLeftClickEvent
            // 
            this.Button_InsertLeftClickEvent.Location = new System.Drawing.Point(6, 19);
            this.Button_InsertLeftClickEvent.Name = "Button_InsertLeftClickEvent";
            this.Button_InsertLeftClickEvent.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertLeftClickEvent.TabIndex = 56;
            this.Button_InsertLeftClickEvent.Text = "Left click";
            this.Button_InsertLeftClickEvent.UseVisualStyleBackColor = true;
            this.Button_InsertLeftClickEvent.Click += new System.EventHandler(this.Button_InsertLeftClickEvent_Click);
            // 
            // Button_InsertRightClickEvent
            // 
            this.Button_InsertRightClickEvent.Location = new System.Drawing.Point(168, 19);
            this.Button_InsertRightClickEvent.Name = "Button_InsertRightClickEvent";
            this.Button_InsertRightClickEvent.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertRightClickEvent.TabIndex = 57;
            this.Button_InsertRightClickEvent.Text = "Right click";
            this.Button_InsertRightClickEvent.UseVisualStyleBackColor = true;
            this.Button_InsertRightClickEvent.Click += new System.EventHandler(this.Button_InsertRightClickEvent_Click);
            // 
            // Buton_InsertMiddleMouseClickEvent
            // 
            this.Buton_InsertMiddleMouseClickEvent.Location = new System.Drawing.Point(87, 19);
            this.Buton_InsertMiddleMouseClickEvent.Name = "Buton_InsertMiddleMouseClickEvent";
            this.Buton_InsertMiddleMouseClickEvent.Size = new System.Drawing.Size(75, 62);
            this.Buton_InsertMiddleMouseClickEvent.TabIndex = 58;
            this.Buton_InsertMiddleMouseClickEvent.Text = "Middle mouse click";
            this.Buton_InsertMiddleMouseClickEvent.UseVisualStyleBackColor = true;
            this.Buton_InsertMiddleMouseClickEvent.Click += new System.EventHandler(this.Button_InsertMiddleMouseClickEvent_Click);
            // 
            // button_RemoveRepeatGroup
            // 
            this.button_RemoveRepeatGroup.Location = new System.Drawing.Point(1340, 333);
            this.button_RemoveRepeatGroup.Name = "button_RemoveRepeatGroup";
            this.button_RemoveRepeatGroup.Size = new System.Drawing.Size(58, 49);
            this.button_RemoveRepeatGroup.TabIndex = 48;
            this.button_RemoveRepeatGroup.Text = "Remove repeat group";
            this.button_RemoveRepeatGroup.UseVisualStyleBackColor = true;
            this.button_RemoveRepeatGroup.Click += new System.EventHandler(this.Button_RemoveRepeatGroup_Click);
            // 
            // MainDataGrid
            // 
            this.MainDataGrid.AllowUserToAddRows = false;
            this.MainDataGrid.AllowUserToDeleteRows = false;
            this.MainDataGrid.AllowUserToResizeRows = false;
            this.MainDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.MainDataGrid.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.MainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.ScriptEvent,
            this.Description,
            this.GroupId,
            this.RepeatCount,
            this.WindowTitle,
            this.WindowHandle,
            this.ProcessName,
            this.CurrentAction});
            this.MainDataGrid.Location = new System.Drawing.Point(12, 27);
            this.MainDataGrid.Name = "MainDataGrid";
            this.MainDataGrid.ReadOnly = true;
            this.MainDataGrid.RowHeadersVisible = false;
            this.MainDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MainDataGrid.Size = new System.Drawing.Size(1386, 212);
            this.MainDataGrid.TabIndex = 49;
            this.MainDataGrid.SelectionChanged += new System.EventHandler(this.MainDataGrid_SelectedIndexChanged);
            this.MainDataGrid.DoubleClick += new System.EventHandler(this.MainDataGrid_DoubleClick);
            // 
            // Index
            // 
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 58;
            // 
            // ScriptEvent
            // 
            this.ScriptEvent.HeaderText = "Script Event";
            this.ScriptEvent.Name = "ScriptEvent";
            this.ScriptEvent.ReadOnly = true;
            this.ScriptEvent.Width = 83;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 85;
            // 
            // GroupId
            // 
            this.GroupId.HeaderText = "Group ID";
            this.GroupId.Name = "GroupId";
            this.GroupId.ReadOnly = true;
            this.GroupId.Width = 70;
            // 
            // RepeatCount
            // 
            this.RepeatCount.HeaderText = "Repeat Count";
            this.RepeatCount.Name = "RepeatCount";
            this.RepeatCount.ReadOnly = true;
            this.RepeatCount.Width = 90;
            // 
            // WindowTitle
            // 
            this.WindowTitle.HeaderText = "Window Title";
            this.WindowTitle.Name = "WindowTitle";
            this.WindowTitle.ReadOnly = true;
            this.WindowTitle.Width = 87;
            // 
            // WindowHandle
            // 
            this.WindowHandle.HeaderText = "Window Handle";
            this.WindowHandle.Name = "WindowHandle";
            this.WindowHandle.ReadOnly = true;
            this.WindowHandle.Width = 99;
            // 
            // ProcessName
            // 
            this.ProcessName.HeaderText = "Process Name";
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.ReadOnly = true;
            this.ProcessName.Width = 93;
            // 
            // CurrentAction
            // 
            this.CurrentAction.HeaderText = "Current Action";
            this.CurrentAction.Name = "CurrentAction";
            this.CurrentAction.ReadOnly = true;
            this.CurrentAction.Width = 91;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 547);
            this.Controls.Add(this.MainDataGrid);
            this.Controls.Add(this.button_RemoveRepeatGroup);
            this.Controls.Add(this.tabControl_ScriptEvents);
            this.Controls.Add(this.MenuStrip_MainForm);
            this.Controls.Add(this.NumericUpDown_RepeatAmount);
            this.Controls.Add(this.button_AddRepeatGroup);
            this.Controls.Add(this.button_RemoveEvent);
            this.Controls.Add(this.button_MoveEventDown);
            this.Controls.Add(this.button_MoveEventUp);
            this.Controls.Add(this.button_StartScript);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip_MainForm;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Dolphin Script";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RepeatAmount)).EndInit();
            this.MenuStrip_MainForm.ResumeLayout(false);
            this.MenuStrip_MainForm.PerformLayout();
            this.tabControl_ScriptEvents.ResumeLayout(false);
            this.tabPage_PauseEvent.ResumeLayout(false);
            this.groupBox25.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fixedDelayNumberBox)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upperRandomDelayNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerRandomDelayNumberBox)).EndInit();
            this.tabPage2_KeyboardEvent.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tabPage_MouseMoveEvent.ResumeLayout(false);
            this.groupBox_mouseMovementMode.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MaxMouseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MinMouseSpeed)).EndInit();
            this.tabPage_MouseMoveToColour.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_ColourSelectionArea)).EndInit();
            this.groupBox22.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.tabPage_MouseClick.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_StartScript;
        private System.Windows.Forms.Button button_MoveEventUp;
        private System.Windows.Forms.Button button_MoveEventDown;
        private System.Windows.Forms.Button button_RemoveEvent;
        public System.Windows.Forms.Button registerClickLocationsButton;
        private System.Windows.Forms.Button button_AddRepeatGroup;
        private System.Windows.Forms.NumericUpDown NumericUpDown_RepeatAmount;
        private System.Windows.Forms.MenuStrip MenuStrip_MainForm;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem saveScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadScriptToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl_ScriptEvents;
        private System.Windows.Forms.TabPage tabPage_PauseEvent;
        private System.Windows.Forms.TabPage tabPage2_KeyboardEvent;
        private System.Windows.Forms.TabPage tabPage_MouseMoveEvent;
        private System.Windows.Forms.GroupBox groupBox_MouseSpeed;
        private System.Windows.Forms.NumericUpDown NumericUpDown_MinMouseSpeed;
        private System.Windows.Forms.TabPage tabPage_MouseClick;
        private System.Windows.Forms.GroupBox groupBox_RelativeToScreen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Button_InsertMouseMoveToAreaEvent;
        private System.Windows.Forms.Button Button_InsertMouseMoveEvent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_MousePosY_1;
        private System.Windows.Forms.TextBox TextBox_MousePosX_1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowTitle_1;
        private System.Windows.Forms.Button Button_InsertMouseMoveToAreaOnWindowEvent;
        private System.Windows.Forms.Button Button_InsertMouseMoveToPointOnWindowEvent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseY_1;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseX_1;
        private System.Windows.Forms.Button Button_ColourPreview2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button Button_AddFixedPause;
        private System.Windows.Forms.NumericUpDown fixedDelayNumberBox;
        private System.Windows.Forms.Button Button_AddRandomPause;
        private System.Windows.Forms.NumericUpDown upperRandomDelayNumberBox;
        private System.Windows.Forms.NumericUpDown lowerRandomDelayNumberBox;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourExistsInArea;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourDoesntExistInArea;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourExistsInAreaOnWindow;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourDoesntExistInAreaOnWindow;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button Button_AddSpecialButton;
        private System.Windows.Forms.RichTextBox RichTextBox_KeyboardEvent;
        private System.Windows.Forms.ComboBox ComboBox_SpecialKeys;
        private System.Windows.Forms.Button Button_AddKeypressEvent;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button Button_InsertRMBUp;
        private System.Windows.Forms.Button Button_InsertMMBUp;
        private System.Windows.Forms.Button Button_InsertLMBUp;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button Button_InsertRMBDown;
        private System.Windows.Forms.Button Button_InsertLMBDown;
        private System.Windows.Forms.Button Button_InsertMMBDown;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button Button_InsertLeftClickEvent;
        private System.Windows.Forms.Button Button_InsertRightClickEvent;
        private System.Windows.Forms.Button Buton_InsertMiddleMouseClickEvent;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button Button_ColourPreview1;
        private System.Windows.Forms.TabPage tabPage_MouseMoveToColour;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Button Button_InsertColourSearchAreaEvent;
        private System.Windows.Forms.Button Button_InsertColourSearchAreaWindowEvent;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Button Button_ColourPreview3;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseY_2;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseX_2;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowTitle_2;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TextBox_MousePosY_2;
        private System.Windows.Forms.TextBox TextBox_MousePosX_2;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.Button button_RemoveRepeatGroup;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wikiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox Picturebox_ColourSelectionArea;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.ToolStripMenuItem refreshFormToolStripMenuItem;
        private System.Windows.Forms.Label label_MaxMouseSpeed;
        private System.Windows.Forms.Label label_MinMouseSpeed;
        private System.Windows.Forms.NumericUpDown NumericUpDown_MaxMouseSpeed;
        private System.Windows.Forms.GroupBox groupBox_mouseMovementMode;
        private System.Windows.Forms.ComboBox ComboBox_MouseMovementMode;
        public System.Windows.Forms.DataGridView MainDataGrid;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RepeatCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowHandle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentAction;
    }
}

