namespace RSSP
{
    partial class MouseMoveForm
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
            this.Button_InsertMouseMoveToAreaEvent = new System.Windows.Forms.Button();
            this.Button_InsertMouseMoveEvent = new System.Windows.Forms.Button();
            this.Button_InsertMouseMoveEventSpecificWindow = new System.Windows.Forms.Button();
            this.Button_InsertMouseMoveToAreaSpecificWindow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_MousePosX = new System.Windows.Forms.TextBox();
            this.TextBox_MousePosY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBox_ActiveWindowMouseY = new System.Windows.Forms.TextBox();
            this.TextBox_ActiveWindowMouseX = new System.Windows.Forms.TextBox();
            this.TextBox_ActiveWindowTitle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Button_InsertColourSearchAreaEvent = new System.Windows.Forms.Button();
            this.Button_InsertColourSearchAreaWindowEvent = new System.Windows.Forms.Button();
            this.Button_InsertMultiColourSearchAreaWindowEvent = new System.Windows.Forms.Button();
            this.Picturebox_ColourSelectionArea = new System.Windows.Forms.PictureBox();
            this.Button_ColourPreview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_ColourSelectionArea)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_InsertMouseMoveToAreaEvent
            // 
            this.Button_InsertMouseMoveToAreaEvent.Location = new System.Drawing.Point(90, 25);
            this.Button_InsertMouseMoveToAreaEvent.Name = "Button_InsertMouseMoveToAreaEvent";
            this.Button_InsertMouseMoveToAreaEvent.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertMouseMoveToAreaEvent.TabIndex = 6;
            this.Button_InsertMouseMoveToAreaEvent.Text = "Mouse Move To Random Point in Area";
            this.Button_InsertMouseMoveToAreaEvent.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveToAreaEvent.Click += new System.EventHandler(this.Button_InsertMouseMoveToAreaEvent_Click);
            // 
            // Button_InsertMouseMoveEvent
            // 
            this.Button_InsertMouseMoveEvent.Location = new System.Drawing.Point(9, 25);
            this.Button_InsertMouseMoveEvent.Name = "Button_InsertMouseMoveEvent";
            this.Button_InsertMouseMoveEvent.Size = new System.Drawing.Size(75, 62);
            this.Button_InsertMouseMoveEvent.TabIndex = 7;
            this.Button_InsertMouseMoveEvent.Text = "Mouse Move To Fixed Point";
            this.Button_InsertMouseMoveEvent.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveEvent.Click += new System.EventHandler(this.Button_InsertMouseMoveEvent_Click);
            // 
            // Button_InsertMouseMoveEventSpecificWindow
            // 
            this.Button_InsertMouseMoveEventSpecificWindow.Location = new System.Drawing.Point(9, 176);
            this.Button_InsertMouseMoveEventSpecificWindow.Name = "Button_InsertMouseMoveEventSpecificWindow";
            this.Button_InsertMouseMoveEventSpecificWindow.Size = new System.Drawing.Size(99, 52);
            this.Button_InsertMouseMoveEventSpecificWindow.TabIndex = 9;
            this.Button_InsertMouseMoveEventSpecificWindow.Text = "Mouse Move To Fixed Point On Window";
            this.Button_InsertMouseMoveEventSpecificWindow.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveEventSpecificWindow.Click += new System.EventHandler(this.Button_InsertMouseMoveEventSpecificWindow_Click);
            // 
            // Button_InsertMouseMoveToAreaSpecificWindow
            // 
            this.Button_InsertMouseMoveToAreaSpecificWindow.Location = new System.Drawing.Point(115, 176);
            this.Button_InsertMouseMoveToAreaSpecificWindow.Name = "Button_InsertMouseMoveToAreaSpecificWindow";
            this.Button_InsertMouseMoveToAreaSpecificWindow.Size = new System.Drawing.Size(99, 52);
            this.Button_InsertMouseMoveToAreaSpecificWindow.TabIndex = 8;
            this.Button_InsertMouseMoveToAreaSpecificWindow.Text = "Mouse Move To Random Point in Area On Window";
            this.Button_InsertMouseMoveToAreaSpecificWindow.UseVisualStyleBackColor = true;
            this.Button_InsertMouseMoveToAreaSpecificWindow.Click += new System.EventHandler(this.Button_InsertMouseMoveToAreaSpecificWindow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Relative To Screen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Relative To Specific Window";
            // 
            // TextBox_MousePosX
            // 
            this.TextBox_MousePosX.Enabled = false;
            this.TextBox_MousePosX.Location = new System.Drawing.Point(138, 93);
            this.TextBox_MousePosX.Name = "TextBox_MousePosX";
            this.TextBox_MousePosX.ReadOnly = true;
            this.TextBox_MousePosX.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosX.TabIndex = 12;
            this.TextBox_MousePosX.TabStop = false;
            // 
            // TextBox_MousePosY
            // 
            this.TextBox_MousePosY.Enabled = false;
            this.TextBox_MousePosY.Location = new System.Drawing.Point(138, 119);
            this.TextBox_MousePosY.Name = "TextBox_MousePosY";
            this.TextBox_MousePosY.ReadOnly = true;
            this.TextBox_MousePosY.Size = new System.Drawing.Size(66, 20);
            this.TextBox_MousePosY.TabIndex = 13;
            this.TextBox_MousePosY.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Cursor Pos on Screen X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Cursor Pos on Screen Y:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Cursor Pos On Window Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Cursor Pos On Window X:";
            // 
            // TextBox_ActiveWindowMouseY
            // 
            this.TextBox_ActiveWindowMouseY.Enabled = false;
            this.TextBox_ActiveWindowMouseY.Location = new System.Drawing.Point(152, 286);
            this.TextBox_ActiveWindowMouseY.Name = "TextBox_ActiveWindowMouseY";
            this.TextBox_ActiveWindowMouseY.ReadOnly = true;
            this.TextBox_ActiveWindowMouseY.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseY.TabIndex = 17;
            this.TextBox_ActiveWindowMouseY.TabStop = false;
            // 
            // TextBox_ActiveWindowMouseX
            // 
            this.TextBox_ActiveWindowMouseX.Enabled = false;
            this.TextBox_ActiveWindowMouseX.Location = new System.Drawing.Point(152, 260);
            this.TextBox_ActiveWindowMouseX.Name = "TextBox_ActiveWindowMouseX";
            this.TextBox_ActiveWindowMouseX.ReadOnly = true;
            this.TextBox_ActiveWindowMouseX.Size = new System.Drawing.Size(66, 20);
            this.TextBox_ActiveWindowMouseX.TabIndex = 16;
            this.TextBox_ActiveWindowMouseX.TabStop = false;
            // 
            // TextBox_ActiveWindowTitle
            // 
            this.TextBox_ActiveWindowTitle.Enabled = false;
            this.TextBox_ActiveWindowTitle.Location = new System.Drawing.Point(94, 234);
            this.TextBox_ActiveWindowTitle.Name = "TextBox_ActiveWindowTitle";
            this.TextBox_ActiveWindowTitle.ReadOnly = true;
            this.TextBox_ActiveWindowTitle.Size = new System.Drawing.Size(180, 20);
            this.TextBox_ActiveWindowTitle.TabIndex = 20;
            this.TextBox_ActiveWindowTitle.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Window Title:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 323);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Move Mouse To Colour";
            // 
            // Button_InsertColourSearchAreaEvent
            // 
            this.Button_InsertColourSearchAreaEvent.Location = new System.Drawing.Point(12, 339);
            this.Button_InsertColourSearchAreaEvent.Name = "Button_InsertColourSearchAreaEvent";
            this.Button_InsertColourSearchAreaEvent.Size = new System.Drawing.Size(107, 62);
            this.Button_InsertColourSearchAreaEvent.TabIndex = 24;
            this.Button_InsertColourSearchAreaEvent.Text = "Colour Search Area Relative To Screen";
            this.Button_InsertColourSearchAreaEvent.UseVisualStyleBackColor = true;
            this.Button_InsertColourSearchAreaEvent.Click += new System.EventHandler(this.Button_InsertColourSearchAreaEvent_Click);
            // 
            // Button_InsertColourSearchAreaWindowEvent
            // 
            this.Button_InsertColourSearchAreaWindowEvent.Location = new System.Drawing.Point(125, 339);
            this.Button_InsertColourSearchAreaWindowEvent.Name = "Button_InsertColourSearchAreaWindowEvent";
            this.Button_InsertColourSearchAreaWindowEvent.Size = new System.Drawing.Size(107, 62);
            this.Button_InsertColourSearchAreaWindowEvent.TabIndex = 23;
            this.Button_InsertColourSearchAreaWindowEvent.Text = "Colour Search Area on Specific Window";
            this.Button_InsertColourSearchAreaWindowEvent.UseVisualStyleBackColor = true;
            this.Button_InsertColourSearchAreaWindowEvent.Click += new System.EventHandler(this.Button_InsertColourSearchAreaWindowEvent_Click);
            // 
            // Button_InsertMultiColourSearchAreaWindowEvent
            // 
            this.Button_InsertMultiColourSearchAreaWindowEvent.Location = new System.Drawing.Point(238, 339);
            this.Button_InsertMultiColourSearchAreaWindowEvent.Name = "Button_InsertMultiColourSearchAreaWindowEvent";
            this.Button_InsertMultiColourSearchAreaWindowEvent.Size = new System.Drawing.Size(107, 62);
            this.Button_InsertMultiColourSearchAreaWindowEvent.TabIndex = 25;
            this.Button_InsertMultiColourSearchAreaWindowEvent.Text = "Multi Colour Search Area on Specific Window";
            this.Button_InsertMultiColourSearchAreaWindowEvent.UseVisualStyleBackColor = true;
            this.Button_InsertMultiColourSearchAreaWindowEvent.Click += new System.EventHandler(this.Button_InsertMultiColourSearchAreaWindowEvent_Click);
            // 
            // Picturebox_ColourSelectionArea
            // 
            this.Picturebox_ColourSelectionArea.Location = new System.Drawing.Point(351, 12);
            this.Picturebox_ColourSelectionArea.Name = "Picturebox_ColourSelectionArea";
            this.Picturebox_ColourSelectionArea.Size = new System.Drawing.Size(609, 389);
            this.Picturebox_ColourSelectionArea.TabIndex = 26;
            this.Picturebox_ColourSelectionArea.TabStop = false;
            // 
            // Button_ColourPreview
            // 
            this.Button_ColourPreview.Location = new System.Drawing.Point(238, 263);
            this.Button_ColourPreview.Name = "Button_ColourPreview";
            this.Button_ColourPreview.Size = new System.Drawing.Size(107, 70);
            this.Button_ColourPreview.TabIndex = 61;
            this.Button_ColourPreview.UseVisualStyleBackColor = true;
            // 
            // MouseMoveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 413);
            this.Controls.Add(this.Button_ColourPreview);
            this.Controls.Add(this.Picturebox_ColourSelectionArea);
            this.Controls.Add(this.Button_InsertMultiColourSearchAreaWindowEvent);
            this.Controls.Add(this.Button_InsertColourSearchAreaEvent);
            this.Controls.Add(this.Button_InsertColourSearchAreaWindowEvent);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TextBox_ActiveWindowTitle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TextBox_ActiveWindowMouseY);
            this.Controls.Add(this.TextBox_ActiveWindowMouseX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBox_MousePosY);
            this.Controls.Add(this.TextBox_MousePosX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_InsertMouseMoveEventSpecificWindow);
            this.Controls.Add(this.Button_InsertMouseMoveToAreaSpecificWindow);
            this.Controls.Add(this.Button_InsertMouseMoveEvent);
            this.Controls.Add(this.Button_InsertMouseMoveToAreaEvent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MouseMoveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mouse Move Event";
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_ColourSelectionArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_InsertMouseMoveToAreaEvent;
        private System.Windows.Forms.Button Button_InsertMouseMoveEvent;
        private System.Windows.Forms.Button Button_InsertMouseMoveEventSpecificWindow;
        private System.Windows.Forms.Button Button_InsertMouseMoveToAreaSpecificWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_MousePosX;
        private System.Windows.Forms.TextBox TextBox_MousePosY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseY;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowMouseX;
        private System.Windows.Forms.TextBox TextBox_ActiveWindowTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Button_InsertColourSearchAreaEvent;
        private System.Windows.Forms.Button Button_InsertColourSearchAreaWindowEvent;
        private System.Windows.Forms.Button Button_InsertMultiColourSearchAreaWindowEvent;
        private System.Windows.Forms.PictureBox Picturebox_ColourSelectionArea;
        private System.Windows.Forms.Button Button_ColourPreview;
    }
}