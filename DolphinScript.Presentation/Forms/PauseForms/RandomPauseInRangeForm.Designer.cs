namespace DolphinScript.Forms.PauseForms
{
    partial class RandomPauseInRangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomPauseInRangeForm));
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.upperRandomDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.lowerRandomDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upperRandomDelayNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerRandomDelayNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.upperRandomDelayNumberBox);
            this.groupBox6.Controls.Add(this.lowerRandomDelayNumberBox);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(177, 50);
            this.groupBox6.TabIndex = 54;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Random Pause within Range";
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 26);
            this.label1.TabIndex = 55;
            this.label1.Text = "Close this window when you\'ve \r\nfinished editing";
            // 
            // RandomPauseInRangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 102);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RandomPauseInRangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Random Pause in Range";
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upperRandomDelayNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerRandomDelayNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown upperRandomDelayNumberBox;
        private System.Windows.Forms.NumericUpDown lowerRandomDelayNumberBox;
        private System.Windows.Forms.Label label1;
    }
}