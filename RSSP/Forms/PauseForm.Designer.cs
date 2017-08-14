namespace RSSP
{
    partial class PauseForm
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
            this.Button_AddFixedPause = new System.Windows.Forms.Button();
            this.fixedDelayLabel = new System.Windows.Forms.Label();
            this.fixedDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.Button_AddRandomPause = new System.Windows.Forms.Button();
            this.highestAmountOfDelayLabel = new System.Windows.Forms.Label();
            this.lowestAmountOfDelayLabel = new System.Windows.Forms.Label();
            this.upperRandomDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.lowerRandomDelayNumberBox = new System.Windows.Forms.NumericUpDown();
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow = new System.Windows.Forms.Button();
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow = new System.Windows.Forms.Button();
            this.Button_InsertPauseWhileColourExistsInArea = new System.Windows.Forms.Button();
            this.Button_InsertPauseWhileColourDoesntExistInArea = new System.Windows.Forms.Button();
            this.Button_ColourPreview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fixedDelayNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperRandomDelayNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerRandomDelayNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_AddFixedPause
            // 
            this.Button_AddFixedPause.Location = new System.Drawing.Point(12, 51);
            this.Button_AddFixedPause.Name = "Button_AddFixedPause";
            this.Button_AddFixedPause.Size = new System.Drawing.Size(116, 39);
            this.Button_AddFixedPause.TabIndex = 50;
            this.Button_AddFixedPause.Text = "Add Fixed Pause";
            this.Button_AddFixedPause.UseVisualStyleBackColor = true;
            this.Button_AddFixedPause.Click += new System.EventHandler(this.Button_AddFixedPause_Click);
            // 
            // fixedDelayLabel
            // 
            this.fixedDelayLabel.AutoSize = true;
            this.fixedDelayLabel.Location = new System.Drawing.Point(12, 9);
            this.fixedDelayLabel.Name = "fixedDelayLabel";
            this.fixedDelayLabel.Size = new System.Drawing.Size(116, 13);
            this.fixedDelayLabel.TabIndex = 49;
            this.fixedDelayLabel.Text = "Fixed Pause (Seconds)";
            // 
            // fixedDelayNumberBox
            // 
            this.fixedDelayNumberBox.DecimalPlaces = 1;
            this.fixedDelayNumberBox.Location = new System.Drawing.Point(12, 25);
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
            this.fixedDelayNumberBox.Size = new System.Drawing.Size(116, 20);
            this.fixedDelayNumberBox.TabIndex = 48;
            this.fixedDelayNumberBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // Button_AddRandomPause
            // 
            this.Button_AddRandomPause.Location = new System.Drawing.Point(168, 51);
            this.Button_AddRandomPause.Name = "Button_AddRandomPause";
            this.Button_AddRandomPause.Size = new System.Drawing.Size(157, 39);
            this.Button_AddRandomPause.TabIndex = 53;
            this.Button_AddRandomPause.Text = "Add Random Delay Between Min and Max";
            this.Button_AddRandomPause.UseVisualStyleBackColor = true;
            this.Button_AddRandomPause.Click += new System.EventHandler(this.Button_AddRandomPause_Click);
            // 
            // highestAmountOfDelayLabel
            // 
            this.highestAmountOfDelayLabel.AutoSize = true;
            this.highestAmountOfDelayLabel.Location = new System.Drawing.Point(246, 9);
            this.highestAmountOfDelayLabel.Name = "highestAmountOfDelayLabel";
            this.highestAmountOfDelayLabel.Size = new System.Drawing.Size(78, 13);
            this.highestAmountOfDelayLabel.TabIndex = 55;
            this.highestAmountOfDelayLabel.Text = "Max (Seconds)";
            // 
            // lowestAmountOfDelayLabel
            // 
            this.lowestAmountOfDelayLabel.AutoSize = true;
            this.lowestAmountOfDelayLabel.Location = new System.Drawing.Point(165, 9);
            this.lowestAmountOfDelayLabel.Name = "lowestAmountOfDelayLabel";
            this.lowestAmountOfDelayLabel.Size = new System.Drawing.Size(75, 13);
            this.lowestAmountOfDelayLabel.TabIndex = 54;
            this.lowestAmountOfDelayLabel.Text = "Min (Seconds)";
            // 
            // upperRandomDelayNumberBox
            // 
            this.upperRandomDelayNumberBox.DecimalPlaces = 1;
            this.upperRandomDelayNumberBox.Location = new System.Drawing.Point(249, 25);
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
            this.upperRandomDelayNumberBox.TabIndex = 52;
            this.upperRandomDelayNumberBox.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            // 
            // lowerRandomDelayNumberBox
            // 
            this.lowerRandomDelayNumberBox.DecimalPlaces = 1;
            this.lowerRandomDelayNumberBox.Location = new System.Drawing.Point(168, 25);
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
            this.lowerRandomDelayNumberBox.TabIndex = 51;
            this.lowerRandomDelayNumberBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // Button_InsertPauseWhileColourDoesntExistInAreaOnWindow
            // 
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Location = new System.Drawing.Point(12, 96);
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Name = "Button_InsertPauseWhileColourDoesntExistInAreaOnWindow";
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Size = new System.Drawing.Size(116, 66);
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.TabIndex = 56;
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = "Pause While Colour Doesn\'t Exist In Area On Window";
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Click += new System.EventHandler(this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow_Click);
            // 
            // Button_InsertPauseWhileColourExistsInAreaOnWindow
            // 
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Location = new System.Drawing.Point(134, 96);
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Name = "Button_InsertPauseWhileColourExistsInAreaOnWindow";
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Size = new System.Drawing.Size(116, 66);
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.TabIndex = 57;
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = "Pause While Colour Exists In Area On Window";
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourExistsInAreaOnWindow.Click += new System.EventHandler(this.Button_InsertPauseWhileColourExistsInAreaOnWindow_Click);
            // 
            // Button_InsertPauseWhileColourExistsInArea
            // 
            this.Button_InsertPauseWhileColourExistsInArea.Location = new System.Drawing.Point(134, 168);
            this.Button_InsertPauseWhileColourExistsInArea.Name = "Button_InsertPauseWhileColourExistsInArea";
            this.Button_InsertPauseWhileColourExistsInArea.Size = new System.Drawing.Size(116, 66);
            this.Button_InsertPauseWhileColourExistsInArea.TabIndex = 59;
            this.Button_InsertPauseWhileColourExistsInArea.Text = "Pause While Colour Exists In Area";
            this.Button_InsertPauseWhileColourExistsInArea.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourExistsInArea.Click += new System.EventHandler(this.Button_InsertPauseWhileColourExistsInArea_Click);
            // 
            // Button_InsertPauseWhileColourDoesntExistInArea
            // 
            this.Button_InsertPauseWhileColourDoesntExistInArea.Location = new System.Drawing.Point(12, 168);
            this.Button_InsertPauseWhileColourDoesntExistInArea.Name = "Button_InsertPauseWhileColourDoesntExistInArea";
            this.Button_InsertPauseWhileColourDoesntExistInArea.Size = new System.Drawing.Size(116, 66);
            this.Button_InsertPauseWhileColourDoesntExistInArea.TabIndex = 58;
            this.Button_InsertPauseWhileColourDoesntExistInArea.Text = "Pause While Colour Doesn\'t Exist In Area";
            this.Button_InsertPauseWhileColourDoesntExistInArea.UseVisualStyleBackColor = true;
            this.Button_InsertPauseWhileColourDoesntExistInArea.Click += new System.EventHandler(this.Button_InsertPauseWhileColourDoesntExistInArea_Click);
            // 
            // Button_ColourPreview
            // 
            this.Button_ColourPreview.Location = new System.Drawing.Point(256, 96);
            this.Button_ColourPreview.Name = "Button_ColourPreview";
            this.Button_ColourPreview.Size = new System.Drawing.Size(66, 138);
            this.Button_ColourPreview.TabIndex = 60;
            this.Button_ColourPreview.UseVisualStyleBackColor = true;
            // 
            // PauseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 239);
            this.Controls.Add(this.Button_ColourPreview);
            this.Controls.Add(this.Button_InsertPauseWhileColourExistsInArea);
            this.Controls.Add(this.Button_InsertPauseWhileColourDoesntExistInArea);
            this.Controls.Add(this.Button_InsertPauseWhileColourExistsInAreaOnWindow);
            this.Controls.Add(this.Button_InsertPauseWhileColourDoesntExistInAreaOnWindow);
            this.Controls.Add(this.Button_AddRandomPause);
            this.Controls.Add(this.highestAmountOfDelayLabel);
            this.Controls.Add(this.lowestAmountOfDelayLabel);
            this.Controls.Add(this.upperRandomDelayNumberBox);
            this.Controls.Add(this.lowerRandomDelayNumberBox);
            this.Controls.Add(this.Button_AddFixedPause);
            this.Controls.Add(this.fixedDelayLabel);
            this.Controls.Add(this.fixedDelayNumberBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PauseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PauseForm";
            ((System.ComponentModel.ISupportInitialize)(this.fixedDelayNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperRandomDelayNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerRandomDelayNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_AddFixedPause;
        private System.Windows.Forms.Label fixedDelayLabel;
        private System.Windows.Forms.NumericUpDown fixedDelayNumberBox;
        private System.Windows.Forms.Button Button_AddRandomPause;
        private System.Windows.Forms.Label highestAmountOfDelayLabel;
        private System.Windows.Forms.Label lowestAmountOfDelayLabel;
        private System.Windows.Forms.NumericUpDown upperRandomDelayNumberBox;
        private System.Windows.Forms.NumericUpDown lowerRandomDelayNumberBox;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourDoesntExistInAreaOnWindow;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourExistsInAreaOnWindow;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourExistsInArea;
        private System.Windows.Forms.Button Button_InsertPauseWhileColourDoesntExistInArea;
        private System.Windows.Forms.Button Button_ColourPreview;
    }
}