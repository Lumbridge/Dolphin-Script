namespace DolphinScript.Forms.PauseForms
{
    partial class FixedPauseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FixedPauseForm));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.fixedDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.Button_SavePauseEvent = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fixedDelayNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.fixedDelayNumberBox);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(192, 58);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Amount of time to pause (seconds)";
            // 
            // fixedDelayNumberBox
            // 
            this.fixedDelayNumberBox.DecimalPlaces = 1;
            this.fixedDelayNumberBox.Location = new System.Drawing.Point(50, 24);
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
            // Button_SavePauseEvent
            // 
            this.Button_SavePauseEvent.Location = new System.Drawing.Point(62, 76);
            this.Button_SavePauseEvent.Name = "Button_SavePauseEvent";
            this.Button_SavePauseEvent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Button_SavePauseEvent.Size = new System.Drawing.Size(75, 23);
            this.Button_SavePauseEvent.TabIndex = 68;
            this.Button_SavePauseEvent.Text = "Save";
            this.Button_SavePauseEvent.UseVisualStyleBackColor = true;
            // 
            // FixedPauseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 105);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.Button_SavePauseEvent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FixedPauseForm";
            this.Text = "Fixed Pause";
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fixedDelayNumberBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown fixedDelayNumberBox;
        private System.Windows.Forms.Button Button_SavePauseEvent;
    }
}